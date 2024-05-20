using PRN231_GroupProject_LearningOnline.Models.Entity;
using System;
using System.Collections.Generic;

namespace PRN231_GroupProject_LearningOnline.temp
{
    public partial class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
            Reviews = new HashSet<Review>();
        }

        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int UserId { get; set; }
        public string CourseInfo { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPrivate { get; set; }
        public long? Price { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
