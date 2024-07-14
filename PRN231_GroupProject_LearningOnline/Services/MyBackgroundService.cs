namespace PRN231_GroupProject_LearningOnline.Services
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;

        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("MyBackgroundService is running.");

                // Thực hiện công việc chạy ngầm ở đây
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
