using StreamingApp.Domain.Entities.Dtos;

namespace StreamingApp.Domain.Responces;
public class StreamRespose
{
    public List<StreamDto> streams { get; set; }

    public bool isSucsess { get; set; }
}
