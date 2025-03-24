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
    public async Task<CommandRespose> GetAllCommands([FromServices] ICRUDCommands crudCommands)
    {
        var commands = await crudCommands.GetAll();

        return new CommandRespose()
        {
            CommandAndResponses = commands,
            isSucsess = commands.Any(),
        };
    }

    [HttpPost("Commands")]
    public async Task<CommandRespose> UpdateCommands([FromServices] ICRUDCommands crudCommands, List<CommandAndResponseDto> commandAndResponses)
    {
        var commands = await crudCommands.CreateOrUpdtateAll(commandAndResponses);

        return new CommandRespose()
        {
            CommandAndResponses = commands,
            isSucsess = commands.Any()
        };
    }

    [HttpDelete("Commands")]
    public async Task<CommandRespose> DeleteCommands([FromServices] ICRUDCommands crudCommands, List<CommandAndResponseDto> commandAndResponses)
    {
        var sucsess = await crudCommands.Delete(commandAndResponses);

        return new CommandRespose()
        {
            isSucsess = sucsess
        };
    }
    #endregion
    
    #region GameInfo
    [HttpGet("GameInfos")]
    public async Task<GameInfoRespose> GetAllGameInfos([FromServices] ICRUDGameInfos crudGameInfos)
    {
        var gameInfos = await crudGameInfos.GetAll();

        return new GameInfoRespose()
        {
            gameInfos = gameInfos,
            isSucsess = gameInfos.Any(),
        };
    }

    [HttpPost("GameInfos")]
    public async Task<GameInfoRespose> UpdateGameInfos([FromServices] ICRUDGameInfos crudGameInfos, List<GameInfoDto> gameInfoDtos)
    {
        var gameInfos = await crudGameInfos.CreateOrUpdtateAll(gameInfoDtos);

        return new GameInfoRespose()
        {
            gameInfos = gameInfos,
            isSucsess = gameInfos.Any()
        };
    }

    [HttpDelete("GameInfos")]
    public async Task<GameInfoRespose> DeleteGameInfos([FromServices] ICRUDGameInfos crudGameInfos, List<GameInfoDto> gameInfoDtos)
    {
        var sucsess = await crudGameInfos.Delete(gameInfoDtos);

        return new GameInfoRespose()
        {
            isSucsess = sucsess
        };
    }
    #endregion

    #region Settings
    [HttpGet("Settings")]
    public async Task<SettingsRespose> GetAllSettings([FromServices] ICRUDSettings crudSettings)
    {
        var settings = await crudSettings.GetAll();

        return new SettingsRespose()
        {
            Settings = settings,
            isSucsess = settings.Any(),
        };
    }

    [HttpPost("Settings")]
    public async Task<SettingsRespose> UpdateGameInfos([FromServices] ICRUDSettings crudSettings, SettingsDto settingsDtos)
    {
        var gameInfos = await crudSettings.Update(settingsDtos);

        return new SettingsRespose()
        {
            isSucsess = gameInfos
        };
    }
    #endregion

    #region SpecialWord
    [HttpGet("SpecialWords")]
    public async Task<SpecialWordRespose> GetAllspecialWords([FromServices] ICRUDSpecialWords getSpecialWords)
    {
        var specialWords = await getSpecialWords.GetAll();

        return new SpecialWordRespose()
        {
            sw = specialWords,
            isSucsess = specialWords.Any(),
        };
    }

    [HttpPost("SpecialWords")]
    public async Task<SpecialWordRespose> UpdatespecialWords([FromServices] ICRUDSpecialWords updateSpecialWords, List<SpecialWordDto> commandAndResponses)
    {
        var specialWords = await updateSpecialWords.CreateOrUpdtateAll(commandAndResponses);

        return new SpecialWordRespose()
        {
            sw = specialWords,
            isSucsess = specialWords.Any()
        };
    }

    [HttpDelete("SpecialWords")]
    public async Task<SpecialWordRespose> DeletespecialWords([FromServices] ICRUDSpecialWords deleteSpecialWords, List<SpecialWordDto> commandAndResponses)
    {
        var sucsess = await deleteSpecialWords.Delete(commandAndResponses);

        return new SpecialWordRespose()
        {
            isSucsess = sucsess
        };
    }
    #endregion

    #region Stream
    [HttpGet("Streams")]
    public async Task<StreamRespose> GetAllStreams([FromServices] ICRUDStreams crudStreams)
    {
        var streams = await crudStreams.GetAll();

        return new StreamRespose()
        {
            streams = streams,
            isSucsess = streams.Any(),
        };
    }
    #endregion

    #region Users
    [HttpGet("Users")]
    public async Task<UserRespose> GetAllUsers([FromServices] ICRUDUsers crudUsers)
    {
        var users = await crudUsers.GetAll();

        return new UserRespose()
        {
            users = users,
            isSucsess = users.Any(),
        };
    }
    #endregion
}
