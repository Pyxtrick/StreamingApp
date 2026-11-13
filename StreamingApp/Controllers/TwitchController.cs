using Microsoft.AspNetCore.Mvc;
using StreamingApp.API.Interfaces;
using StreamingApp.Core.Commands;
using StreamingApp.Domain.Entities.APIs;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TwitchController : ControllerBase
{
    [HttpPut("StartTwichConnection")]
    public void StartTwitchRequest([FromServices] IStartTwitchApi startTwitchApi)
    {
        startTwitchApi.Execute();
    }

    [HttpGet("StreamData")]
    public async Task<ChannelInfo> GetChannelInfo([FromServices] ITwitchSendRequest twitchSendRequest)
    {
        return await twitchSendRequest.GetChannelInfo("", true);
    }
}
