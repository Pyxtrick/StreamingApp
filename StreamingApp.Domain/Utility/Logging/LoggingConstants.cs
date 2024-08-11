namespace StreamingApp.Domain.Utility.Logging;

public static class LoggingConstants
{
    public static readonly Dictionary<int, string> LoggerEventId = new()
    {
        { 0, "Twitch" },
        { 1, "Youtube" },
        { 2, "API" },
        { 3, "DB" },
        { 4, "SignalR" },
        { 5, "Internal" },
    };
}
