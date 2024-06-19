using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands;
using StreamingApp.Core.Commands.Interfaces;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandController : ControllerBase
{
    [HttpGet]
    public CommandRespose GetAllCommands([FromServices] IStartTwitchApi startTwitchApi)
    {
        //startTwitchApi.Execute();

        return new CommandRespose()
        {
            cads = new List<CommandAndResponseDto>()
            {
                new CommandAndResponseDto()
                {
                    Id = 1,
                    Command = "command",
                    Response = "response",
                    Description = "description",
                    Active = true,
                    Auth = AuthEnum.Mod,
                    Category  = CategoryEnum.Undefined
                }
            },
            isSucsess = true
        };
    }

    [HttpPost]
    public CommandRespose UpdateCommands([FromServices] IAddDBData getTwitchDataQuery, List<CommandAndResponseDto> commandAndResponses)
    {
        //getTwitchDataQuery.Execute();

        return new CommandRespose()
        {
            cads = new List<CommandAndResponseDto>()
            {
                new CommandAndResponseDto()
                {
                    Id = 1,
                    Command = "command",
                    Response = "response",
                    Description = "description",
                    Active = true,
                    Auth = AuthEnum.Mod,
                    Category  = CategoryEnum.Undefined
                }
            },
            isSucsess = true
        };
    }
}
