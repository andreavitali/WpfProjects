using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservations.MVVM.Database;

public class ReservationEntity
{
    public Guid Id { get; set; }
    public int FloorNumber { get; set; }
    public int RoomNumber { get; set; }
    public string Username { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
};

public class ReservationDTOEntityConfiguration : IEntityTypeConfiguration<ReservationEntity>
{
    public void Configure(EntityTypeBuilder<ReservationEntity> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Username).IsRequired();
        builder.Property(r => r.FloorNumber).IsRequired();
        builder.Property(r => r.RoomNumber).IsRequired();
        builder.Property(r => r.CheckInDate).IsRequired();
        builder.Property(r => r.CheckOutDate).IsRequired();        
    }
}