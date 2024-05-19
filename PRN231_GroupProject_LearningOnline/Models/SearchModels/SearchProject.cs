namespace PRN231_GroupProject_LearningOnline.Models.SearchModels
{
    public class SearchProject
    {
        public string? Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
