﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.Domain.Entities.Internal;

/// <summary>
/// games / categorys played durring a stream
/// </summary>
public class StreamGame
{
    public int GameCategoryId { get; set; }
    public GameInfo? GameCategory { get; set; }

    public int StreamId { get; set; }
    public Stream? Stream { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}