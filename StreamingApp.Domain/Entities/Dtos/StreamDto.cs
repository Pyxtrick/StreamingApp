namespace StreamingApp.Domain.Entities.Dtos;

public class StreamDto
{
    public int Id { get; set; }

    public string StreamTitle { get; set; }

    public DateTime StreamStart { get; set; }

    public DateTime StreamEnd { get; set; }

    public List<GameHistoryDto> GameHistoryDtos { get; set; }
}
