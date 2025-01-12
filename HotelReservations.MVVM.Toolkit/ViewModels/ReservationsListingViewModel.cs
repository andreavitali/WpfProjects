using HotelReservations.MVVM.Commands;
using HotelReservations.MVVM.Services;
using HotelReservations.MVVM.Stores;
using MVVM.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace MVVM.ViewModels
{
    public class ReservationsListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        private readonly HotelStore _hotelStore;
        private readonly INavigationService _navigationService;

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError => !String.IsNullOrEmpty(_errorMessage);

        public ICommand MakeReservationCommand { get; }
        public ICommand LoadReservationsCommand { get; }

        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        public bool HasReservations => _reservations.Any();

        public ReservationsListingViewModel(HotelStore hotelStore, INavigationService navigationService)
        {
            this._reservations = new ObservableCollection<ReservationViewModel>();
            this._hotelStore = hotelStore;
            this._navigationService = navigationService;
            MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(this._navigationService);
            LoadReservationsCommand = new LoadReservationsCommand(this, hotelStore);
            LoadReservationsCommand.Execute(null);

            _hotelStore.ReservationMade += HotelStore_ReservationMade;
            _reservations.CollectionChanged += Reservations_CollectionChanged;
        }

        private void Reservations_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasReservations));
        }

        private void HotelStore_ReservationMade(Reservation obj)
        {
            _reservations.Add(new ReservationViewModel(obj));
        }

        //public static ReservationsListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService<MakeReservationViewModel> makeReservationNavigationService)
        //{
        //    ReservationsListingViewModel viewModel = new ReservationsListingViewModel(hotelStore, makeReservationNavigationService);
        //    viewModel.LoadReservationsCommand.Execute(null);
        //    return viewModel;
        //}

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();
            foreach (var reservation in reservations)
            {
                _reservations.Add(new ReservationViewModel(reservation));
            } 
        }

        public override void Dispose()
        {
            _hotelStore.ReservationMade -= HotelStore_ReservationMade;
            base.Dispose();
        }
    }
}
