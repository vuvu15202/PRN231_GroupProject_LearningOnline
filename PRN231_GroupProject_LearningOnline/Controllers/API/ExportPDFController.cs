using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Services;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportPDFController : ControllerBase
    {
        private readonly PDFService _pdfService;

        private readonly DonationWebApp_v2Context _context;

        public ExportPDFController(PDFService pdfService, DonationWebApp_v2Context context)
        {
            _pdfService = pdfService;
            _context = context;
        }

        [HttpGet]
        [Authorize(RoleEnum.Student)]
        [Route("generate/{studentId}/{courseId}")]
        public async Task<IActionResult> GeneratePdf(int studentId, int courseId)
        {
            var compelteCourse = await _context.CourseEnrolls.Include(c => c.User).Include(c => c.Course).Where(c => c.UserId == studentId && c.CourseId == courseId).FirstOrDefaultAsync();
            if (compelteCourse == null)
            {
                return NotFound("Student is not found or you haven't enrolled this course.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Services", "templateExport.html");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }
            var fileContent = await System.IO.File.ReadAllTextAsync(filePath);


            if (fileContent == null || string.IsNullOrWhiteSpace(fileContent))
            {
                return BadRequest("Invalid request.");
            }

            fileContent = fileContent.Replace("((name))", compelteCourse.User.FirstName + " " + compelteCourse.User.LastName)
                .Replace("((course))", compelteCourse.Course.Name)
                .Replace("((date))", DateTime.Now.ToString("MM/dd/yyyy"));
            var pdfBytes = _pdfService.GeneratePdf(fileContent);

            return File(pdfBytes, "application/pdf", "generated.pdf");
        }
    }

    public class PdfRequest
    {
        public string HtmlContent { get; set; }
    }

    //{
    //    private readonly IConverter _convert;
    //    private readonly IExportHTMLtoPDF _export;

    //    public ExportPDFController(IConverter convert, IExportHTMLtoPDF export)
    //    {
    //        _convert = convert;
    //        _export = export;
    //    }

    //    [Authorize(RoleEnum.Admin)]
    //    [HttpGet]
    //    public async Task<IActionResult> GeneratePdf()
    //    {
    //        string fileName = "Persons.pdf";
    //        var glb = new GlobalSettings
    //        {
    //            ColorMode = ColorMode.Color,
    //            Orientation = Orientation.Landscape,
    //            PaperSize = PaperKind.A4,
    //            Margins = new MarginSettings()
    //            {
    //                Bottom = 20,
    //                Left = 20,
    //                Right = 20,
    //                Top = 30
    //            },
    //            DocumentTitle = "Persons",
    //            Out = Path.Combine(Directory.GetCurrentDirectory(), "Services", fileName)
    //        };
    //        var objectSettings = new ObjectSettings
    //        {
    //            PagesCount = true,
    //            HtmlContent = _export.ToHtmlFile(new List<string> {"abc", "abc", "abc" }),
    //            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
    //        };
    //        var pdf = new HtmlToPdfDocument
    //        {
    //            GlobalSettings = glb,
    //            Objects = { objectSettings }
    //        };
    //        _convert.Convert(pdf);
    //        string result = $"Files{fileName}";
    //        await Task.Yield();
    //        return Ok(result);
    //    }
    //}
}
