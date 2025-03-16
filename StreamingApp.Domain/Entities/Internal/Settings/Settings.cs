using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal.Settings;

public class Settings
{
    public int Id { get; set; }

    // Applies settings for specific platform
    public ChatOriginEnum Origin { get; set; }

    // Changes who is shown in the the "OnScreen" chat
    [Required]
    public AuthEnum AllChat { get; set; }

    // Mutes Allets in the frontend
    public bool MuteAllerts { get; set; }

    // Mute Send Messages in Chat
    public bool MuteChatMessages { get; set; }

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
