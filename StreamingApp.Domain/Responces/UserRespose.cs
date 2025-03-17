using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Domain.Responces;
public class UserRespose
{
    public List<UserDto> users { get; set; }

    public bool isSucsess { get; set; }
}
