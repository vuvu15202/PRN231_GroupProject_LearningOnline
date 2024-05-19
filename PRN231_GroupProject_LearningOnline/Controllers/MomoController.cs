using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.Momo;
using PRN231_GroupProject_LearningOnline.Services;

namespace PRN231_GroupProject_LearningOnline.Controllers;

public class MomoController : Controller
{
    private IMomoService _momoService;

    public MomoController(IMomoService momoService)
    {
        _momoService = momoService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model)
    {
        var response = await _momoService.CreatePaymentAsync(model);
        return Redirect(response.PayUrl);
    }

    [HttpGet]
    public IActionResult PaymentCallBack()
    {
        var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);

        DonationWebApp_v2Context _context = new DonationWebApp_v2Context();
        
        try
        {
            if (response.ErrorCode == 0)
            {
                Order order = new Order()
                {
                    ProjectId = response.ProjectId,
                    OrderId = response.OrderId,
                    PaymentMethod = "MomoQR",
                    Amount = response.Amount,
                    OrderInfo = response.OrderInfo,
                    ErrorCode = response.ErrorCode.ToString(),
                    LocalMessage = response.LocalMessage,
                    DateOfDonation = DateTime.Now,
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                
            }
            
        }
        catch(Exception ex)
        {
            return Redirect("https://localhost:5000/project?id=" + response.ProjectId + "&statuscode=2");
        }
        return Redirect("https://localhost:5000/project?id=" +response.ProjectId + "&statuscode=1");
    }
}