﻿using System;
using System.Collections.Generic;

namespace CIPlatformIntegration.Entities.Models;

public partial class Timesheet
{
    public long TimesheetId { get; set; }

    public long? UserId { get; set; }

    public long? MissionId { get; set; }

    public DateTime Time { get; set; }

    public int? Action { get; set; }

    public DateTime DateVolunteered { get; set; }

    public string? Notes { get; set; }

    public string Status { get; set; } = null!;

    public byte[] CreatedAt { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Mission? Mission { get; set; }

    public virtual User? User { get; set; }
}
