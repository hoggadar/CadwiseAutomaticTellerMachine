using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Commands;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{
    public class HelpViewModel : ViewModelBase
    {
        private readonly ICardService _cardService;
        private readonly IStorageService _storageService;

        public ICommand IncreaseCashCommand { get; }
        public ICommand IncreaseBanknoteCommand { get; }

        public HelpViewModel(ICardService cardService, IStorageService storageService)
        {
            _cardService = cardService;
            _storageService = storageService;

            IncreaseCashCommand = new RelayCommand(async _ => await IncreaseCashFunc(), _ => true);
            IncreaseBanknoteCommand = new RelayCommand(async _ => await IncreaseBanknoteFunc(), _ => true);
        }

        private async Task IncreaseCashFunc()
        {
            await _cardService.IncreaseCash();
        }

        private async Task IncreaseBanknoteFunc()
        {
            List<BanknoteQuantityDto> dtos = new List<BanknoteQuantityDto>
            {
                new BanknoteQuantityDto{ Denomination = 5000, Quantity = 100 },
                new BanknoteQuantityDto{ Denomination = 2000, Quantity = 100 },
                new BanknoteQuantityDto{ Denomination = 1000, Quantity = 100 },
                new BanknoteQuantityDto{ Denomination = 500, Quantity = 100 },
                new BanknoteQuantityDto{ Denomination = 200, Quantity = 100 },
                new BanknoteQuantityDto{ Denomination = 100, Quantity = 100 },
                new BanknoteQuantityDto{ Denomination = 50, Quantity = 100 },
                new BanknoteQuantityDto{ Denomination = 10, Quantity = 100 },
            };
            await _storageService.IncreaseBanknoteQuantity(dtos);
        }
    }
}
