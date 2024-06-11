using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using System;
using System.Collections.Generic;

namespace PRN231_GroupProject_LearningOnline.temp
{
    public partial class CourseDTO
    {

        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPrivate { get; set; }
        public long? Price { get; set; } = null!;
        public List<LessonDTO>? Lessons { get; set; }
    }
}
