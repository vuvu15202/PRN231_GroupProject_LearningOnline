using PRN231_GroupProject_LearningOnline.Entities.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Models.DTO
{
    public class CourseEnrollDTO
    {
        public int CourseEnrollId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public int LessonCurrent { get; set; } = 1;
        public int CourseStatus { get; set; }
        public string? Grade { get; set; }
        public float? AverageGrade { get; set; }
        public string? StudentFeeId { get; set; }
        public virtual CourseDTO Course { get; set; } = null!;
        public virtual UserDTO User { get; set; } = null!;
    }

    public class EnrolledCourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string CourseImage { get; set; } = null!;
        public string CourseDescription { get; set; } = null!;
        public DateTime EnrollDate { get; set; }
        public int LessonCurrent { get; set; } = 1;
        public string? CurrentLessonName { get; set; }
		public string? Grade { get; set; }
		public float? AverageGrade { get; set; }
		public StatusCourseEnum CourseStatus { get; set; }
        public List<LessonDTO>? Lessons { get; set; }

    }

}
