using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Entities.DTO;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.temp;
using X.PagedList;

namespace PRN231_GroupProject_LearningOnline.Models
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<FundraisingProject, ProjectDTO>()
                .ForMember(p => p.Href, opt => opt.MapFrom(src => "#"))
                .ForMember(p => p.TypeText, opt => opt.MapFrom(src => Enum.GetName(typeof(TypeEnums), src.Type)))
                .ForMember(p => p.StartDateText, opt => opt.MapFrom(src => src.StartDate.ToShortDateString()))
                .ForMember(p => p.EndDateText, opt => opt.MapFrom(src => src.EndDate.ToShortDateString()))
                .ForMember(p => p.Total, opt => opt.MapFrom(src => src.Orders.Sum(x => Int32.Parse(x.Amount)))) 
                .ForMember(p => p.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(ProjectStatusEnum), src.Discontinued)));

            CreateMap<StudentFee, StudentFeeDTO>()
                .ForMember(p => p.DateOfPaid, opt => opt.MapFrom(src => src.DateOfPaid!.Value.ToString("dd MMMM yyyy, 'at' hh:mm:ss tt")));

            CreateMap<PagedList<FundraisingProject>, PagedList<ProjectDTO>>();
            CreateMap<CourseEnroll, CourseEnrollDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Course, CourseDTO>()
                      .ForMember(x => x.NumberEnrolled,opt => opt.MapFrom(src => src.CourseEnrolls.Count()));

            CreateMap<CreateCategory, Category>();
            CreateMap<UpdateCategory, Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
            


            CreateMap<Lesson, LessonDTO>().ForMember(dest => dest.Quiz, opt => opt.MapFrom(src => src.Quiz));
            // Thêm các mappings khác nếu cần
        }
    }
    public static class AutoMapperConfig
    {
        public static IMapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            return config.CreateMapper();
        }
    }
}
