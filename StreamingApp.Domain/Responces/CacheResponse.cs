using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Domain.Responces;
public class CacheResponse
{
    public List<MessageDto> messages { get; set; }
    public List<SubDto> subs { get; set; }
    public List<AlertDto> alerts { get; set; }
    public List<RaidDto> raids { get; set; }
    public List<EmoteDto> emotes { get; set; }
}
