using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.InternalDB.Trigger;

public class SpecialWords : EntityBase
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Comment { get; set; }

    public SpecialWordEnum Type { get; set; }

    public int TimesUsed { get; set; }
}
