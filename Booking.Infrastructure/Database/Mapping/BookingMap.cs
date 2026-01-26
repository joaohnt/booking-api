using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.Entities;
namespace Booking.Infrastructure.Database.Mapping;

public class BookingMap : IEntityTypeConfiguration<Domain.Entities.Booking>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Booking> builder)
    {
        //tabela
        builder.ToTable("Bookings");
        
        //chave primaria
        builder.HasKey(x => x.Id);
        
        //identity
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("CreatedAt");
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(); // PK IDENTITY (1,1)
        
        builder.Property(x=> x.CanceledAt).HasColumnName("CanceledAt");
        builder.Property(x => x.CancellationReason).HasColumnName("CancellationReason");
        
        //Fks
        builder.HasOne(x => x.Client).WithMany(x => x.Bookings).HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x=>x.Availability).WithMany().HasForeignKey(x => x.AvailabilityId).OnDelete(DeleteBehavior.Cascade);
    }
}