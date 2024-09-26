using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal.Stream;

public class GameInfo
{
    public int Id { get; set; }

    public List<StreamGame> GameCategories { get; set; }

    [Required]
    public string Game { get; set; }

    public string GameId { get; set; }

    public string Message { get; set; }

    public GameCategoryEnum GameCategory { get; set; }
}
