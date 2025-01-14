using CommunityToolkit.Mvvm.Messaging.Messages;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.MVVM.Toolkit.Messages
{
    public class ReservationMadeMessage : RequestMessage<Reservation>
    {
        private Reservation reservation;

        public ReservationMadeMessage(Reservation reservation)
        {
            this.reservation = reservation;
        }
    }
}
