using HotelReservations.MVVM.Database;
using HotelReservations.MVVM.Services;
using HotelReservations.MVVM.Stores;
using MVVM.Models;
using MVVM.ViewModels;
using System.Windows;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly HotelStore _hotelStore;

        public App()
        {
            ReservationService reservationService = new ReservationService();
            ReservationBook reservationBook = new ReservationBook(reservationService);
            var hotel = new Hotel("Overlook Hotel", reservationBook);
            _navigationStore = new NavigationStore();
            _hotelStore = new HotelStore(hotel);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using ReservationsDbContext reservationsDbContext = new ReservationsDbContext();
            reservationsDbContext.Database.EnsureCreated();

            _navigationStore.CurrentViewModel = CreateReservationsListingViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(
                _hotelStore, 
                new NavigationService(_navigationStore, CreateReservationsListingViewModel));
        }

        private ReservationsListingViewModel CreateReservationsListingViewModel()
        {
            return ReservationsListingViewModel.LoadViewModel(
                _hotelStore, 
                CreateMakeReservationViewModel(), 
                new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }            
    }
}
