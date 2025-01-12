using HotelReservations.MVVM.Services;
using HotelReservations.MVVM.Stores;
using MVVM.Exceptions;
using MVVM.Models;
using MVVM.ViewModels;
using System.Windows;

namespace HotelReservations.MVVM.Commands
{
    public class MakeReservationCommand : AsyncCommandBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly HotelStore _hotelStore;
        private readonly INavigationService _navigationService;

        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, HotelStore hotelStore, INavigationService navigationService)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _hotelStore = hotelStore;
            _navigationService = navigationService;
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(MakeReservationViewModel.Username) ||
               e.PropertyName == nameof(MakeReservationViewModel.FloorNumber) ||
               e.PropertyName == nameof(MakeReservationViewModel.RoomNumber))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return
                !String.IsNullOrEmpty(_makeReservationViewModel.Username) &&
                _makeReservationViewModel.FloorNumber > 0 &&
                _makeReservationViewModel.RoomNumber > 0 &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Reservation reservation = new Reservation(
                new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
                _makeReservationViewModel.StartDate,
                _makeReservationViewModel.EndDate,
                _makeReservationViewModel.Username);

            try
            {
                await _hotelStore.MakeReservation(reservation);
                MessageBox.Show("Reservation successful.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _navigationService.NavigateTo<ReservationsListingViewModel>();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("This room is already taken for the selected dates.", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }
    }
}
