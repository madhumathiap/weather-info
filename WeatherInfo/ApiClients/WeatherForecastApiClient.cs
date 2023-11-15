using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using WeatherInfo.ApiClients.Models;

namespace WeatherInfo.ApiClients;

public class WeatherForecastApiClient : IWeatherForecastApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherForecastApiClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<WeatherApiResponse> GetWeatherForecastAsync(string location)
    {
        var weatherApiResponse = new WeatherApiResponse();

        var response = await this._httpClient.GetAsync($"/v1/forecast.json?q={location}&days=1&key={_apiKey}");
        var stringResult = await response.Content.ReadAsStringAsync();

        var jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        if (response.StatusCode is HttpStatusCode.OK)
        {
            var apiSuccessResponse = JsonConvert.DeserializeObject<WeatherApiSuccessResponse>(stringResult, jsonSerializerSettings);
            weatherApiResponse.WeatherApiSuccessResponse = apiSuccessResponse;
        }
        else
        {
            var apiErrorResponse = JsonConvert.DeserializeObject<WeatherApiErrorResponse>(stringResult, jsonSerializerSettings);
            weatherApiResponse.WeatherApiErrorResponse = apiErrorResponse;
        }
        return weatherApiResponse;
    }
}
