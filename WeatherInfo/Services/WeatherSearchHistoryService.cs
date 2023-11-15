using Microsoft.EntityFrameworkCore;
using WeatherInfo.Data;
using WeatherInfo.Models;

namespace WeatherInfo.Services;

public class WeatherSearchHistoryService : IWeatherSearchHistoryService
{
    private readonly WeatherContext _weatherContext;
    public WeatherSearchHistoryService(WeatherContext weatherContext)
    {
        _weatherContext = weatherContext;
    }

    public async Task<List<WeatherDetail>> GetWeatherDetailsHistoryAsync()
    {
        return await _weatherContext.WeatherDetails.ToListAsync();
    }

    public async Task SaveWeatherDetailsAsync(WeatherDetail weatherDetail)
    {
        await _weatherContext.WeatherDetails.AddAsync(weatherDetail);
        await _weatherContext.SaveChangesAsync();
    }
}
