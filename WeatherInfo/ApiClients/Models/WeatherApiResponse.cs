namespace WeatherInfo.ApiClients.Models;

public class WeatherApiResponse
{
    public WeatherApiSuccessResponse? WeatherApiSuccessResponse { get; set; }
    public WeatherApiErrorResponse? WeatherApiErrorResponse { get; set; }
}
