using WeatherInfo.ApiClients.Models;

namespace WeatherInfo.ApiClients;

public interface IWeatherForecastApiClient
{
    public Task<WeatherApiResponse> GetWeatherForecastAsync(string location);
}
