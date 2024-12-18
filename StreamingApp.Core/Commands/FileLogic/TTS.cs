using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Commands.FileLogic;

public class TTS
{
    private readonly IManageFile _manageFile;

    public TTS(IManageFile manageFile)
    {
        _manageFile = manageFile;
    }

    public void Execute(string user, string message, TypeEnum type)
    {
        string text = $"{user}-{type}-{message}";

        _manageFile.WriteFile([text], true);
    }
}
