using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.temp
{
    public partial class Category
    {
        public Category()
        {
            Courses = new HashSet<Course>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
