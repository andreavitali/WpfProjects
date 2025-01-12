using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.MVVM.Database
{
    public static class Mapping
    {
        public static Reservation MapToReservation(this ReservationEntity entity)
        {
            return new Reservation(new RoomID(entity.FloorNumber, entity.RoomNumber), entity.CheckInDate, entity.CheckOutDate, entity.Username);
        }

        public static ReservationEntity MapToReservationEntity(this Reservation reservation)
        {
            return new ReservationEntity()
            {
                Username = reservation.Username,
                FloorNumber = reservation.RoomID.FloorNumber,
                RoomNumber = reservation.RoomID.RoomNumber,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate
            };
        }
    }
}
