using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Interfaces;
using System.Windows;

namespace CadwiseAutomaticTellerMachine.Business.Services
{
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository _storageRepository;
        private IAuthService _authService;
        private readonly ICardService _cardService;

        public StorageService(IStorageRepository storageRepository, IAuthService authService, ICardService cardService)
        {
            _storageRepository = storageRepository;
            _authService = authService;
            _cardService = cardService;
        }

        public async Task<int> GetStorageBalance()
        {
            return await _storageRepository.GetStorageBalance();
        }

        public async Task<List<BanknoteQuantityDto>> GetBanknoteQuantity()
        {
            return await _storageRepository.GetBanknoteQuantity();
        }

        public async Task<List<BanknoteQuantityDto>> WithdrawMoneyBig(int moneyRequest)
        {
            int temp = moneyRequest;

            if (temp < 0)
                throw new ArgumentException("Сумма должна быть положительной");

            if (temp > _authService.CurrentCard!.Cash)
                throw new ArgumentException("На вашей карте недостаточно средств");

            int storageBalance = await GetStorageBalance();
            if (temp > storageBalance)
                throw new InvalidOperationException("Недостаточно средств для выдачи запрашиваемой суммы");

            var result = new List<BanknoteQuantityDto>();
            var banknoteQuantities = await _storageRepository.GetBanknoteQuantity();
            var denominations = banknoteQuantities.OrderByDescending(x => x.Denomination).ToList();

            foreach (var banknote in denominations)
            {
                if (temp == 0) break;

                int maxBills = Math.Min(temp / banknote.Denomination, banknote.Quantity);
                if (maxBills > 0)
                {
                    result.Add(new BanknoteQuantityDto
                    {
                        Denomination = banknote.Denomination,
                        Quantity = maxBills
                    });
                    temp -= maxBills * banknote.Denomination;
                }
            }

            if (temp > 0)
                throw new InvalidOperationException("Недостаточно средств для выдачи запрашиваемой суммы");

            await _storageRepository.UpdateBanknotesQuantity(result);
            _authService.CurrentCard.Cash -= moneyRequest;
            await _cardService.Update(_authService.CurrentCard);

            return result;
        }
    }
}
