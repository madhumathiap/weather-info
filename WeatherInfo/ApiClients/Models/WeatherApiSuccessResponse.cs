namespace WeatherInfo.ApiClients.Models;

public class Astro
{
    public required string Sunrise { get; set; }
    public required string Sunset { get; set; }
}
public class Current
{
    public double TempC { get; set; }
    public int Humidity { get; set; }
}

public class Day
{
    public required double MaxtempC { get; set; }
    public required double MintempC { get; set; }
}

public class Forecast
{
    public required List<Forecastday> Forecastday { get; set; }
}

public class Forecastday
{
    public required Day Day { get; set; }
    public required Astro Astro { get; set; }
}
public class Location
{
    public required string Name { get; set; }
}

public class WeatherApiSuccessResponse
{
    public required Location Location { get; set; }
    public required Current Current { get; set; }
    public required Forecast Forecast { get; set; }
}