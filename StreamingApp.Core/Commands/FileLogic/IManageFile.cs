namespace StreamingApp.Core.Commands.FileLogic;

public interface IManageFile
{
    void CreateFile(string contence = "Achievements");

    List<string> ReadFile(string contence = "Achievements");

    void WriteFile(string[] lines, bool isAdding);
}