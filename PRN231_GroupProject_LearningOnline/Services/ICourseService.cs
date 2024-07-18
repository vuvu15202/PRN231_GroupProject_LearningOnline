using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Resourse;
using PRN231_GroupProject_LearningOnline.Models.SearchModels;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Services
{
    public interface ICourseService
    {
        Task<List<CourseResponse>> GetTop3CourseAsync();

        Task<PaginationResult<List<CourseResponse>>> GetListCourseAsync(SearchCourseModel request);

        Task<PaginationResult<List<EnrolledCourseDTO>>> GetListEnrolledCourseAsync(QueryResource query,int userId);

        Task<(bool IsSuccess, CourseResponse Data)> CreateCourseAsync(CreateCourseResquest resquest,int teacherId);
        
    }
}
