using ISun.ConsoleApplication.Infrastructure;

namespace ISun.ConsoleApplication;

public static class ProgramMain
{
    public static async Task MainAsync(string[] args)
    {
        var serviceProvider = DIContainer.CreateDiContainer();
        ServiceLocator.Initialize(serviceProvider);
        ExceptionHandler.InitializeExceptionHandler();

        await ProcessToFetchingDataFromISun.StartBackgroundProcess();
        ConsoleUI.ConsoleUIEngine();
    }
}