using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Commands;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{
    public class BalanceViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IAuthService _authService;

        private string _userFullName;
        private int _userBalance;

        public ICommand NavigateToServiceCommand { get; }

        public BalanceViewModel(INavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;

            _userFullName = string.Empty;
            _userBalance = 0;

            NavigateToServiceCommand = new RelayCommand(_ => NavigationService.NavigateTo<ServiceViewModel>(), _ => true);
            NavigationService.NavigatedTo += LoadUserData;
        }

        private void LoadUserData()
        {
            var currentUser = _authService.CurrentUser;
            var card = _authService.CurrentCard;

            UserFullName = $"{currentUser!.Name} {currentUser!.Surname}";
            UserBalance = card!.Cash;
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
    }
}
