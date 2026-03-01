using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Test.TestBuilder;

public static class CommandAndResponseBuilder
{
    public static CommandAndResponse Create(this UnitOfWorkContext unitOfWork)
    {
        CommandAndResponse commandAndResponse = new CommandAndResponse();
        unitOfWork.Add(commandAndResponse);

        return commandAndResponse;
    }

    public static CommandAndResponse WithDefaults(this CommandAndResponse commandAndResponse, int id = 1, bool isActive = true)
    {
        commandAndResponse.Id = id;
        commandAndResponse.Command = "testCommand";
        commandAndResponse.Response = "This is a Test Command";
        commandAndResponse.Description = "Description";
        commandAndResponse.Active = isActive;
        commandAndResponse.Auth = AuthEnum.Undefined;

        return commandAndResponse;
    }

    public static CommandAndResponse WithCommandResponse(this CommandAndResponse commandAndResponse, string command, string response)
    {
        commandAndResponse.Command = command;
        commandAndResponse.Response = response;

        return commandAndResponse;
    }

    public static CommandAndResponse WithAuth(this CommandAndResponse commandAndResponse, AuthEnum auth)
    {
        commandAndResponse.Auth = auth;

        return commandAndResponse;
    }
}
