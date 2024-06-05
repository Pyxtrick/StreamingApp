using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.APIs;

public class BetterTVUser
{
    [Required]
    public string id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Displayname { get; set; }

    [Required]
    public string ProviderId { get; set; }

    public BetterTVBage? badge { get; set; }
}
