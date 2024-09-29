using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.Business.Services;
using CadwiseAutomaticTellerMachine.MVVM.Commands;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IAuthService _authService;

        private string _userFullName;

        public ICommand GetBalanceCommand { get; }
        public ICommand WithdrawMoneyCommand { get; }
        public ICommand NavigateToAuthCommand { get; }


        public ServiceViewModel(INavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;

            var currentUser = _authService.CurrentUser;
            _userFullName = $"{currentUser!.Name} {currentUser!.Surname}";

            GetBalanceCommand = new RelayCommand(_ => NavigationService.NavigateTo<BalanceViewModel>(), _ => true);
            WithdrawMoneyCommand = new RelayCommand(_ => NavigationService.NavigateTo<WithdrawMoneyViewModel>(), _ => true);
            NavigateToAuthCommand = new RelayCommand(_ => NavigationService.NavigateTo<AuthViewModel>(), _ => true);

            NavigationService.NavigatedTo += LoadUserData;
        }

        private void LoadUserData()
        {
            var currentUser = _authService.CurrentUser;
            UserFullName = $"{currentUser!.Name} {currentUser!.Surname}";
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
    }
}
