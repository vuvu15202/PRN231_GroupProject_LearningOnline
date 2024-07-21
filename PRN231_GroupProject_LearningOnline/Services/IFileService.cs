namespace PRN231_GroupProject_LearningOnline.Services
{
    public interface IFileService
    {
        Task<(int status, string message)> SaveImageAsync(IFormFile imageFile);
        Task DeleteImageAsync(string imageFileName);

        Task<(int status, string message)> ImportQzuiz(IFormFile QuizFile);
    }
}