using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherInfo.ApiClients;
using WeatherInfo.Data;
using WeatherInfo.Services;

namespace WeatherInfo.Tests;

public class WeatherforecastServiceTest
{
    protected ServiceProvider serviceProvider;

    protected IServiceScope scope;
    public WeatherforecastServiceTest()
    {
        CreateServiceCollection();
        InitializeDatabase();
    }

    public void CreateServiceCollection()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<WeatherContext>(options =>
                        options.UseSqlite("Filename=WeatherDatabaseTest.db"));

        serviceCollection.AddScoped<IWeatherForecastService, WeatherForecastService>();
        serviceCollection.AddScoped<IWeatherSearchHistoryService, WeatherSearchHistoryService>();
        serviceCollection.AddScoped<IWeatherForecastApiClient, WeatherForecastApiClient>(wf =>
        {
            return new WeatherForecastApiClient(new HttpClient()
            {
                BaseAddress = new Uri("https://api.weatherapi.com")
            }, "<PROVIDE-API-KEY>");
        });

        serviceProvider = serviceCollection.BuildServiceProvider();
        scope = serviceProvider.CreateScope();
    }

    public void InitializeDatabase()
    {
        var recipeBookContext = scope.ServiceProvider.GetRequiredService<WeatherContext>();
        recipeBookContext.Database.EnsureDeleted();
        recipeBookContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task WhenValidLocationIsGiven_ThenWeatherInfoIsRetrieved()
    {
        // Arrange
        var service = serviceProvider.GetRequiredService<IWeatherForecastService>();

        // Act
        var result = await service.GetWeatherForecastWithHistoryAsync("london");

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.WeatherData);
        Assert.Null(result.ErrorData);
    }

    [Fact]
    public async Task WhenInvalidLocationIsGiven_ThenErrorIsReturned()
    {
        // Arrange
        var service = serviceProvider.GetRequiredService<IWeatherForecastService>();

        // Act
        var result = await service.GetWeatherForecastWithHistoryAsync("asdfasdf");

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.WeatherData);
        Assert.NotNull(result.ErrorData);
    }
}