using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelReservations.MVVM.Services;
using HotelReservations.MVVM.Stores;
using MVVM.Exceptions;
using MVVM.Models;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace MVVM.ViewModels
{
    [ObservableRecipient]
    public partial class MakeReservationViewModel : ObservableValidator, IPageViewModel
    {
        private readonly HotelStore _hotelStore;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Username is required.")]
        private string _username;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyDataErrorInfo]
        [Range(1, 20, ErrorMessage = "Floor number must be between 1 and 20")]
        private int _floorNumber;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyDataErrorInfo]
        [Range(1, 999, ErrorMessage = "Room number must be between 1 and 999")]
        private int _roomNumber;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyDataErrorInfo]
        [CustomValidation(typeof(MakeReservationViewModel), 
            nameof(ValidateStartDateBeforEndDate), 
            ErrorMessage = "Start date cannot be after end date.")]
        private DateTime _startDate = DateTime.Today;

        partial void OnStartDateChanged(DateTime oldValue, DateTime newValue)
        {
            ValidateProperty(EndDate, nameof(EndDate));
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanCreateReservation))]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        [NotifyDataErrorInfo]
        [CustomValidation(typeof(MakeReservationViewModel),
            nameof(ValidateStartDateBeforEndDate),
            ErrorMessage = "End date cannot be after start date.")]
        private DateTime _endDate = DateTime.Today.AddDays(1);

        partial void OnEndDateChanged(DateTime oldValue, DateTime newValue)
        {
            ValidateProperty(StartDate, nameof(StartDate));
        }

        public static ValidationResult ValidateStartDateBeforEndDate(string name, ValidationContext context)
        {
            MakeReservationViewModel instance = (MakeReservationViewModel)context.ObjectInstance;

            if (instance.StartDate < instance.EndDate)
            {
                return ValidationResult.Success;
            }

            return new("Start date is not before end date");
        }

        [RelayCommand(CanExecute = nameof(CanCreateReservation))]
        private async Task Submit()
        {
            Reservation reservation = new Reservation(
                new RoomID(FloorNumber, RoomNumber),
                StartDate, EndDate, Username);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }

        [RelayCommand]
        private void Cancel()
        {
            _navigationService.NavigateTo<ReservationsListingViewModel>();
        }

        public bool CanCreateReservation => !String.IsNullOrEmpty(Username) && FloorNumber > 0 && RoomNumber > 0 && !HasErrors;

        public MakeReservationViewModel(HotelStore hotelStore, INavigationService navigationService)
        {
            this._hotelStore = hotelStore;
            this._navigationService = navigationService;
            Messenger = WeakReferenceMessenger.Default;
        }
    }
}
