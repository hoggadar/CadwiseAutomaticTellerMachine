using CadwiseAutomaticTellerMachine.MVVM.ViewModels;

namespace CadwiseAutomaticTellerMachine.MVVM.Navigation
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
        event Action NavigatedTo;
    }
}
