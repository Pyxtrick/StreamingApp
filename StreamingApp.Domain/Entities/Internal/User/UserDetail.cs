using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal.User;

public class UserDetail
{
    public int Id { get; set; }

    public User User { get; set; }

    // User Name
    [Required]
    public string UserName { get; set; }

    // User ID
    [Required]
    public string UserId { get; set; }

    // Url | defined internal
    public string? Url { get; set; }

    // Gives Autherisation for the App / Backend and Frontend
    public AppAuthEnum AppAuthEnum { get; set; }
}
