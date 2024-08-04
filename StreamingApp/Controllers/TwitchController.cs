using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Commands.Interfaces;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TwitchController : ControllerBase
{
    [HttpPut]
    public void StartTwitchRequest([FromServices] IStartTwitchApi startTwitchApi)
    {
        startTwitchApi.Execute();
    }

    [HttpGet]
    public void GetTwitchChatData([FromServices] IAddDBData addDBData)
    {
        addDBData.Execute();
    }
}
