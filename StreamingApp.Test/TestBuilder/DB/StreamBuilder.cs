using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Stream;
using Stream = StreamingApp.Domain.Entities.InternalDB.Stream.Stream;

namespace StreamingApp.Test.TestBuilder.DB;

public static class StreamBuilder
{
    public static Stream Create(this UnitOfWorkContext unitOfWork)
    {
        Stream stream = new Stream();
        unitOfWork.Add(stream);

        return stream;
    }

    public static Stream WithDefaults(this Stream stream, int id = 1)
    {
        stream.Id = id;
        stream.StreamStart = DateTime.UtcNow;
        stream.StreamEnd = DateTime.UtcNow.AddHours(3);

        return stream;
    }

    public static Stream WithTitleAndVodUrl(this Stream stream, string streamTitle, string vodUrl)
    {
        stream.StreamTitle = streamTitle;
        stream.VodUrl = vodUrl;

        return stream;
    }

    public static Stream WithGameCategories(this Stream stream, List<StreamGame> gameCategories)
    {
        stream.GameCategories = gameCategories;

        return stream;
    }

    public static Stream WithStreamHighlights(this Stream stream, List<StreamHighlight> streamHighlights)
    {
        stream.StreamHighlights = streamHighlights;

        return stream;
    }
}
