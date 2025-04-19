using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class FileDataDto
{
    public User? user { get; set; }

    public int amount { get; set; }

    public AlertTypeEnum Type { get; set; }
}
