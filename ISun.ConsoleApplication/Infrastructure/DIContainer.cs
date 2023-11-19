using ISun.Application.Interfaces;
using ISun.Application.Interfaces.IThirdPartyIntegrationServices;
using ISun.Application.Mapper;
using ISun.Application.Services;
using ISun.Application.Services.ThirdPartyIntegrationServices;
using ISun.Domain.Interfaces;
using ISun.Infrastructure.Data;
using ISun.Infrastructure.ExternalServices.Interfaces;
using ISun.Infrastructure.ExternalServices.Services;
using ISun.Infrastructure.Repositories;
using ISun.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;

namespace ISun.ConsoleApplication.Infrastructure;

public static class DIContainer
{
    public static ServiceProvider CreateDiContainer()
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IWeatherForecastOfCityRepository, WeatherForecastOfCityRepository>()
            .AddScoped<IWeatherForecastOfCityService, WeatherForecastOfCityService>()
            .AddScoped<IWeatherForecastOfCityISunHttpClient, WeatherForecastOfCityISunHttpClient>()
            .AddScoped<IWeatherForecastOfCityFromISunService, WeatherForecastOfCityFromISunService>()
            .AddScoped<IDataInsertProcessService, DataInsertProcessService>()
            .AddSingleton<ILoggerService, LoggerService>()
            .AddHttpClient()
            .AddAutoMapper(typeof(WeatherForecastOfCityProfile))
            .AddDbContext<ISunMemoryDbContext>(options =>
            {
                options.UseInMemoryDatabase("ISunInMemoryDatabase");
            })
            .BuildServiceProvider();
        
        NLog.LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().WriteToConsole();
        });
        
        return serviceProvider;
    }

}