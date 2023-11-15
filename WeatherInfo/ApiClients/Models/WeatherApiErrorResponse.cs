namespace WeatherInfo.ApiClients.Models;

public class WeatherApiErrorResponse
{
    public required Error Error { get; set; }
}
public class Error
{
    public int Code { get; set; }
    public required string Message { get; set; }
}
