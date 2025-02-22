namespace StreamingApp.Domain.Entities.Internal;

public class CommandRespose
{
    public List<CommandAndResponseDto> CommandAndResponses { get; set; }

    public bool isSucsess { get; set; }
}
