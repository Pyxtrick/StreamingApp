using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Queries;
using StreamingApp.Domain.Entities.Dtos;

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

    [HttpPost]
    public void AddDBData([FromServices] IAddDBData addDBData)
    {
        addDBData.Execute();
    }

    [HttpGet]
    public List<ChatDto> GetTwitchChatData([FromServices] IGetTwitchChatData getTwitchChatData)
    {
        return getTwitchChatData.Execute();
    }
}
