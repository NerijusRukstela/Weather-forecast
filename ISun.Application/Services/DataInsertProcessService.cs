using AutoMapper;
using ISun.Application.DTOs;
using ISun.Application.Interfaces;
using ISun.Application.Interfaces.IThirdPartyIntegrationServices;
using ISun.Domain.Interfaces;

namespace ISun.Application.Services;

public class DataInsertProcessService : IDataInsertProcessService
{
    private readonly IWeatherForecastOfCityFromISunService _fromISunService;
    private readonly IWeatherForecastOfCityService _weatherForecastOfCityService;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;
    
    public DataInsertProcessService(IWeatherForecastOfCityFromISunService fromISunService, 
                                    IWeatherForecastOfCityService weatherForecastOfCityService, 
                                    IMapper mapper,
                                    ILoggerService loggerService)
    {
        _fromISunService = fromISunService;
        _weatherForecastOfCityService = weatherForecastOfCityService;
        _mapper = mapper;
        _loggerService = loggerService;
    }

    public async Task PeriodicallyBackgroundJob()
    {
        _loggerService.Logger.Info("Fetching data at: "+ DateTime.Now);

        var fetchedWeatherForecast = await _fromISunService.GetWeatherForecastOfCities().ToListAsync();
        var fetchedWeatherForecastDtos = _mapper.Map<List<WeatherForecastOfCityDTO>>(fetchedWeatherForecast);        

        PrintWeatherForecast(fetchedWeatherForecastDtos);
        _weatherForecastOfCityService.UpdateWeatherForecastOfCities(fetchedWeatherForecastDtos);
    }
    
    private void PrintWeatherForecast(IEnumerable<WeatherForecastOfCityDTO> weatherForecastOfCityDtos)
    {
        Console.WriteLine(System.Environment.NewLine);
        Console.WriteLine("<---------------------------------------->");
        
        foreach (var city in weatherForecastOfCityDtos)
        {
            Console.WriteLine($"Weather forecast in {city.City}. Temperature: {city.Temperature}, Precipitation: {city.Precipitation}, Wind speed: {city.WindSpeed}, Summary: {city.Summary}");
        }

        Console.WriteLine("<---------------------------------------->");
        Console.WriteLine(System.Environment.NewLine);
    }
}