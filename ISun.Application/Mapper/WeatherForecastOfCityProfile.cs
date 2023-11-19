using AutoMapper;
using ISun.Application.DTOs;
using ISun.Domain.Entities;
using ISun.Infrastructure.ExternalServices.Models;

namespace ISun.Application.Mapper;

public class WeatherForecastOfCityProfile: Profile
{
    public WeatherForecastOfCityProfile()
    {
        CreateMap<WeatherForecastOfCity, WeatherForecastOfCityDTO>();
        CreateMap<WeatherForecastOfCityDTO, WeatherForecastOfCity>();
        CreateMap<WeatherForecastOfCityISun, WeatherForecastOfCityDTO>();
    }

}