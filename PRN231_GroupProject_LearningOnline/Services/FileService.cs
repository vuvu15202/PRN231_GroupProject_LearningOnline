using PRN231_GroupProject_LearningOnline.Services;

public class FileService : IFileService
{
    private IWebHostEnvironment environment;
    public FileService(IWebHostEnvironment env)
    {
        this.environment = env;
    }

    public async Task<(int status, string message)> SaveImageAsync(IFormFile imageFile)
    {
        try
        {
            var contentPath = this.environment.ContentRootPath;
            // path = "c://projects/productminiapi/wwwroot/images" ,not exactly something like that
            var path = Path.Combine("wwwroot", "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Check the allowed extenstions
            var ext = Path.GetExtension(imageFile.FileName);
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(ext))
            {
                string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                return new(0, msg);
            }
            string uniqueString = Guid.NewGuid().ToString();
            // we are trying to create a unique filename here
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);

            var stream = new FileStream(fileWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            stream.Close();
            return new(1, "/uploads/" + newFileName);
        }
        catch (Exception ex)
        {
            return new(0, "Error has occured");
        }
    }

    public async Task DeleteImageAsync(string imageFileName)
    {
        var contentPath = this.environment.ContentRootPath;
        var path = Path.Combine(contentPath, "wwwroot", "uploads", imageFileName);
        if (File.Exists(path))
            File.Delete(path);
    }

    public Task<(int status, string message)> ImportQzuiz(IFormFile QuizFile)
    {
        throw new NotImplementedException();
    }
}
