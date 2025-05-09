﻿using StreamingApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.InternalDB.Trigger;

public class CommandAndResponse : EntityBase
{
    public int Id { get; set; }

    // Chat Command
    [Required]
    public string Command { get; set; }

    // Chat Response
    [Required]
    public string Response { get; set; }

    public string Description { get; set; }

    // Is Active
    public bool Active { get; set; }

    // needed Authentication
    public AuthEnum Auth { get; set; }

    // Category to easier disdinguish | search between commands
    public CategoryEnum Category { get; set; }

    public bool HasLogic { get; set; }

    public int? TargetId { get; set; }
    public Target? Target { get; set; }
}
