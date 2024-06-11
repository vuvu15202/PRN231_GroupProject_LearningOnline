using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
