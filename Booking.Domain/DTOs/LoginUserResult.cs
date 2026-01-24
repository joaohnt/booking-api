namespace Booking.Domain.DTOs;

public class LoginUserResult
{
    public LoginUserResult(string email, string token)
    {
        Email = email;
        Token = token;
    }

    public string Email { get; set; }
    public string Token { get; set; }
}