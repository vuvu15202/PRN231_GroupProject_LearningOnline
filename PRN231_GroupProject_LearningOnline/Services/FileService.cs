using OfficeOpenXml;
using PRN231_GroupProject_LearningOnline.Services;

public class FileService : IFileService
{
    private IWebHostEnvironment environment;
    public FileService(IWebHostEnvironment env)
    {
        this.environment = env;
    }

    public async Task<(int, string)> SaveImageAsync(IFormFile imageFile)
    {
        try
        {
            var contentPath = this.environment.ContentRootPath;
            // path = "c://projects/productminiapi/wwwroot/images" ,not exactly something like that
            var path = Path.Combine("wwwroot", "images");
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
                return new (0, msg);
            }
            string uniqueString = Guid.NewGuid().ToString();
            // we are trying to create a unique filename here
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);

            var stream = new FileStream(fileWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            stream.Close();
            return new (1, "/images/"+newFileName);
        }
        catch (Exception ex)
        {
            return new (0, "Error has occured");
        }
    }

    public async Task DeleteImageAsync(string imageFileName)
    {
        var contentPath = this.environment.ContentRootPath;
        var path = Path.Combine(contentPath,"wwwroot", "images", imageFileName);
        if (File.Exists(path))
            File.Delete(path);
    }

    public Task<(int status, string message)> ImportQzuiz(IFormFile QuizFile)
    {
        //ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        //if (QuizFile != null && QuizFile.Length > 0)
        //{
        //    try
        //    {
        //        using (var package = new ExcelPackage(QuizFile.OpenReadStream()))
        //        {
        //            try
        //            {
        //                var worksheet = package.Workbook.Worksheets[0];


        //                int rowCount = worksheet.Dimension.Rows;
        //                for (int row = 2; row <= rowCount; row++) // Start from the second row (excluding the header)
        //                {
        //                    var product = new Product();
        //                    //name, description, category, date, discout,price image, avaiable, homestatus
        //                    if (worksheet.Cells[row, 1].Value == null)
        //                    {
        //                        TempData["checkExcel"] = "add failed";
        //                        return Redirect("DashProduct");
        //                    }
        //                    else
        //                    {
        //                        product.ProductName = worksheet.Cells[row, 1].Value.ToString(); // Read value from the Name column (column 2)
        //                    }


        //                    if (worksheet.Cells[row, 2].Value == null)
        //                    {
        //                        TempData["checkExcel"] = "add failed";
        //                        return Redirect("DashProduct");
        //                    }
        //                    else
        //                    {
        //                        product.ProductDescription = worksheet.Cells[row, 2].Value.ToString();
        //                    }


        //                    if (worksheet.Cells[row, 3].Value == null)
        //                    {
        //                        TempData["checkExcel"] = "add failed";
        //                        return Redirect("DashProduct");
        //                    }
        //                    else
        //                    {
        //                        string cellValue = worksheet.Cells[row, 3].Value.ToString();
        //                        if (int.TryParse(cellValue, out int result))
        //                        {
        //                            product.SubCategoryID = result;
        //                        }
        //                        else
        //                        {
        //                            TempData["checkExcel"] = "add failed";
        //                            return Redirect("DashProduct");
        //                        }

        //                    }


        //                    product.ImportDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));


        //                    if (worksheet.Cells[row, 4].Value == null)
        //                    {
        //                        TempData["checkExcel"] = "add failed";
        //                        return Redirect("DashProduct");
        //                    }
        //                    else
        //                    {
        //                        string cellValue = worksheet.Cells[row, 4].Value.ToString();
        //                        if (double.TryParse(cellValue, out double result))
        //                        {
        //                            product.Discount = result;
        //                        }
        //                        else
        //                        {
        //                            TempData["checkExcel"] = "add failed";
        //                            return Redirect("DashProduct");
        //                        }

        //                    }


        //                    if (worksheet.Cells[row, 5].Value == null)
        //                    {
        //                        TempData["checkExcel"] = "add failed";
        //                        return Redirect("DashProduct");
        //                    }
        //                    else
        //                    {
        //                        string cellValue = worksheet.Cells[row, 5].Value.ToString();
        //                        if (double.TryParse(cellValue, out double result))
        //                        {
        //                            product.ProductPrice = result;
        //                        }
        //                        else
        //                        {
        //                            TempData["checkExcel"] = "add failed";
        //                            return Redirect("DashProduct");
        //                        }

        //                    }


        //                    if (worksheet.Cells[row, 6].Value == null)
        //                    {
        //                        TempData["checkExcel"] = "add failed";
        //                        return Redirect("DashProduct");
        //                    }
        //                    else
        //                    {
        //                        product.ImageMain = worksheet.Cells[row, 6].Value.ToString();

        //                    }

        //                    product.IsAvailble = false;
        //                    product.HomeStatus = false;
        //                    _shopContext.Products.Add(product);
        //                    _shopContext.SaveChanges();
        //                }
        //                TempData["checkExcel"] = "add successfull";
        //            }
        //            catch (Exception ex)
        //            {
        //                TempData["checkExcel"] = "add failed";
        //                return Redirect("DashProduct");
        //            }

        //        }
        //    }
        //    catch
        //    {
        //        TempData["checkExcel"] = "add failed";
        //        return Redirect("DashProduct");
        //    }
        //}
        //return Redirect("DashProduct");
        throw new NotImplementedException();
    }
}