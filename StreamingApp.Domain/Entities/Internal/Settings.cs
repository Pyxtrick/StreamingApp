using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal;

public class Settings
{
    public int Id { get; set; }

    public ChatOriginEnum Origin { get; set; }

    // Changes who is shown in the the "All" chat
    [Required]
    public AuthEnum AllChat { get; set; }

    // Mutes Allets in the frontend
    public bool MuteAllerts { get; set; }

    // To activate comunity Day commands
    public bool ComunityDayActive { get; set; }

    // TODO: every x minutes: https://crontab.cronhub.io/
    // Sends a message every x minutes with out been pined | for sponsor or anouncements
    [Required]
    public string Delay { get; set; }

    // Allert Delays in Seconds
    public int AllertDelayS { get; set; }

    // How long a user will be timeouted at minimum if a banned word has been used
    public int TimeOutSeconds { get; set; }

    // Stops TTS if the same message is used X times
    public int SpamAmmount { get; set; }
}
