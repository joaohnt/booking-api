using Booking.Domain.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Repositories;
using Booking.Domain.ValueObjects;
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

    public Task<User?> GetByEmail(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == Email.Create(email));
    }

    public Task<List<User>> GetProviders()
    {
        return  _context.Users.Where(u => u.Role == Role.PROVIDER == true).ToListAsync();
    }
}