using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands.Twitch.Interfaces;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StreamController : ControllerBase
{
    [HttpPost("StartOrEndStream")]
    public void StartOrEndStream([FromServices] IManageStream updateStream, string streamTitle, string categoryName)
    {
        updateStream.StartOrEndStream(streamTitle, categoryName);
    }

    [HttpPost("ChangeCategory")]
    public void ChangeCategory([FromServices] IManageStream updateStream, string categoryName)
    {
        updateStream.ChangeCategory(categoryName);
    }
}
