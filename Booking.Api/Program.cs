using System.Text.Json.Serialization;
using Booking.Application.Services;
using Booking.Domain.Repositories;
using Booking.Domain.Services;
using Booking.Infrastructure.Database;
using Booking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); //converte o enum em string json
ConfigureService(builder);

var app = builder.Build();

app.MapGet("/", () => "Booking API");
app.MapControllers();

app.Run();


void ConfigureService(WebApplicationBuilder builder)
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(conn));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();
}