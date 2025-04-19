using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.InternalDB;

public class EntityBase
{
    [StringLength(100, MinimumLength = 1)]
    public string UpdatedBy { get; set; } = "unknown";

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
}
