using PRN231_GroupProject_LearningOnline.Models.Entity;
using System;
using System.Collections.Generic;

namespace PRN231_GroupProject_LearningOnline.temp
{
    public partial class Review
    {

        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int Vote { get; set; }
        public string? Content { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
