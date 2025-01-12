using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Models
{
    public class Reservation
    {
        public string Username { get; }
        public RoomID RoomID { get; }
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }

        public TimeSpan Length => CheckOutDate.Subtract(CheckInDate);

        public Reservation(RoomID roomID, DateTime checkInDate, DateTime checkOutDate, string username)
        {
            RoomID = roomID;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Username = username;
        }
    }
}
