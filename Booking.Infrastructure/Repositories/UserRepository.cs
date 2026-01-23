using Booking.Domain.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BookingDbContext  _context;

    public UserRepository(BookingDbContext context)
    {
        _context = context;
    }
    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}