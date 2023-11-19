using ISun.Domain.Interfaces;

namespace ISun.ConsoleApplication.Infrastructure;

public static class ExceptionHandler
{
    private static ILoggerService? _loggerService;
    public static void InitializeExceptionHandler()
    {
        System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        _loggerService = ServiceLocator.GetService<ILoggerService>();
    }
    
    static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e) {
        _loggerService?.Logger.Error(e.ExceptionObject.ToString());
        ConsoleUI.ConsoleUIEngine();
    }
}