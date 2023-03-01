﻿using System;
using System.Collections.Generic;

namespace CIPlatformIntegration.Entities.Models
{
    public partial class MissionSkill
    {
        public long MissionSkillId { get; set; }
        public long SkillId { get; set; }
        public long? MissionId { get; set; }
        public byte[] CreatedAt { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Mission? Mission { get; set; }
        public virtual Skill Skill { get; set; } = null!;
    }
}
