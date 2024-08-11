using Microsoft.Extensions.Logging;

namespace StreamingApp.Domain.Utility.Logging;

public static class StructuredLogger
{
    public static void LogException(this ILogger logger, LogLevel logLevel, string className, string message, Exception exception)
    {
        ArgumentValidator.EnsureNotNull(exception, nameof(exception));

        Action<ILogger, Exception> loggingException = LoggerMessage.Define(logLevel, GetEventId(className), $"{message}\nException {nameof(exception)} occurred: ");
        loggingException(logger, exception);
    }

    public static void LogMessage(this ILogger logger, LogLevel logLevel, string className, string message)
    {
        Action<ILogger, Exception> loggingMessage = LoggerMessage.Define(logLevel, GetEventId(className), message);
        loggingMessage(logger, null);
    }

    private static EventId GetEventId(string className)
    {
        int id = (from variable in LoggingConstants.LoggerEventId where variable.Value.Equals(className, StringComparison.OrdinalIgnoreCase) select variable.Key).FirstOrDefault();

        return new EventId(id, className);
    }
}
