using HotelReservations.MVVM.Database;
using HotelReservations.MVVM.Services;
using HotelReservations.MVVM.Stores;
using Microsoft.Extensions.Hosting;
using MVVM.Models;
using MVVM.ViewModels;
using System.Windows;
using HotelReservations.MVVM.Toolkit;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Navigation;
using MVVM.Views;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // DB
                    services.AddDbContext<ReservationsDbContext>();

                    // Services
                    services.AddSingleton<IReservationService, ReservationService>();

                    services.AddSingleton<NavigationService<MakeReservationViewModel>>();
                    services.AddSingleton<Func<MakeReservationViewModel>>(services => () => services.GetRequiredService<MakeReservationViewModel>());
                    services.AddSingleton<NavigationService<ReservationsListingViewModel>>();
                    services.AddSingleton<Func<ReservationsListingViewModel>>(services => () => services.GetRequiredService<ReservationsListingViewModel>());

                    // Stores
                    services.AddSingleton<HotelStore>();
                    services.AddSingleton<NavigationStore>();

                    // Models
                    services.AddTransient<ReservationBook>();
                    services.AddSingleton<Hotel>(services =>
                    {
                        var reservationBook = services.GetRequiredService<ReservationBook>();
                        return new Hotel("Overlook Hotel", reservationBook);
                    });

                    // ViewModels
                    services.AddSingleton<MainViewModel>();
                    services.AddTransient<MakeReservationViewModel>();
                    //services.AddTransient<ReservationsListingViewModel>(services => CreateReservationListingViewModel(services));
                    services.AddTransient<ReservationsListingViewModel>();

                    // Views/Windows
                    services.AddSingleton<MainWindow>(services => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                    services.AddTransient<MakeReservationView>();
                    services.AddTransient<ReservationsListingView>();
                });

            _host = builder.Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            using ReservationsDbContext reservationsDbContext = _host.Services.GetRequiredService<ReservationsDbContext>();
            reservationsDbContext.Database.EnsureCreated();

            var navigationService = _host.Services.GetRequiredService<NavigationService<ReservationsListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }

        //private static ReservationsListingViewModel CreateReservationListingViewModel(IServiceProvider services)
        //{
        //    return ReservationsListingViewModel.LoadViewModel(
        //        services.GetRequiredService<HotelStore>(),
        //        services.GetRequiredService<NavigationService<MakeReservationViewModel>>());
        //}
    }        

}
