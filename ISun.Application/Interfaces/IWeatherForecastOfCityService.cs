using ISun.Application.DTOs;

namespace ISun.Application.Interfaces;

public interface IWeatherForecastOfCityService
{
    public IEnumerable<WeatherForecastOfCityDTO> GetWeatherForecastOfCities(string[] names);
    public void UpdateWeatherForecastOfCities(IEnumerable<WeatherForecastOfCityDTO> weatherForecastOfCities);
}