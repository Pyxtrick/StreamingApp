using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class UserQueueDto(string userName, bool isActive, int queue, ChatOriginEnum origin)
    {

    public string UserName { get; set; } = userName;

    public bool IsActive { get; set; } = isActive;

    public int Queue { get; set; } = queue;

    public ChatOriginEnum Origin { get; set; } = origin;
}
