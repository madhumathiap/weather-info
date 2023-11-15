using Microsoft.AspNetCore.Mvc;
using WeatherInfo.Services;
using WeatherInfo.ViewModels;

namespace WeatherInfo.Controllers;
public class WeatherController : Controller
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly IWeatherSearchHistoryService _weatherSearchHistoryService;

    public WeatherController(ILogger<WeatherController> logger, IWeatherForecastService weatherForecastService, IWeatherSearchHistoryService weatherSearchHistoryService)
    {
        _logger = logger;
        _weatherForecastService = weatherForecastService;
        _weatherSearchHistoryService = weatherSearchHistoryService;
    }

    [HttpGet]
    public async Task<IActionResult> WeatherDetails()
    {
        var weatherViewModel = new WeatherViewModel
        {
            WeatherDetailsHistory = await _weatherSearchHistoryService.GetWeatherDetailsHistoryAsync()
        };

        return View(weatherViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> WeatherDetails(string location)
    {
        var result = await _weatherForecastService.GetWeatherForecastWithHistoryAsync(location);
        return View(result);
    }
}
