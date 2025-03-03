using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands.Twitch.Interfaces;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreamController : ControllerBase
{
    [HttpPost("StartStream")]
    public async Task StartStream([FromServices] IManageStream updateStream)
    {
        await updateStream.StartStream();
    }

    [HttpPost("EndStream")]
    public async Task EndStream([FromServices] IManageStream updateStream)
    {
        await updateStream.EndStream();
    }

    [HttpPost("ChangeCategory")]
    public async Task ChangeCategory([FromServices] IManageStream updateStream)
    {
        await updateStream.ChangeCategory();
    }
}
