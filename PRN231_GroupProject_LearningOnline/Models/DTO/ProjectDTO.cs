using PRN231_GroupProject_LearningOnline.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace PRN231_GroupProject_LearningOnline.Models.DTO
{
    public class ProjectDTO
    {
        public ProjectDTO()
        {
            Orders = new HashSet<Order>();
        }

        public int ProjectId { get; set; }
        public byte Type { get; set; }
        public string Title { get; set; } = null!;
        public string Story { get; set; } = null!;
        public string? Image { get; set; }
        public int? TargetAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Discontinued { get; set; }
        public int? LikeCount { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public string Href { get; set; }
        public string TypeText { get; set; }
        public string StartDateText { get; set; }
        public string EndDateText { get; set; }
        public int Total { get; internal set; }
        public object Status { get; internal set; }
    }
}
