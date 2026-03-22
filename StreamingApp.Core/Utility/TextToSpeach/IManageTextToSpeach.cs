using StreamingApp.Domain.Entities.Dtos.Twitch;

namespace StreamingApp.Core.Utility.TextToSpeach;

public interface IManageTextToSpeach
{
    void Execute(MessageDto message);
    Task PlaySpecificTTSMessage(int id);

    Task PlayLatestTTSMessage();
}