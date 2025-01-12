using HotelReservations.MVVM.Database;
using Microsoft.EntityFrameworkCore;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.MVVM.Services
{
    public class ReservationService : IReservationService
    {
        public ReservationService()
        {

        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            using ReservationsDbContext reservationsDbContext = new ReservationsDbContext();
            reservationsDbContext.Reservations.Add(reservation.MapToReservationEntity());
            await reservationsDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            using ReservationsDbContext reservationsDbContext = new ReservationsDbContext();
            IEnumerable<ReservationEntity> reservationDTOs = await reservationsDbContext.Reservations.ToListAsync();
            await Task.Delay(2000);
            return reservationDTOs.Select(rDTO => rDTO.MapToReservation());
        }

        public async Task<Reservation?> GetConflicting(Reservation reservation)
        {
            using ReservationsDbContext reservationsDbContext = new ReservationsDbContext();
            var conflictinReservation = await reservationsDbContext.Reservations
                .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber &&
                    r.RoomNumber == reservation.RoomID.RoomNumber &&
                    r.CheckInDate < reservation.CheckOutDate &&
                    r.CheckOutDate > reservation.CheckInDate)
                .FirstOrDefaultAsync();

            return conflictinReservation?.MapToReservation();
        }
    }
}
