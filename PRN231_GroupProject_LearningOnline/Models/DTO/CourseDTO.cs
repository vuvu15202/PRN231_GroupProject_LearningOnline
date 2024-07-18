using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231_GroupProject_LearningOnline.temp
{
    public partial class CourseDTO
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


    public class CourseResponse
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long? Price { get; set; } = null!;
        public int NumberEnroll { get; set; } = 0;
    }

    public class CreateCourseResquest
    {
        [Required(ErrorMessage = "Tên course là bắt buộc")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage ="CategoryId là bắt buộc")]
        public int CategoryId { get; set; }
        public string? Description { get; set; } = null!;
        [Required(ErrorMessage = "Giá Course là bắt buộc")]
        public long? Price { get; set; } = null!;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }


    }

    public class CreateLessonRequest
    {

    }





}
