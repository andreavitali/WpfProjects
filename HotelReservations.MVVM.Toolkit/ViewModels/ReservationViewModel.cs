using CommunityToolkit.Mvvm.ComponentModel;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModels
{
    public class ReservationViewModel
    {
        private readonly Reservation _reservation;

        public string Username => _reservation.Username;
        public string RoomID => _reservation.RoomID.ToString();
        public string StartDate => _reservation.CheckInDate.ToShortDateString();
        public string EndDate => _reservation.CheckOutDate.ToShortDateString();

        public ReservationViewModel(Reservation reservation)
        {   
            this._reservation = reservation;
        }
    }
}
