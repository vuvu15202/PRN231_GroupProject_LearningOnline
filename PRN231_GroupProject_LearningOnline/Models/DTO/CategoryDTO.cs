using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231_GroupProject_LearningOnline.Models.DTO
{
    public class CategoryDTO
    {
    }

    public class CreateCategory
    {
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
    public class UpdateCategory
    {
        [Required]
        public int categoryId { get; set; }
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }

}
