using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Database.Mapping;

public class AvailabilityMap : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        //tabela
        builder.ToTable("Availabilities");
        
        //chave primaria
        builder.HasKey(x => x.Id);
        
        //identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(); // PK IDENTITY (1,1)
        
        builder.OwnsOne(a => a.TimeRange, tr =>
        {
            tr.Property(p => p.Start).HasColumnName("Start");
            tr.Property(p => p.End).HasColumnName("End");
        });        
        
        builder.Property(x=> x.AvailabilityStatus).HasColumnName("AvailabilityStatus");
        
        //Fks
        builder.HasOne(x => x.Provider).WithMany(x => x.Availabilities).HasForeignKey(x => x.ProviderId).OnDelete(DeleteBehavior.Cascade);
    }
}