using WeatherInfo.Models;

namespace WeatherInfo.ViewModels;

public class WeatherViewModel
{
    public WeatherDetail? WeatherData { get; set; }
    public ErrorData? ErrorData { get; set; }
    public List<WeatherDetail> WeatherDetailsHistory { get; set; } = new();
}