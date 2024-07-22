using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public class Reply
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UserName { get; set; }
        public bool IsHide { get; set; }
        public int CommentId { get; set; }
        [JsonIgnore]
        public virtual Comment? Comment { get; set; } = null!;
    }
}
