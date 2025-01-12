using HotelReservations.MVVM.Services;
using MVVM.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
    public class ReservationBook
    {
        private readonly IReservationService reservationService;

        public ReservationBook(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await reservationService.GetReservationsAsync();
        }

        public async Task AddReservation(Reservation reservation)
        {
            var conflictingReservation = await reservationService.GetConflicting(reservation);

            if (conflictingReservation != null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }

            await this.reservationService.AddReservationAsync(reservation);
        }
    }
}
