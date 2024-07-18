using System.Text.Json.Serialization;

namespace Project_Client.Models
{
    public class CourseResponse
    {
        [JsonPropertyName("courseId")]
        public int CourseId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("image")]
        public string Image { get; set; } = null!;
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("price")]
        public long? Price { get; set; } = null!;
        [JsonPropertyName("numberEnroll")]
        public int NumberEnroll { get; set; } = 0;
    }
}
