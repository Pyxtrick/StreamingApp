using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.APIs;

public class BetterTVEmotes
{
    public int Id { get; set; }

    [Required]
    public string code { get; set; }

    [Required]
    public string imageType { get; set; }

    public bool animated { get; set; }

    [Required]
    public string userId { get; set; }
}
