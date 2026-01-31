namespace Booking.Application.DTOs;

public class LoginUserResponse
{
    public LoginUserResponse(string email, string token)
    {
        Email = email;
        Token = token;
    }

    public string Email { get; set; }
    public string Token { get; set; }
}