using ISun.Domain.Entities;

namespace ISun.Domain.Interfaces;

public interface IWeatherForecastOfCityRepository
{
    public IEnumerable<WeatherForecastOfCity> GetWeatherForecastOfCities(string[] names);
    public void UpdateWeatherForecastOfCities(IEnumerable<WeatherForecastOfCity> weatherForecastOfCities);
}