using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StreamingApp.API.SignalRHub;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Core.Commands.Twitch.Interfaces;
namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreamController : ControllerBase
{
    [HttpPost("StartStream")]
    public async Task<bool> StartStream([FromServices] IManageStream updateStream)
    {
        await updateStream.StartStream();
        return true;
    }

    [HttpPost("EndStream")]
    public async Task<bool> EndStream([FromServices] IManageStream updateStream)
    {
        await updateStream.EndStream();
        return true;
    }

    [HttpPost("ChangeCategory")]
    public async Task<bool> ChangeCategory([FromServices] IManageStream updateStream)
    {
        await updateStream.ChangeCategory();
        return true;
    }

    [HttpPost("SwitchChat")]
    public async Task<bool> ChangeChat([FromServices] IHubContext<ChatHub> clientHub, bool isVerticalChat)
    {
        await clientHub.Clients.All.SendAsync("ReceiveSwitchChat", isVerticalChat);
        return true;
    }

    [HttpPost("SwitchAdsDisplay")]
    public async Task<bool> SwitchAdsDisplay([FromServices] ICRUDSettings cRUDSettings, bool isDisableAdsDisplay)
    {
        await cRUDSettings.SwitchAdsDisplay(isDisableAdsDisplay);
        return true;
    }
}
