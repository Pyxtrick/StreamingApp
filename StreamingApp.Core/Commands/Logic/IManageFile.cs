namespace StreamingApp.Core.Commands.Logic;

public interface IManageFile
{
    void CreateFile(string contence);

    List<string> ReadFile(string contence);

    void WriteFile(string[] lines, bool isAdding);
}