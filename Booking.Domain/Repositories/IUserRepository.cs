using Booking.Domain.DTOs;
using Booking.Domain.Entities;

namespace Booking.Domain.Repositories;

public interface IUserRepository
{
    void Add(User user);
    Task<User?> GetByEmail(string email);
    Task<List<User>> GetProviders();
}