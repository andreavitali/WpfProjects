using CommunityToolkit.Mvvm.ComponentModel;
using MVVM.ViewModels;

namespace HotelReservations.MVVM.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private IPageViewModel _currentViewModel;
        private readonly Func<Type, IPageViewModel> _viewModelResolver;

        public IPageViewModel CurrentViewModel 
        {
            get => _currentViewModel;
            private set 
            {
                if(_currentViewModel is not null)
                {
                    _currentViewModel.IsActive = false;
                }

                _currentViewModel = value;

                if (_currentViewModel is not null)
                {
                    _currentViewModel.IsActive = true;
                }

                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, IPageViewModel> viewModelResolver)
        {
            _viewModelResolver = viewModelResolver;
        }

        public void NavigateTo<TViewModel>() where TViewModel : IPageViewModel
        {
            var viewModel = _viewModelResolver(typeof(TViewModel));
            CurrentViewModel = viewModel;
        }
    }
}
