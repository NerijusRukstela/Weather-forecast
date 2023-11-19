using ISun.Infrastructure.ExternalServices.Models;

namespace ISun.Infrastructure.ExternalServices.Interfaces;

public interface IWeatherForecastOfCityISunHttpClient
{
    public Task<WeatherForecastOfCityISun> GetWeatherForecastOfCityISunApi(string city);
    public Task<IEnumerable<string>> GetCitiesISunApi();
    Task<bool> LoginISunApi();
}