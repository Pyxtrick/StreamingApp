﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.Domain.Entities.Internal;

/// <summary>
/// used of Highlights / clip durring a stream
/// </summary>
public class StreamHighlight
{
    public int Id { get; set; }

    public int StreamId { get; set; }
    public Stream Stream { get; set; }

    public string Description { get; set; }

    public string? HighlightUrl { get; set; }

    public DateTime HighlighteTime { get; set; }
}
