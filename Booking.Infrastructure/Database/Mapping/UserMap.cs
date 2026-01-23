using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Database.Mapping;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        //tabela
        builder.ToTable("Users");
        
        //chave primaria
        builder.HasKey(x => x.Id);
        
        //identity
        builder.Property(x => x.Name).IsRequired().HasColumnName("Name")
            .HasMaxLength(80);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(); // PK IDENTITY (1,1)
        
        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasMaxLength(160)
            .HasConversion(
                email => email.MailAdress,
                value => Email.Create(value)
            );
        
        builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash").HasMaxLength(255);
        
        builder.Property(x => x.Role).HasColumnName("Role").IsRequired().HasMaxLength(50);
        
        //Fks
        builder.HasMany(u => u.Availabilities).WithOne(a => a.Provider).HasForeignKey(a => a.ProviderId);
    }
}