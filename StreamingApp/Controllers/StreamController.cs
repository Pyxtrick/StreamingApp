using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands.Twitch.Interfaces;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreamController : ControllerBase
{
    [HttpPost("StartOrEndStream")]
    public async Task StartOrEndStream([FromServices] IManageStream updateStream)
    {
        await updateStream.StartOrEndStream();
    }

    [HttpPost("ChangeCategory")]
    public async Task ChangeCategory([FromServices] IManageStream updateStream)
    {
        await updateStream.ChangeCategory();
    }
}
