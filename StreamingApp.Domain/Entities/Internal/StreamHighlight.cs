using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.Domain.Entities.Internal;

public class StreamHighlight
{
    public int Id { get; set; }

    public int StreamHistoryId { get; set; }
    [NotMapped]
    public StreamHistory StreamHistory { get; set; }

    public string Description { get; set; }

    public DateTime TimeOfDay { get; set; }
    
    public TimeSpan StreamTime { get; set; }
}
