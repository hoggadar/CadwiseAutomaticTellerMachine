using CadwiseAutomaticTellerMachine.Business.DTOs;
using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Commands;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{
    public class WithdrawMoneyViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IAuthService _authService;
        private IStorageService _storageService;

        private ObservableCollection<BanknoteQuantityDto> _banknoteQuantity;
        private string _userFullName;
        private int _userBalance;
        private int _atmBalance;
        private string _moneyRequest;
        private bool _isSmallBills;
        private List<BanknoteQuantityDto> _result;

        public ICommand NavigateToServiceCommand { get; }
        public ICommand WithdrawMoneyCommand { get; }

        public WithdrawMoneyViewModel(INavigationService navigationService, IAuthService authService, IStorageService storageService)
        {
            _navigationService = navigationService;
            _authService = authService;
            _storageService = storageService;

            _banknoteQuantity = new ObservableCollection<BanknoteQuantityDto>();
            _userFullName = string.Empty;
            _userBalance = 0;
            _atmBalance = 0;
            _moneyRequest = "0";
            _isSmallBills = false;
            _result = new List<BanknoteQuantityDto>();

            NavigateToServiceCommand = new RelayCommand(_ => NavigationService.NavigateTo<ServiceViewModel>(), _ => true);
            WithdrawMoneyCommand = new RelayCommand(_ => WithdrawMoney(), _ => true);

            NavigationService.NavigatedTo += LoadData;
        }

        private async void LoadData()
        {
            var currentUser = _authService.CurrentUser;
            var card = _authService.CurrentCard;
            var banknoteQuantity = await _storageService.GetBanknoteQuantity();

            UserFullName = $"{currentUser!.Name} {currentUser!.Surname}";
            UserBalance = card!.Cash;
            ATMBalance = await _storageService.GetStorageBalance();
            MoneyRequest = "0";
            _banknoteQuantity.Clear();
            foreach(var item in banknoteQuantity) _banknoteQuantity.Add(item);
        }

        private async void WithdrawMoney()
        {
            try
            {
                int money = int.Parse(_moneyRequest);
                if (money % 10 != 0) throw new ArgumentException("Сумма должна быть кратна 10");

                if (_isSmallBills) _result = await _storageService.WithdrawMoneySmall(money);
                else _result = await _storageService.WithdrawMoneyBig(money);

                if (_result != null && _result.Count > 0)
                {
                    var message = "Выданные купюры:\n";
                    foreach (var banknote in _result)
                        message += $"Номинал: {banknote.Denomination} - Количество: {banknote.Quantity}\n";

                    LoadData();
                    MessageBox.Show(message, "Выдача денег", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Не удалось выдать деньги. Проверьте сумму или наличие купюр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Указана некорректная сумма", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ObservableCollection<BanknoteQuantityDto> BanknoteQuantity
        {
            get => _banknoteQuantity;
            set
            {
                _banknoteQuantity = value;
                OnPropertyChanged(nameof(BanknoteQuantity));
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

        public string MoneyRequest
        {
            get => _moneyRequest;
            set
            {
                _moneyRequest = value;
                OnPropertyChanged(nameof(MoneyRequest));
            }
        }

        public bool IsSmallBills
        {
            get => _isSmallBills;
            set
            {
                _isSmallBills = value;
                OnPropertyChanged(nameof(IsSmallBills));
            }
        }
    }
}
