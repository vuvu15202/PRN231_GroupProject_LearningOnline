using System;
using System.Collections.Generic;

namespace PRN231_GroupProject_LearningOnline.temp
{
    public partial class Lesson
    {

        public int LessonId { get; set; }
        public int? LessonNum { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        public string? Quiz { get; set; }
        public int? PreviousLessionId { get; set; }

        public virtual Lesson? PreviousLession { get; set; }
        public virtual Course Course { get; set; } = null!;
    }
}
