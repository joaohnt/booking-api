using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Booking.Domain.ValueObjects;

public class Email
{
    private static readonly Regex EmailRegex =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    public string MailAdress { get; }
    internal Email(string mailAdress)
    {
        MailAdress = mailAdress;
    }
    
    public static Email Create(string mailAdress)
    {
        if (string.IsNullOrWhiteSpace(mailAdress))
            throw new ArgumentException("Email nao pode ser vazio");
        
        mailAdress = mailAdress.Trim().ToLowerInvariant();

        if (!EmailRegex.Match(mailAdress).Success)
            throw new ArgumentException("Email invalido");
        
        return new Email(mailAdress);
    }

    public override bool Equals(object? obj)
        => obj is Email other && MailAdress == other.MailAdress;
    
    public override int GetHashCode()
        => MailAdress.GetHashCode();
    
    public override string ToString()
        => MailAdress;
}