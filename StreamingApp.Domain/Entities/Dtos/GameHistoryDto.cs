
namespace StreamingApp.Domain.Entities.Dtos;
public class GameHistoryDto : GameInfoDto
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
