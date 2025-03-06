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

    public void CreateFile(string contence = "Achievements")
    {
        // File Names
        // Stream Id        Online      Contence        Date
        // 4                .0 / .5     Achievements    12.11.2024
        // 4.0-Achievemnts-12.11.2024.txt

        var stream = _unitOfWork.StreamHistory.OrderBy(s => s.StreamStart).Last();

        string path = $"{stream.Id}.{(stream.StreamEnd != null ? 0 : 5)}-{contence}-{DateOnly.FromDateTime(stream.StreamStart)}.txt";

        using (var streamWriter = File.AppendText(path))
        {
            streamWriter.WriteLine($"Data for {contence}");
        }
    }

    public List<string> ReadFile(string contence = "Achievements")
    {
        var stream = _unitOfWork.StreamHistory.OrderBy(t => t.Id).Last();

        DateOnly dateOnly = DateOnly.FromDateTime(stream.StreamStart);

        var t = stream.StreamStart.ToString("dd-MM-yyyy");

        DocPath = $"NewFolder/{stream.Id}.{(stream.StreamEnd != null ? 0 : 5)}-{contence}-{t}.txt";

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
                        DocPath = $"{stream.Id}.5-{contence}-{t}.txt";
                        dateOnly.AddDays(1);
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
