using System.Text.Json;
using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.Models.DTO
{
    public class LessonDTO
    {
        public int LessonId { get; set; }
        public int? LessonNum { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
        [JsonIgnore]
        public string? Quiz { get; set; }
        public List<QuizDTO>? Quizes
        {
            get
            {
                return string.IsNullOrEmpty(Quiz) ? new List<QuizDTO>() : JsonSerializer.Deserialize<List<QuizDTO>>(Quiz);
            }
            set
            {
                Quiz = value == null ? null : JsonSerializer.Serialize(value);
            }
        }
        public int? PreviousLessioNum { get; set; }
    }
}
