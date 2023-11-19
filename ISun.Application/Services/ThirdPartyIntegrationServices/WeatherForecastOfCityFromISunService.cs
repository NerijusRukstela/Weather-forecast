using ISun.Application.Interfaces.IThirdPartyIntegrationServices;
using ISun.Infrastructure.ExternalServices.Interfaces;
using ISun.Infrastructure.ExternalServices.Models;

namespace ISun.Application.Services.ThirdPartyIntegrationServices;

public class WeatherForecastOfCityFromISunService : IWeatherForecastOfCityFromISunService
{
    private readonly IWeatherForecastOfCityISunHttpClient _weatherForecastOfCityISunHttpClient;

    
    public WeatherForecastOfCityFromISunService(IWeatherForecastOfCityISunHttpClient weatherForecastOfCityISunHttpClient)
    {
        _weatherForecastOfCityISunHttpClient = weatherForecastOfCityISunHttpClient;
    }
    
    public async IAsyncEnumerable<WeatherForecastOfCityISun> GetWeatherForecastOfCities()
    {
        var cities = await _weatherForecastOfCityISunHttpClient.GetCitiesISunApi();
        
        foreach (var city in cities)
        {
            yield return await _weatherForecastOfCityISunHttpClient.GetWeatherForecastOfCityISunApi(city);
        }
    }
}