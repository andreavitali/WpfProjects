using Microsoft.EntityFrameworkCore;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.MVVM.Database
{
    public class ReservationsDbContext : DbContext
    {
        public DbSet<ReservationEntity> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=reservoom.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReservationDTOEntityConfiguration());
        }
    }
}
