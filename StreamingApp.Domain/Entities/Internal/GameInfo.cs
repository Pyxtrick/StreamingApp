using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal;

public class GameInfo
{
    public int Id { get; set; }

    [Required]
    public string Game { get; set; }

    [Required]
    public string GameId { get; set; }

    [Required]
    public string Message { get; set; }

    public GameCategoryEnum GameCategory { get; set; }
}
