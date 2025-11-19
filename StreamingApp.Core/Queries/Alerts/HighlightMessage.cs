using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Queries.Alerts.Interfaces;

namespace StreamingApp.Core.Queries.Alerts;

public class HighlightMessage : IHighlightMessage
{
    private readonly ITwitchCallCache _twitchCallCache;

    private readonly IHubContext<ChatHub> _hubContext;

    public HighlightMessage(ITwitchCallCache twitchCallCache, IHubContext<ChatHub> hubContext)
    {
        _twitchCallCache = twitchCallCache;
        _hubContext = hubContext;
    }

    public async Task Execute(string messageId)
    {
        var message = _twitchCallCache.GetSpecificMessage(messageId);

        await _hubContext.Clients.All.SendAsync("ReceiveHighlightMessage", message);
    }
}
