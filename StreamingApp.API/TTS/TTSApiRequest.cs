using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.Domain.Entities;

namespace StreamingApp.API.TTS;

public class TTSApiRequest : ITTSApiRequest
{
    private readonly IHubContext<ClientHub> _hubContext;

    public TTSApiRequest(IHubContext<ClientHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessage(TTSData ttsData)
    {
        await _hubContext.Clients.All.SendAsync("TTSDectalk", ttsData.Message);
    }
}
