using CadwiseAutomaticTellerMachine.MVVM.Commands;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ICommand GetBalanceCommand { get; }
        public ICommand WithdrawMoneyCommand { get; }

        public ServiceViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GetBalanceCommand = new RelayCommand(_ => NavigationService.NavigateTo<BalanceViewModel>(), _ => true);
            WithdrawMoneyCommand = new RelayCommand(_ => NavigationService.NavigateTo<WithdrawMoneyViewModel>(), _ => true);
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
    }
}
