using ISun.Application.Interfaces;
using ISun.ConsoleApplication.Infrastructure;
using ISun.Domain.Interfaces;

namespace ISun.ConsoleApplication;

public class ConsoleUI
{
    public static void ConsoleUIEngine()
    {
        var service = ServiceLocator.GetService<IWeatherForecastOfCityService>();
        var loggerService = ServiceLocator.GetService<ILoggerService>();
        
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Please write city name or names, just add character ',' between names");
            var clientInput = Console.ReadLine();
            loggerService?.Logger.Info("Client input value: " + clientInput);
            var citiesInputFromClient = clientInput?.Split(',');

            if (citiesInputFromClient != null)
            {
                var weatherForecastOfCities = service?.GetWeatherForecastOfCities(citiesInputFromClient);
                Console.WriteLine("Selected cities:");
                if (weatherForecastOfCities.Count() == 0)
                {
                    Console.WriteLine("Can not find weather forecast for theses cities");
                }
            
                loggerService.Logger.Info("Retrieved element count: " + weatherForecastOfCities.Count());

                foreach (var weatherForecastOfCity in weatherForecastOfCities)
                {
                    Console.WriteLine(
                        $"Weather forecast in {weatherForecastOfCity.City}. Temperature: {weatherForecastOfCity.Temperature}, Precipitation: {weatherForecastOfCity.Precipitation}, Wind speed: {weatherForecastOfCity.WindSpeed}, Summary: {weatherForecastOfCity.Summary}");
                }
            }
        }
    }
}