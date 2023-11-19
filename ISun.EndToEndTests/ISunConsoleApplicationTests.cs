using AutoMapper;
using ISun.Application.DTOs;
using ISun.Application.Services;
using ISun.Domain.Interfaces;
using Moq;
using ISun.Application.Mapper;
using ISun.Infrastructure.Data;
using ISun.Infrastructure.Repositories;
using ISun.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace ISun.EndToEndTests;

public class ISunConsoleApplicationTests
{
    private ISunMemoryDbContext _dbContext;
    private IMapper _mapper;
    
    [SetUp]
    public void Setup()
    { 
        InitializeDatabase();
        ConfigureMapper();
    }
    
    [Test]
    public void AddForecastOfCities_ValidInput_InsertsDataToDatabase()
    {
        ConfigureNLog();
        var cities = new string[1]{ "Klaipeda"};
        var mockILoggerService = new LoggerService();
        var repositoryService = new WeatherForecastOfCityRepository(_dbContext, mockILoggerService);
        var weatherForecastOfCityService = new WeatherForecastOfCityService(repositoryService, _mapper);
        var newItems = new List<WeatherForecastOfCityDTO>
        {
            new WeatherForecastOfCityDTO
                { City = "Klaipeda", WindSpeed = 2, Summary = "Sum", Precipitation = 2, Temperature = 2 },
            new WeatherForecastOfCityDTO 
                { City = "Vilnius", WindSpeed = 1, Summary = "Summ", Precipitation = 1, Temperature = 1},
        };
        
        weatherForecastOfCityService.UpdateWeatherForecastOfCities(newItems);
        var weatherForecastOfCities = weatherForecastOfCityService.GetWeatherForecastOfCities(cities);
        
        Assert.AreEqual(1, weatherForecastOfCities.Count());
        
        var weatherForecastOfCity = weatherForecastOfCities.FirstOrDefault();
        Assert.AreEqual("Klaipeda", weatherForecastOfCity.City);
        Assert.AreEqual(2, weatherForecastOfCity.WindSpeed);
        Assert.AreEqual(2, weatherForecastOfCity.Temperature);
    }
    
    [Test]
    public void GetCity_ExistElement_GetCityOfNameFromDatabase()
    {
        var cities = new string[1]{ "Klaipeda"};
        var loggerServiceMock = new Mock<ILoggerService>();
        var repositoryServiceMock = new WeatherForecastOfCityRepository(_dbContext, loggerServiceMock.Object);
        var weatherForecastOfCityService = new WeatherForecastOfCityService(repositoryServiceMock, _mapper);

        var weatherForecastOfCities = weatherForecastOfCityService.GetWeatherForecastOfCities(cities);
        
        Assert.AreEqual(1, weatherForecastOfCities.Count());

        var weatherForecastOfCity = weatherForecastOfCities.FirstOrDefault();
        Assert.AreEqual("Klaipeda",weatherForecastOfCity.City);
        Assert.AreEqual(2,weatherForecastOfCity.WindSpeed);
        Assert.AreEqual("Sum",weatherForecastOfCity.Summary);
        Assert.AreEqual(2,weatherForecastOfCity.Precipitation);
        Assert.AreEqual(2,weatherForecastOfCity.Temperature);
    }
    
    [Test]
    public void GetCities_ExistElements_GetCitiesOfNameFromDatabase()
    {
        var cities = new string[2]{ "Klaipeda", "Vilnius"};
        var loggerServiceMock = new Mock<ILoggerService>();
        var repositoryServiceMock = new WeatherForecastOfCityRepository(_dbContext, loggerServiceMock.Object);
        var weatherForecastOfCityService = new WeatherForecastOfCityService(repositoryServiceMock, _mapper);

        var weatherForecastOfCities = weatherForecastOfCityService.GetWeatherForecastOfCities(cities);

        Assert.AreEqual(2, weatherForecastOfCities.Count());
    }
    
    private void InitializeDatabase()
    {
        var options = new DbContextOptionsBuilder<ISunMemoryDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
            .Options;

        _dbContext = new ISunMemoryDbContext(options);
    }
    
    private void ConfigureMapper()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<WeatherForecastOfCityProfile>());
        _mapper = new Mapper(configuration);
    }
    
    private void ConfigureNLog()
    {
        NLog.LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().WriteToConsole();
        });
    }
}