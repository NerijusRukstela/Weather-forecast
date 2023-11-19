using NLog;

namespace ISun.Domain.Interfaces;

public interface ILoggerService
{
    ILogger Logger { get; }
}