using System;
using System.Collections.Generic;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
