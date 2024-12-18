using StreamingApp.DB;
using System.Text;

namespace StreamingApp.Core.Commands.FileLogic;
public class ManageFile : IManageFile
{
    private string DocPath { get; set; }

    private readonly UnitOfWorkContext _unitOfWork;

    public ManageFile(UnitOfWorkContext unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void CreateFile(string contence)
    {
        // File Names
        // Stream Count     Online      Contence        Data
        // 4                .0 / .5     Achievements    12.11.2024
        // 4.0-Achievemnts-12.11.2024.txt

        var stream = _unitOfWork.StreamHistory.Last();

        string path = $"{stream.Id}.{(stream.StreamEnd != null ? 0 : 5)}-{contence}-{DateOnly.FromDateTime(stream.StreamStart)}.txt";

        using (var streamWriter = File.AppendText(path))
        {
            streamWriter.WriteLine($"Data for {contence}");
        }
    }

    public List<string> ReadFile(string contence)
    {
        var stream = _unitOfWork.StreamHistory.Last();

        DateOnly dateOnly = DateOnly.FromDateTime(stream.StreamStart);

        DocPath = $"{stream.Id}.{(stream.StreamEnd != null ? 0 : 5)}-{contence}-{dateOnly}.txt";

        bool noWalidPathFound = true;

        if (stream.StreamEnd != null)
        {
            // Find a Valid File
            while (noWalidPathFound)
            {
                using (var fileStream = File.OpenRead(DocPath))
                {
                    if (fileStream != null)
                    {
                        noWalidPathFound = false;
                    }
                    else
                    {
                        DocPath = $"{stream.Id}.5-{contence}-{dateOnly}.txt";
                        dateOnly.AddDays(1);
                        noWalidPathFound = true;
                    }

                    if (dateOnly == DateOnly.FromDateTime(DateTime.Now))
                    {
                        noWalidPathFound = true;
                    }
                }
            }
        }

        const Int32 BufferSize = 128;

        List<string> lines = new List<string>();

        using (var fileStream = File.OpenRead(DocPath))
        using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
        {
            String line;
            while ((line = streamReader.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }

        return lines;
    }

    public void WriteFile(string[] lines, bool isAdding)
    {
        // replace file content if someting is replaced
        if (!isAdding)
        {
            File.WriteAllText(DocPath, "");
        }

        using (var streamWriter = new StreamWriter(DocPath))
        {
            foreach (string line in lines)
            {
                streamWriter.WriteLine(line);
            }
        }
    }
}
