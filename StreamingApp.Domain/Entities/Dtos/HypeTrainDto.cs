using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Dtos;

public class HypeTrainDto
{
    public int Ammount { get; set; }

    [Required]
    public string RaiderName { get; set; }
}
