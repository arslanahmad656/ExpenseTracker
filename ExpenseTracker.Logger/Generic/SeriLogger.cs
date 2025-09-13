using ExpenseTracker.Contracts.Logging;
using ExpenseTracker.Contracts.Logging.Generic;
using Serilog;

namespace ExpenseTracker.Logger.Generic;

public class SeriLogger<T> : ILoggerManager<T>
{
    private readonly ILogger logger = Log.ForContext<T>();

    public void LogDebug(string messageTemplate, params object[] propertyValues) => logger.Debug(messageTemplate, propertyValues);

    public void LogError(string messageTemplate, params object[] propertyValues) => logger.Error(messageTemplate, propertyValues);

    public void LogError(Exception exception, string messageTemplate, params object[] propertyValues) => logger.Error(exception, messageTemplate, propertyValues);

    public void LogInfo(string messageTemplate, params object[] propertyValues) => logger.Information(messageTemplate, propertyValues);

    public void LogWarn(string messageTemplate, params object[] propertyValues) => logger.Warning(messageTemplate, propertyValues);
}

