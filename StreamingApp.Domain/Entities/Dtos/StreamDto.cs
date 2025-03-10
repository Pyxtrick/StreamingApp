namespace StreamingApp.Domain.Entities.Dtos;

public class StreamDto(
    int Id,
    string StreamTitle,
    DateTime StreamStart,
    DateTime StreamEnd,
    List<GameHistoryDto> GameHistoryDtos
    )
{
    public int Id { get; set; } = Id;

    public string StreamTitle { get; set; } = StreamTitle;

    public DateTime StreamStart { get; set; } = StreamStart;

    public DateTime StreamEnd { get; set; } = StreamEnd;

    public List<GameHistoryDto> GameHistoryDtos { get; set; } = GameHistoryDtos;
}
