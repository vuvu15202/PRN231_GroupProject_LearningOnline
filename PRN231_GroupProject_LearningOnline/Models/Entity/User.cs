
using PRN231_GroupProject_LearningOnline.temp;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            Reviews = new HashSet<Review>();
            Courses = new HashSet<Course>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
