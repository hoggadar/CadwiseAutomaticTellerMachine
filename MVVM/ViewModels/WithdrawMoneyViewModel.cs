using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Commands;
using CadwiseAutomaticTellerMachine.MVVM.Models;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using System.Windows;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{
    public class WithdrawMoneyViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IAuthService _authService;
        private IStorageService _storageService;

        private string _userFullName;
        private int _userBalance;
        private int _atmBalance;
        private int _userMoneyRequest;
        private List<BanknoteQuantityDto> _result;

        public ICommand NavigateToServiceCommand { get; }
        public ICommand WithdrawMoneyCommand { get; }

        public WithdrawMoneyViewModel(INavigationService navigationService, IAuthService authService, IStorageService storageService)
        {
            _navigationService = navigationService;
            _authService = authService;
            _storageService = storageService;

            _userFullName = string.Empty;
            _userBalance = 0;
            _atmBalance = 0;
            _result = new List<BanknoteQuantityDto>();

            NavigateToServiceCommand = new RelayCommand(_ => NavigationService.NavigateTo<ServiceViewModel>(), _ => true);
            WithdrawMoneyCommand = new RelayCommand(_ => WithdrawMoney(), _ => true);
            NavigationService.NavigatedTo += LoadUserData;
            
        }

        private async void LoadUserData()
        {
            var currentUser = _authService.CurrentUser;
            var card = _authService.CurrentCard;

            UserFullName = $"{currentUser!.Name} {currentUser!.Surname}";
            UserBalance = card!.Cash;
            ATMBalance = await _storageService.GetStorageBalance();
        }

        private async void WithdrawMoney()
        {
            try
            {
                _result = await _storageService.WithdrawMoneyBig(UserMoneyRequest);
                if (_result != null && _result.Count > 0)
                {
                    var message = "Выданные купюры:\n";
                    foreach (var banknote in _result)
                    {
                        message += $"Номинал: {banknote.Denomination} - Количество: {banknote.Quantity}\n";
                    }
                    MessageBox.Show(message, "Выдача денег", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Не удалось выдать деньги. Проверьте сумму или наличие купюр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged(nameof(NavigationService));
            }
        }

        public string UserFullName
        {
            get => _userFullName;
            set
            {
                _userFullName = value;
                OnPropertyChanged(nameof(UserFullName));
            }
        }

        public int UserBalance
        {
            get => _userBalance;
            set
            {
                _userBalance = value;
                OnPropertyChanged(nameof(UserBalance));
            }
        }

        public int ATMBalance
        {
            get => _atmBalance;
            set
            {
                _atmBalance = value;
                OnPropertyChanged(nameof(ATMBalance));
            }
        }

        public int UserMoneyRequest
        {
            get => _userMoneyRequest;
            set
            {
                _userMoneyRequest = Convert.ToInt32(value);
                OnPropertyChanged(nameof(UserMoneyRequest));
            }
        }
    }
}
