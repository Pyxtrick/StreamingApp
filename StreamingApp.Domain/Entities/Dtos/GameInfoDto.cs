using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;
public class GameInfoDto
{
    public int Id { get; set; }

    public string Game { get; set; }

    public string Message { get; set; }

    public GameCategoryEnum GameCategory { get; set; }
}
