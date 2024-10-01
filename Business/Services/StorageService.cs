using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Interfaces;

namespace CadwiseAutomaticTellerMachine.Business.Services
{
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IAuthService _authService;
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

            await ValidateMoneyRequest(moneyRequest);

            var result = new List<BanknoteQuantityDto>();
            var banknoteQuantities = await _storageRepository.GetBanknoteQuantity();
            var denominations = banknoteQuantities.OrderByDescending(x => x.Denomination).ToList();

            temp = ProcessWithdraw(denominations, moneyRequest, result);

            if (temp > 0)
                throw new InvalidOperationException("Недостаточно средств для выдачи запрашиваемой суммы");

            await _storageRepository.UpdateBanknotesQuantity(result);
            _authService.CurrentCard!.Cash -= moneyRequest;
            await _cardService.Update(_authService.CurrentCard);

            return result;
        }

        public async Task<List<BanknoteQuantityDto>> WithdrawMoneySmall(int moneyRequest)
        {
            await ValidateMoneyRequest(moneyRequest);

            var result = new List<BanknoteQuantityDto>();
            var banknoteQuantity = await _storageRepository.GetBanknoteQuantity();
            var allDenominations = banknoteQuantity.OrderByDescending(x => x.Denomination).ToList();
            var smallDenominations = allDenominations.Where(x => x.Denomination <= 500).ToList();
            int requestSmall = 0, requestBig = 0;

            if (moneyRequest <= 1000)
            {
                requestSmall = ProcessWithdraw(smallDenominations, moneyRequest, result);
            }
            else
            {
                int remainder = moneyRequest % 1000;
                requestSmall = (remainder == 0) ? 1000 : remainder + 1000;
                requestBig = moneyRequest - requestSmall;

                requestSmall = ProcessWithdraw(smallDenominations, requestSmall, result);
                requestBig = ProcessWithdraw(allDenominations, requestBig, result);
            }

            if (requestBig > 0 || requestSmall > 0)
            {
                result.Clear();
                requestBig = ProcessWithdraw(allDenominations, moneyRequest, result);
                if (requestBig > 0)
                    throw new InvalidOperationException("Недостаточно средств для выдачи запрашиваемой суммы");
            }

            result = result.OrderByDescending(x => x.Denomination).ToList();

            await _storageRepository.UpdateBanknotesQuantity(result);
            _authService.CurrentCard!.Cash -= moneyRequest;
            await _cardService.Update(_authService.CurrentCard);

            return result;
        }

        public int ProcessWithdraw(List<BanknoteQuantityDto> denominations, int request, List<BanknoteQuantityDto> result)
        {
            foreach (var banknote in denominations)
            {
                if (request == 0) break;

                int max = Math.Min(request / banknote.Denomination, banknote.Quantity);
                if (max > 0)
                {
                    result.Add(new BanknoteQuantityDto
                    {
                        Denomination = banknote.Denomination,
                        Quantity = max
                    });
                    request -= max * banknote.Denomination;
                }
            }
            return request;
        }

        public async Task ValidateMoneyRequest(int moneyRequest)
        {
            if (moneyRequest <= 0)
                throw new ArgumentException("Сумма должна быть положительной");

            if (moneyRequest > _authService.CurrentCard!.Cash)
                throw new ArgumentException("На вашей карте недостаточно средств");

            int storageBalance = await GetStorageBalance();
            if (moneyRequest > storageBalance)
                throw new InvalidOperationException("Недостаточно средств для выдачи запрашиваемой суммы");
        }

        public async Task IncreaseBanknoteQuantity(List<BanknoteQuantityDto> banknotesQuantity)
        {
            await _storageRepository.IncreaseBanknoteQuantity(banknotesQuantity);
        }
    }
}
