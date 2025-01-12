using HotelReservations.MVVM.Database;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.MVVM.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task AddReservationAsync(Reservation reservation);
        Task<Reservation?> GetConflicting(Reservation reservation);
    }
}
