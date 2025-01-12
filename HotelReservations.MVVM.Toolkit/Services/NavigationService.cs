using CommunityToolkit.Mvvm.ComponentModel;
using MVVM.ViewModels;

namespace HotelReservations.MVVM.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private ViewModelBase _currentViewModel;
        private readonly Func<Type, ViewModelBase> _viewModelResolver;

        public ViewModelBase CurrentViewModel 
        {
            get => _currentViewModel;
            private set 
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelResolver)
        {
            _viewModelResolver = viewModelResolver;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = _viewModelResolver(typeof(TViewModel));
            CurrentViewModel = viewModel;
        }
    }
}
