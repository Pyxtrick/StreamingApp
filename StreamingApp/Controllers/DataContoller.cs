using Microsoft.AspNetCore.Mvc;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Responces;

namespace StreamingApp.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataContoller : ControllerBase
{
    #region Commands
    [HttpGet("Commands")]
    public CommandRespose GetAllCommands([FromServices] ICRUDCommands crudCommands)
    {
        var commands = crudCommands.GetAll();

        return new CommandRespose()
        {
            CommandAndResponses = commands,
            isSucsess = commands.Any(),
        };
    }

    [HttpPost("Commands")]
    public CommandRespose UpdateCommands([FromServices] ICRUDCommands crudCommands, List<CommandAndResponseDto> commandAndResponses)
    {
        var commands = crudCommands.CreateOrUpdtateAll(commandAndResponses);

        return new CommandRespose()
        {
            CommandAndResponses = commands,
            isSucsess = commands.Any()
        };
    }

    [HttpDelete("Commands")]
    public CommandRespose DeleteCommands([FromServices] ICRUDCommands crudCommands, List<CommandAndResponseDto> commandAndResponses)
    {
        var sucsess = crudCommands.Delete(commandAndResponses);

        return new CommandRespose()
        {
            isSucsess = sucsess
        };
    }
    #endregion
    
    #region Stream
    [HttpGet("Streams")]
    public StreamRespose GetAllStreams([FromServices] ICRUDStreams crudStreams)
    {
        var streams = crudStreams.GetAll();

        return new StreamRespose()
        {
            streams = streams,
            isSucsess = streams.Any(),
        };
    }
    #endregion

    #region GameInfo
    [HttpGet("GameInfos")]
    public GameInfoRespose GetAllGameInfos([FromServices] ICRUDGameInfos crudGameInfos)
    {
        var gameInfos = crudGameInfos.GetAll();

        return new GameInfoRespose()
        {
            gameInfos = gameInfos,
            isSucsess = gameInfos.Any(),
        };
    }

    [HttpPost("GameInfos")]
    public GameInfoRespose UpdateGameInfos([FromServices] ICRUDGameInfos crudGameInfos, List<GameInfoDto> gameInfoDtos)
    {
        var gameInfos = crudGameInfos.CreateOrUpdtateAll(gameInfoDtos);

        return new GameInfoRespose()
        {
            gameInfos = gameInfos,
            isSucsess = gameInfos.Any()
        };
    }

    [HttpDelete("GameInfos")]
    public GameInfoRespose DeleteGameInfos([FromServices] ICRUDGameInfos crudGameInfos, List<GameInfoDto> gameInfoDtos)
    {
        var sucsess = crudGameInfos.Delete(gameInfoDtos);

        return new GameInfoRespose()
        {
            isSucsess = sucsess
        };
    }
    #endregion

    #region SpecialWord
    [HttpGet("SpecialWords")]
    public SpecialWordRespose GetAllspecialWords([FromServices] ICRUDSpecialWords getSpecialWords)
    {
        var specialWords = getSpecialWords.GetAll();

        return new SpecialWordRespose()
        {
            sw = specialWords,
            isSucsess = specialWords.Any(),
        };
    }

    [HttpPost("SpecialWords")]
    public SpecialWordRespose UpdatespecialWords([FromServices] ICRUDSpecialWords updateSpecialWords, List<SpecialWordDto> commandAndResponses)
    {
        var specialWords = updateSpecialWords.CreateOrUpdtateAll(commandAndResponses);

        return new SpecialWordRespose()
        {
            sw = specialWords,
            isSucsess = specialWords.Any()
        };
    }

    [HttpDelete("SpecialWords")]
    public SpecialWordRespose DeletespecialWords([FromServices] ICRUDSpecialWords deleteSpecialWords, List<SpecialWordDto> commandAndResponses)
    {
        var sucsess = deleteSpecialWords.Delete(commandAndResponses);

        return new SpecialWordRespose()
        {
            isSucsess = sucsess
        };
    }
    #endregion
}
