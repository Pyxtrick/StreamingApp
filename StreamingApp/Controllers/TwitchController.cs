using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands;

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
}
