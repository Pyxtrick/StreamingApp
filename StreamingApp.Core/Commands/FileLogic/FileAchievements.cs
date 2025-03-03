
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.FileLogic;

public class FileAchievements : IFileAchievements
{
    private readonly IManageFile _manageFile;

    public FileAchievements(IManageFile manageFile)
    {
        _manageFile = manageFile;
    }

    /// <summary>
    /// Twitch       UserX   gifted Subs    5
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="user"></param>
    /// <param name="what"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public async Task Execute(ChatOriginEnum platform, string user, string what, int count, string contence = "Achievements")
    {
        List<string> lines = _manageFile.ReadFile(contence);

        // file Logic
        // Platform     Who     What            Number
        // Twitch       UserX   gifted Subs    5
        // Twitch-UserX-gifted Subs-5

        string line = $"{nameof(platform)}-{user}-{what}";

        string? foundLine = lines.FirstOrDefault(l => l.Contains(line) && !l.Contains("Hypetrain Level"));

        if (foundLine != null)
        {
            string[] parts = foundLine.Split('-');

            int oldcount = int.Parse(parts[parts.Length]);

            int sum = oldcount + count;

            string newLine = $"{line}-{sum}";

            lines.FirstOrDefault(foundLine).Replace(foundLine, newLine);

            _manageFile.WriteFile(lines.ToArray(), false);
        }
        else
        {
            lines = new List<string>() { $"{line}-{count}" };

            _manageFile.WriteFile(lines.ToArray(), true);
        }
    }
}
