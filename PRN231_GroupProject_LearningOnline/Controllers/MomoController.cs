using System.Diagnostics;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.Momo;
using PRN231_GroupProject_LearningOnline.Services;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Controllers;

public class MomoController : Controller
{
    private IMomoService _momoService;
    private readonly IEmailSender _emailSender;

    public MomoController(IMomoService momoService, IEmailSender emailSender)
    {
        _momoService = momoService;
        _emailSender = emailSender;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model)
    {
        var user = (User)HttpContext.Items["User"];
        model.UserId = user.UserId;
        var response = await _momoService.CreatePaymentAsync(model);
        return Redirect(response.PayUrl);
    }

    [HttpGet]
    public async Task<IActionResult> PaymentCallBack()
    {
        var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);

        DonationWebApp_v2Context _context = new DonationWebApp_v2Context();
        
        try
        {
            
            StudentFee studentFee = new StudentFee()
            {
                //CourseEnrollId = response.CourseId,
                StudentFeeId = response.OrderId,
                PaymentMethod = "MomoQR",
                Amount = response.Amount,
                OrderInfo = response.OrderInfo,
                ErrorCode = response.ErrorCode == 0 ? "0" : "Đang xử lí",
                LocalMessage = response.LocalMessage,
                DateOfPaid = DateTime.Now,
            };
            _context.StudentFees.Add(studentFee);
            _context.SaveChanges();
            CourseEnroll courseEnroll = new CourseEnroll()
            {
                UserId = response.UserId,
                CourseId = response.CourseId,
                EnrollDate = DateTime.Now,
                LessonCurrent = 1,
                CourseStatus = 1,
                StudentFeeId = studentFee.StudentFeeId,
            };
            _context.CourseEnrolls.Add(courseEnroll);
            _context.SaveChanges();

            var user = (User)HttpContext.Items["User"];
            await _emailSender.SendEmailAsync(user.Email, "Notification", $"Ban vua dang ky khoa hoc thanh cong");
        }
        catch(Exception ex)
        {
            //return Redirect("https://localhost:5000/courses/payment?courseId=" + response.CourseId + "&statuscode=2");
            return Redirect($"/Courses/Detail?id={response.CourseId}&statuscode=2");

        }
        return Redirect($"/Courses/Detail?id={response.CourseId}&statuscode=1");
    }
}