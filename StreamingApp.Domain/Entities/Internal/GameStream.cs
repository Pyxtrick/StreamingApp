using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.Domain.Entities.Internal;

public class GameStream
{
    public int GameCategoryId { get; set; }

    public int StreamId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? VodUrl { get; set; }

    [NotMapped]
    public GameInfo? GameCategory { get; set; }
}
