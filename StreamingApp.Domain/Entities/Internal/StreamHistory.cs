using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.Domain.Entities.Internal;

public class StreamHistory
{
    public int Id { get; set; }

    [Required]
    public string StreamTitle { get; set; }

    public int GameStreamId { get; set; }
    [NotMapped]
    public GameStream? GameCategories { get; set; }
}
