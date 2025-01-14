using CommunityToolkit.Mvvm.ComponentModel;
using HotelReservations.MVVM.Services;


namespace MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public INavigationService NavigationService { get; }

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}
