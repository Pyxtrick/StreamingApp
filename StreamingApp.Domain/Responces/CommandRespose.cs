namespace StreamingApp.Domain.Entities.InternalDB;

public class CommandRespose
{
    public List<CommandAndResponseDto> CommandAndResponses { get; set; }

    public bool isSucsess { get; set; }
}
