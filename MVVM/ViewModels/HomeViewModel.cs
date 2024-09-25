using CadwiseAutomaticTellerMachine.MVVM.Commands;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ICommand StartCommand { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StartCommand = new RelayCommand(_ => NavigationService.NavigateTo<AuthViewModel>(), _ => true);
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
