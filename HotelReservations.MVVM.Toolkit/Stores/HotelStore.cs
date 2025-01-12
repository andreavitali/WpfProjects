using MVVM.Models;

namespace HotelReservations.MVVM.Stores
{
    public class HotelStore
    {
        private readonly Hotel _hotel;
        private readonly List<Reservation> _reservations;
        private Lazy<Task> _loadLazy;

        public IEnumerable<Reservation> Reservations => _reservations;

        public event Action<Reservation>? ReservationMade;

        public HotelStore(Hotel hotel)
        {
            _hotel = hotel;
            _reservations = new List<Reservation>();
            _loadLazy = new Lazy<Task>(Initialize);
        }

        public async Task LoadAsync()
        {
            try
            {
                await _loadLazy.Value;
            }
            catch (Exception)
            {
                _loadLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        public async Task MakeReservation(Reservation reservation)
        {
            await _hotel.MakeReservation(reservation);
            _reservations.Add(reservation);
            OnReservationMade(reservation);
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationMade?.Invoke(reservation);
        }

        private async Task Initialize()
        {
            var loadedReservations = await _hotel.GetAllReservations();
            _reservations.Clear();
            _reservations.AddRange(loadedReservations);
        }
    }
}
