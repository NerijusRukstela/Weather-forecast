using ISun.Domain.Entities;
using ISun.Domain.Interfaces;
using ISun.Infrastructure.Data;

namespace ISun.Infrastructure.Repositories;

public class WeatherForecastOfCityRepository : IWeatherForecastOfCityRepository
{
    private readonly ISunMemoryDbContext _sunMemoryDbContext;
    private readonly ILoggerService _loggerService;
    
    public WeatherForecastOfCityRepository(ISunMemoryDbContext sunMemoryDbContext, ILoggerService loggerService)
    {
        _sunMemoryDbContext = sunMemoryDbContext;
        _loggerService = loggerService;
    }

    public IEnumerable<WeatherForecastOfCity> GetWeatherForecastOfCities(string[] searchTerms)
    {
        return _sunMemoryDbContext.Cities
                .Where(x => searchTerms.Contains(x.City, StringComparer.CurrentCultureIgnoreCase)); 
    }

    public void UpdateWeatherForecastOfCities(IEnumerable<WeatherForecastOfCity> weatherForecastOfCities)
    {
        _loggerService.Logger.Info("Starting update forecast, elements count: " + weatherForecastOfCities.Count());
        if (!_sunMemoryDbContext.Cities.Any())
        {
            _sunMemoryDbContext.Cities.AddRange(weatherForecastOfCities);
        }
        else
        {
            foreach (var item in weatherForecastOfCities)
            {
                var entity = _sunMemoryDbContext.Cities
                    .FirstOrDefault(x => x.City == item.City);
                
                entity.WindSpeed = item.WindSpeed;
                entity.Precipitation = item.Precipitation;
                entity.Temperature = item.Temperature;
                entity.Summary = item.Summary;
            }
        }
        
        _sunMemoryDbContext.SaveChanges();
        
        _loggerService.Logger.Info("Updated weather forecast, elements in DB count: " + _sunMemoryDbContext.Cities.Count());
    }
}