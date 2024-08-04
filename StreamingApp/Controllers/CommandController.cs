using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Queries.Web.Interfaces;
using StreamingApp.Domain.Entities.Internal;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandController : ControllerBase
{
    [HttpGet]
    public CommandRespose GetAllCommands([FromServices] IGetCommands getCommands)
    {
        var commands = getCommands.GetAll();

        return new CommandRespose()
        {
            cads = commands,
            isSucsess = commands.Any(),
        };
    }

    [HttpPost]
    public CommandRespose UpdateCommands([FromServices] IUpdateCommands updateCommands, List<CommandAndResponseDto> commandAndResponses)
    {
        var commands = updateCommands.CreateOrUpdtateAll(commandAndResponses);

        return new CommandRespose()
        {
            cads = commands,
            isSucsess = commands.Any()
        };
    }

    [HttpPost]
    public CommandRespose DeleteCommands([FromServices] IDeleteCommands deleteCommands, List<CommandAndResponseDto> commandAndResponses)
    {
        var sucsess = deleteCommands.Delete(commandAndResponses);

        return new CommandRespose()
        {
            isSucsess = sucsess
        };
    }
}
