using ISun.Infrastructure.ExternalServices.Models;

namespace ISun.Application.Interfaces.IThirdPartyIntegrationServices;

public interface IWeatherForecastOfCityFromISunService
{
    public IAsyncEnumerable<WeatherForecastOfCityISun> GetWeatherForecastOfCities();
}