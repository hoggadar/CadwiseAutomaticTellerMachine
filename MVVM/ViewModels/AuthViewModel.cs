using CadwiseAutomaticTellerMachine.Business.Interfaces;
using CadwiseAutomaticTellerMachine.MVVM.Commands;
using CadwiseAutomaticTellerMachine.MVVM.DTOs;
using CadwiseAutomaticTellerMachine.MVVM.Navigation;
using System.Windows;
using System.Windows.Input;

namespace CadwiseAutomaticTellerMachine.MVVM.ViewModels
{

    public class AuthViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IAuthService _authService;

        private AuthDto dto;

        public ICommand AuthCommand { get; }

        public AuthViewModel(INavigationService navigationService, IAuthService authService)
        {
            _navigationService = navigationService;
            _authService = authService;
            dto = new AuthDto();
            AuthCommand = new RelayCommand(async _ => await NavigateToService(), _ => true);

            NavigationService.NavigatedTo += ClearCardData;
        }

        private void ClearCardData()
        {
            CardNumber = string.Empty;
            CardPIN = string.Empty;
        }

        public async Task NavigateToService()
        {
            if (await _authService.AuthUser(dto))
            {
                NavigationService.NavigateTo<ServiceViewModel>();
            }
            else
            {
                MessageBox.Show("Некорректный номер или pin карты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public string CardNumber
        {
            get => dto.CardNumber;
            set
            {
                dto.CardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }
        }

        public string CardPIN
        {
            get => dto.CardPIN;
            set
            {
                dto.CardPIN = value;
                OnPropertyChanged(nameof(CardPIN));
            }
        }
    }
}
