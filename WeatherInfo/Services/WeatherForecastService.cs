using WeatherInfo.ApiClients;
using WeatherInfo.ApiClients.Models;
using WeatherInfo.Models;
using WeatherInfo.ViewModels;

namespace WeatherInfo.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastApiClient _weatherForecastApiClient;
    private readonly IWeatherSearchHistoryService _weatherSearchHistoryService;

    public WeatherForecastService(IWeatherForecastApiClient weatherForecastApiClient, IWeatherSearchHistoryService weatherSearchHistoryService)
    {
        _weatherForecastApiClient = weatherForecastApiClient;
        _weatherSearchHistoryService = weatherSearchHistoryService;
    }

    public async Task<WeatherViewModel> GetWeatherForecastWithHistoryAsync(string location)
    {
        var weatherApiResponse = await _weatherForecastApiClient.GetWeatherForecastAsync(location);

        var weatherViewModel = MapToWeatherViewModel(weatherApiResponse);

        weatherViewModel.WeatherDetailsHistory = await _weatherSearchHistoryService.GetWeatherDetailsHistoryAsync();

        if (weatherViewModel.WeatherData != null)
        {
            await _weatherSearchHistoryService.SaveWeatherDetailsAsync(weatherViewModel.WeatherData);
        }

        return weatherViewModel;
    }

    private static WeatherViewModel MapToWeatherViewModel(WeatherApiResponse weatherApiResponse)
    {
        var weatherViewModel = new WeatherViewModel();
        if (weatherApiResponse.WeatherApiSuccessResponse is not null)
        {
            var response = weatherApiResponse.WeatherApiSuccessResponse;
            weatherViewModel.WeatherData = new WeatherDetail
            {
                Location = response.Location.Name,
                CurrentTemperature = response.Current.TempC,
                MinimumTemperature = response.Forecast.Forecastday.FirstOrDefault()?.Day.MintempC ?? 0,
                MaximumTemperature = response.Forecast.Forecastday.FirstOrDefault()?.Day.MaxtempC ?? 0,
                Humidity = response.Current.Humidity,
                Sunrise = response.Forecast.Forecastday.FirstOrDefault()?.Astro.Sunrise ?? "",
                Sunset = response.Forecast.Forecastday.FirstOrDefault()?.Astro.Sunset ?? "",
                CreatedAt = DateTime.Now
            };
        }
        else if (weatherApiResponse.WeatherApiErrorResponse is not null)
        {
            weatherViewModel.ErrorData = new ErrorData
            {
                Message = weatherApiResponse.WeatherApiErrorResponse.Error.Message
            };
        }

        return weatherViewModel;
    }
}
