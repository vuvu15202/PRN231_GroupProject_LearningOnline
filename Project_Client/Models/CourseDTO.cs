using System.Text.Json.Serialization;
using System.Text.Json;

namespace Project_Client.Models
{
	public class CourseDTO
	{
		public int CourseId { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; } = null!;
		public string Image { get; set; } = null!;
		public string Description { get; set; } = null!;
		public bool IsPrivate { get; set; }
		public long? Price { get; set; } = null!;
		public List<LessonDTO>? Lessons { get; set; }
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
		public List<QuizDTO>? Quizes {  get; set; }
		public int? PreviousLessioNum { get; set; }
	}

	public class QuizDTO
	{
		public int questionNo { get; set; }
		public string question { get; set; }
		public string answerA { get; set; }
		public string answerB { get; set; }
		public string answerC { get; set; }
		public string answerD { get; set; }
		[JsonIgnore]
		public string correctAnswer { get; set; }
		public string answer { get; set; }
	}




}
