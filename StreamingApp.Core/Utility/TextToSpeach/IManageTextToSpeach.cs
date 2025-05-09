using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Utility.TextToSpeach;
public interface IManageTextToSpeach
{
    void Execute(string message, ChatOriginEnum chatOrigin);
    Task PlayTTSMessage();
}