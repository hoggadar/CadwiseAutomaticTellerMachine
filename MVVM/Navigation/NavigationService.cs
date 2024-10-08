﻿using CadwiseAutomaticTellerMachine.MVVM.ViewModels;

namespace CadwiseAutomaticTellerMachine.MVVM.Navigation
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        private ViewModelBase _currentView;
        public event Action NavigatedTo;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModelBase = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModelBase;
            NavigatedTo?.Invoke();
        }
    }
}
