using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal;

public class UserDetail
{
    public int Id { get; set; }

    // User Name
    [Required]
    public string UserName { get; set; }

    // User ID
    [Required]
    public string UserId { get; set; }

    // Url | defined internal
    public string? Url { get; set; }
}
