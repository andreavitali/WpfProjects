using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HotelReservations.MVVM.Services;
using HotelReservations.MVVM.Stores;
using HotelReservations.MVVM.Toolkit.Messages;
using MVVM.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MVVM.ViewModels
{
    public partial class ReservationsListingViewModel : ObservableRecipient, IRecipient<ReservationMadeMessage>, IPageViewModel
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        private readonly HotelStore _hotelStore;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string _errorMessage;

        public bool HasError => !String.IsNullOrEmpty(ErrorMessage);

        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public bool HasReservations => _reservations.Any();

        [RelayCommand]
        private async Task LoadReservations()
        {
            ErrorMessage = string.Empty;
            IsLoading = true;

            try
            {
                await _hotelStore.LoadAsync();
                UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                ErrorMessage = "Failed to load reservations.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void MakeReservation()
        {
            _navigationService.NavigateTo<MakeReservationViewModel>();
        }

        public ReservationsListingViewModel(HotelStore hotelStore, INavigationService navigationService)
        {
            this._reservations = new ObservableCollection<ReservationViewModel>();
            this._hotelStore = hotelStore;
            this._navigationService = navigationService;
            LoadReservationsCommand.Execute(null);

            _reservations.CollectionChanged += Reservations_CollectionChanged;
        }

        public void Receive(ReservationMadeMessage message)
        {
            _reservations.Add(new ReservationViewModel(message));
        }

        private void Reservations_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasReservations));
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();
            foreach (var reservation in reservations)
            {
                _reservations.Add(new ReservationViewModel(reservation));
            }
        }        
    }
}
