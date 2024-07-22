using System.ComponentModel.DataAnnotations;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public int CourseId { get; set; }
        public bool IsHide { get; set; }
        public int ParentId { get; set; }

    }
}
