using PRN231_GroupProject_LearningOnline.temp;
using System.ComponentModel.DataAnnotations;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UserName { get; set; }
        public int CourseId { get; set; }
        public bool IsHide { get; set; }

        public virtual ICollection<Reply>? Replies { get; set; }
    }
}
