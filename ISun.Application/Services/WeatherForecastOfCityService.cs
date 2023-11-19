using AutoMapper;
using ISun.Application.DTOs;
using ISun.Application.Interfaces;
using ISun.Domain.Entities;
using ISun.Domain.Interfaces;

namespace ISun.Application.Services;

public class WeatherForecastOfCityService : IWeatherForecastOfCityService
{
    private readonly IWeatherForecastOfCityRepository _weatherWeatherForecastOfCityRepository;
    private readonly IMapper _mapper;  
    
    public WeatherForecastOfCityService(IWeatherForecastOfCityRepository weatherWeatherForecastOfCityRepository, IMapper mapper
        )
    {
        _weatherWeatherForecastOfCityRepository = weatherWeatherForecastOfCityRepository;
        _mapper = mapper;
       
    }

    public IEnumerable<WeatherForecastOfCityDTO> GetWeatherForecastOfCities(string[] names)
    {
        var weatherForecastOfCityResponse = _weatherWeatherForecastOfCityRepository.GetWeatherForecastOfCities(names);
        return _mapper.Map<IEnumerable<WeatherForecastOfCityDTO>>(weatherForecastOfCityResponse);
    }
    
    public void UpdateWeatherForecastOfCities(IEnumerable<WeatherForecastOfCityDTO> weatherForecastOfCities)
    {
        var mappedWeatherForecastOfCities = _mapper.Map<IEnumerable<WeatherForecastOfCity>>(weatherForecastOfCities); 
        _weatherWeatherForecastOfCityRepository.UpdateWeatherForecastOfCities(mappedWeatherForecastOfCities);
    }
}