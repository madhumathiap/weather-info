using WeatherInfo.ViewModels;

namespace WeatherInfo.Services;

public interface IWeatherForecastService
{
    public Task<WeatherViewModel> GetWeatherForecastWithHistoryAsync(string location);
}
