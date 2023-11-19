using ISun.Domain.Interfaces;
using NLog;

namespace ISun.Infrastructure.Services;

public class LoggerService : ILoggerService
{
    public ILogger Logger => LogManager.GetCurrentClassLogger();
}