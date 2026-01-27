using System.Text;
using System.Text.Json.Serialization;
using Booking.Application.Services;
using Booking.Domain.Repositories;
using Booking.Domain.Services;
using Booking.Infrastructure.Database;
using Booking.Infrastructure.Repositories;
using Booking.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); //converte o enum em string json
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}) .AddJwtBearer(x =>
{
    var configuration = builder.Configuration;
    var tokenKey = Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value ?? string.Empty);
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateAudience = true,
        ValidAudience = configuration["Jwt:Audience"],
        ValidateIssuer = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(tokenKey)
    };
});
builder.Services.AddAuthorization(); 
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureService(builder);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


void ConfigureService(WebApplicationBuilder builder)
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(conn));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
    builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
    builder.Services.AddScoped<IBookingService, BookingService>();
    builder.Services.AddScoped<IBookingRepository, BookingRepository>();
}
