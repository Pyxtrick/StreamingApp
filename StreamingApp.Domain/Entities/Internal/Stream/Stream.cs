using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.Domain.Entities.Internal.Stream;

/// <summary>
/// tracks a streams
/// </summary>
public class Stream
{
    public int Id { get; set; }

    [Required]
    public string StreamTitle { get; set; }

    public List<StreamGame> GameCategories { get; set; }

    public List<StreamHighlight> StreamHighlights { get; set; }

    public string? VodUrl { get; set; }

    public DateTime StreamStart { get; set; }

    public DateTime? StreamEnd { get; set; }
}
