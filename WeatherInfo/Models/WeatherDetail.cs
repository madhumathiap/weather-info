using System.ComponentModel.DataAnnotations;

namespace WeatherInfo.Models;

public class WeatherDetail
{
    [Key]
    public int Id { get; set; }
    public required string Location { get; set; }
    public double CurrentTemperature { get; set; }
    public double MinimumTemperature { get; set; }
    public double MaximumTemperature { get; set; }
    public int Humidity { get; set; }
    public required string Sunrise { get; set; }
    public required string Sunset { get; set; }
    public DateTime CreatedAt { get; set; }
}
