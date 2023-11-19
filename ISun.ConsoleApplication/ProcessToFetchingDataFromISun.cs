using ISun.Application.Interfaces;
using ISun.ConsoleApplication.Infrastructure;
using ISun.Infrastructure.ExternalServices.Interfaces;

namespace ISun.ConsoleApplication;

public static class ProcessToFetchingDataFromISun
{
    public static async Task StartBackgroundProcess()
    {
        var weatherForecastOfCityISunHttpClient = ServiceLocator.GetService<IWeatherForecastOfCityISunHttpClient>();

        if (weatherForecastOfCityISunHttpClient == null) return;
            
        var loggedIn = await weatherForecastOfCityISunHttpClient.LoginISunApi();

        if (loggedIn)
        {
            var timer = new System.Threading.Timer(FetchingData,null, 0, 15000);
        }
    }

    private static async void FetchingData(object? state)
    {
        var dataInsertProcessService = ServiceLocator.GetService<IDataInsertProcessService>();
        
        if (dataInsertProcessService != null)
        {
            await dataInsertProcessService.PeriodicallyBackgroundJob();
        }
    }
}