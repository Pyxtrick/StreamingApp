using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.APIs;

public class _7TVEmote
{

    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }
}
