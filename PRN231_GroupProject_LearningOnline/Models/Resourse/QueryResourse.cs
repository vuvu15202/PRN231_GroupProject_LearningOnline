using System.ComponentModel.DataAnnotations;

namespace PRN231_GroupProject_LearningOnline.Models.Resourse
{
    public class QueryResource
    {
        #region Property
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
        #endregion
    }
}