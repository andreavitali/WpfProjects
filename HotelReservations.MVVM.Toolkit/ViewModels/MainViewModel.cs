using HotelReservations.MVVM.Services;


namespace MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigationService NavigationService { get; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}
