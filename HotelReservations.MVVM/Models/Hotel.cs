using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
    public class Hotel
    {
        private readonly ReservationBook _reservationBook;

        public string Name { get; }

        public Hotel(string name, ReservationBook reservationBook)
        {
            _reservationBook = reservationBook;
            Name = name;
        }

        //public async IEnumerable<Reservation> GetReservationsForUser(string username)
        //{
        //    return await _reservationBook.GetReservationsForUser(username);
        //}

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationBook.GetAllReservations();
        }

        public async Task MakeReservation(Reservation reservation)
        {
            await _reservationBook.AddReservation(reservation);
        }
    }
}
