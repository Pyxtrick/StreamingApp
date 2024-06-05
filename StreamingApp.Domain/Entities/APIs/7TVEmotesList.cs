using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.APIs;

public class _7TVEmoteList
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string name { get; set; }

    public List<_7TVEmote>? emotes { get; set; }
}
