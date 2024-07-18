using Microsoft.AspNetCore.SignalR;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Notification;
using System.Dynamic;

namespace PRN231_GroupProject_LearningOnline.Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private DonationWebApp_v2Context _context = new DonationWebApp_v2Context();
        private readonly IHubContext<NotiHub> _myHub;
        private readonly IUserContextService _userContextService;

        public MyBackgroundService(ILogger<MyBackgroundService> logger, IHubContext<NotiHub> myHub, IUserContextService userContextService)
        {
            _logger = logger;
            _myHub = myHub;
            _userContextService = userContextService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("MyBackgroundService is running.");
                // Thực hiện công việc chạy ngầm ở đây

                var user = _userContextService.GetUser();
                if (user != null)
                {
                    //var studentFees = _context.CourseEnrolls
                    //    .Where(s => s.StudentFeeId != null && (DateTime.Now - s.EnrollDate).Days > 30 * 3 && (DateTime.Now - s.EnrollDate).Days < (30 * 3) + 1).ToList();
                    UserNoti u = UserList.GetUser(user.UserId.ToString());
                    if(u != null)
                    {
                        //await _myHub.Clients.Client(u.ConnectionId).SendAsync("ReceivedNoti", "Thông báo gia hạn khóa học", "Khóa học abc của bạn sắp hết hạn, vui lòng đăng ký thêm để tiếp tục rèn luyện!");
                        //_context.Notifications.Add(new Models.Entity.Notification {
                        //  NotificationTitle = "Thông báo gia hạn khóa học",
                        //  NotificationContent = "Khóa học abc của bạn sắp hết hạn, vui lòng đăng ký thêm để tiếp tục rèn luyện!",
                        //  NotificationAt = DateTime.Now,
                        //  NotificationTo = user.UserId
                        //});
                        //_context.SaveChanges();
                        _logger.LogInformation(u.ConnectionId);
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    public interface IUserContextService
    {
        User GetUser();
        void SetUser(User user);
    }

    public class UserContextService : IUserContextService
    {
        private User _user;

        public User GetUser()
        {
            return _user;
        }

        public void SetUser(User user)
        {
            _user = user;
        }
    }

    public class YourMiddleware
    {
        private readonly RequestDelegate _next;

        public YourMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserContextService userContextService)
        {
            // Lấy thông tin người dùng từ context và đặt vào userContextService
            var user = context.Items["User"] as User;
            if (user != null)
            {
                userContextService.SetUser(user);
            }

            await _next(context);
        }



    }

}
