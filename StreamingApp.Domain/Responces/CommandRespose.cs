namespace StreamingApp.Domain.Entities.Internal;

public class CommandRespose
{
    public List<CommandAndResponseDto> cads { get; set; }

    public bool isSucsess { get; set; }
}
