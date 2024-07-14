namespace PRN231_GroupProject_LearningOnline.Notification
{
    public class User
    {
        public string UserName { get; set; }
        public string ConnectionId { get; set; }

        public User(string userName, string connectionId)
        {
            UserName = userName;
            ConnectionId = connectionId;
        }
    }

    public class UserList
    {
        public static List<User> Users = new List<User>();

        public static void AddUser(User user)
        {
            Users.Add(user);
        }

        public static User GetUser(string userName)
        {
            return Users.FirstOrDefault(x => x.UserName.Equals(userName));
        }

        public static User GetUserByConnectionId(string connectionId)
        {
            return Users.FirstOrDefault(x => x.ConnectionId.Equals(connectionId));
        }
    }
}
