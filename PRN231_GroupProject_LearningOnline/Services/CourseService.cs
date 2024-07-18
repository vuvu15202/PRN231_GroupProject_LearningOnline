using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Helpers;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.Resourse;
using PRN231_GroupProject_LearningOnline.Models.SearchModels;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Services
{
    public class CourseService : ICourseService
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IFileService _fileService;

        public CourseService(DonationWebApp_v2Context context, IMapper mapper, IUriService uriService,IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _uriService = uriService;
            _fileService = fileService; 
        }

        public async Task<(bool IsSuccess, CourseResponse Data)> CreateCourseAsync(CreateCourseResquest resquest, int teacherId)
        {
            var course = _mapper.Map<Course>(resquest);
            course.TeacherId = teacherId;

            if (resquest.ImageFile != null)
            {
                var fileResult = await _fileService.SaveImageAsync(resquest.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    course.Image = fileResult.Item2; // getting name of image
                }
                try
                {
                    await _context.Courses.AddAsync(course);

                    await _context.SaveChangesAsync();

                    return (true, _mapper.Map<CourseResponse>(course));
                }catch (Exception ex)
                {
                    return (false, null);
                }
                
                 
            }
            return (false, _mapper.Map<CourseResponse>(course));

        }

        public async Task<PaginationResult<List<CourseResponse>>> GetListCourseAsync(SearchCourseModel request)
        {
            var query = _context.Courses
                .Include(c => c.EnrollCourses)
                .AsQueryable();

            if(request.CategoryId != null)
            {
                query = query.Where(c => c.CategoryId == int.Parse(request.CategoryId.ToString()));
            }

            if(request.KeySearch != null)
            {
                query = query.Where(c => c.Name.Contains( request.KeySearch));
            }

            var course = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();

            foreach(var c in course)
            {
                c.Image = string.Concat(_uriService.GetBaseUri(), c.Image);
            }


            var totalRecord = query.Count();

            var courseResponse = _mapper.Map<List<CourseResponse>>(course);

            var response = new PaginationResult<List<CourseResponse>>
            {
                Data = courseResponse,
            };

            var queryResourse = new QueryResource
            {
                Page = request.Page,
                PageSize = request.PageSize,
            };

            response.CreatePaginationResponse(queryResourse, totalRecord);

            return response;

        }

        public async Task<PaginationResult<List<EnrolledCourseDTO>>> GetListEnrolledCourseAsync(QueryResource request, int userId)
        {

            var query = _context.CourseEnrolls
                .Where(cr => cr.UserId == userId).AsQueryable();



            var totalRecord = query.Count();

            var data = await query
                .Include(cr => cr.User)
                .Include(cr => cr.Course).ThenInclude(c => c.Lessons)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();
            
            var response = new PaginationResult<List<EnrolledCourseDTO>>
            {
                Data = _mapper.Map<List<EnrolledCourseDTO>>(data)
            };
            foreach(var course in response.Data)
            {
                course.CourseImage = string.Concat(_uriService.GetBaseUri(), course.CourseImage);
            }


            response.CreatePaginationResponse(request, totalRecord);

            return response;


        }



        /// <summary>
        /// Chức năng lấy top 3 course có số người join cao nhất
        /// </summary>
        /// <returns></returns>
        public async Task<List<CourseResponse>> GetTop3CourseAsync()
        {
            var data = await _context.Courses
                .Include(c => c.EnrollCourses)
                .OrderByDescending(c => c.EnrollCourses.Count())
                .Take(3)
                .ToListAsync();
            var response = _mapper.Map<List<CourseResponse>>(data);
            return response;
            
        }




    }
}
