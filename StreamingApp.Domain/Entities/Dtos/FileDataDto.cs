using StreamingApp.Domain.Entities.Internal;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class FileDataDto
{
    public User? user { get; set; }

    public int amount { get; set; }

    public TypeEnum Type { get; set; }
}
