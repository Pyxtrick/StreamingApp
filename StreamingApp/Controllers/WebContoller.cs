using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Queries.Alerts;
using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WebContoller : ControllerBase
{
    [HttpPost("HighlightMessage")]
    public async void HighlightMessage([FromServices] IHighlightMessage highlightMessage, string messageId)
    {
        await highlightMessage.Execute(messageId);
    }

    [HttpPost("SendHighlightMessage")]
    public async void SendHighlightMessage([FromServices] IHubContext<ChatHub> clientHub, MessageDto chatMessage)
    {
        await clientHub.Clients.All.SendAsync("ReceiveHighlightMessage", chatMessage);
    }
}
