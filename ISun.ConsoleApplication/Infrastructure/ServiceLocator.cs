using Microsoft.Extensions.DependencyInjection;

namespace ISun.ConsoleApplication.Infrastructure;

public static class ServiceLocator
{
    private static IServiceProvider _serviceProvider;
    
    public static void Initialize(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static T? GetService<T>()
    {
        return _serviceProvider.GetService<T>();
    }
}