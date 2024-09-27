using CadwiseAutomaticTellerMachine.MVVM.Commands;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToHelpCommand { get; }
        public ICommand NavigateToAboutCommand { get; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToHomeCommand = new RelayCommand(_ => NavigationService.NavigateTo<HomeViewModel>(), _ => true);
            NavigateToHelpCommand = new RelayCommand(_ => NavigationService.NavigateTo<HelpViewModel>(), _ => true);
            NavigateToAboutCommand = new RelayCommand(_ => NavigationService.NavigateTo<AboutViewModel>(), _ => true);
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
