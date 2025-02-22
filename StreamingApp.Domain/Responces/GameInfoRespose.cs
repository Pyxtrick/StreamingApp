using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Domain.Responces;

public class GameInfoRespose
{
    public List<GameInfoDto> gameInfos { get; set; }

    public bool isSucsess { get; set; }
}
