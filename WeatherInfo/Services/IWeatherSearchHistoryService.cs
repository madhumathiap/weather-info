using WeatherInfo.Models;

namespace WeatherInfo.Services;

public interface IWeatherSearchHistoryService
{
    Task<List<WeatherDetail>> GetWeatherDetailsHistoryAsync();
    Task SaveWeatherDetailsAsync(WeatherDetail weatherDetail);
}
