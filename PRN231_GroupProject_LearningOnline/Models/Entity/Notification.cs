using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public class Notification
    {
        public Notification()
        {
            //Courses = new HashSet<Course>();
        }

        public int NotificationId { get; set; }
        public string NotificationTitle { get; set; } = null!;
        public string NotificationContent { get; set; } = null!;
        public DateTime NotificationAt { get; set; }
        public int NotificationTo { get; set; }
        public bool IsRead { get; set; } = default!;

        public virtual User User { get; set; }
    }
}
