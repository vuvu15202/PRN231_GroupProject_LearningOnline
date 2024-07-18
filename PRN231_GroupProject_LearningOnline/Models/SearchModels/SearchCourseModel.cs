using PRN231_GroupProject_LearningOnline.Models.Resourse;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.Models.SearchModels
{
    public class SearchCourseModel : QueryResource
    {
        
        public string? KeySearch {  get; set; }
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public TypePrice? TypePrice { get; set; }
    }
}
