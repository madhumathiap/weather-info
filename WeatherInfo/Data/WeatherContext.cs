using Microsoft.EntityFrameworkCore;
using WeatherInfo.Models;

namespace WeatherInfo.Data;

public class WeatherContext : DbContext
{
    public DbSet<WeatherDetail> WeatherDetails { get; set; }
    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
    {
    }
}
