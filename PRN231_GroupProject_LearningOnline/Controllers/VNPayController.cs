using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.VNPay;
using PRN231_GroupProject_LearningOnline.Services;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    public class VNPayController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public VNPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            DonationWebApp_v2Context _context = new DonationWebApp_v2Context();

            try
            {
                if (response.VnPayResponseCode.Equals("00"))
                {
                    Order order = new Order()
                    {
                        ProjectId = int.Parse(response.OrderDescription.Split(";")[1]),
                        OrderId = response.OrderId,
                        PaymentMethod = response.PaymentMethod,
                        BankCode = response.Vnp_BankCode,
                        Amount = response.Vnp_Amount,
                        OrderInfo = response.OrderDescription.Split(";")[0],
                        ErrorCode = response.VnPayResponseCode,
                        LocalMessage = "Thành Công",
                        DateOfDonation = DateTime.Now,
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return Redirect("https://localhost:5000/project?id=" + int.Parse(response.OrderDescription.Split(";")[1]) + "&statuscode=2");

            }
            return Redirect("https://localhost:5000/project?id="+ int.Parse(response.OrderDescription.Split(";")[1]) + "&statuscode=1");
        }

        //public IActionResult PaymentCallback()
        //{
        //    var response = _vnPayService.PaymentExecute(Request.Query);

        //    return Json(response);
        //}


    }
}

//{
//    "orderDescription": "Code Mega Thanh toán tại Code Mega 20000000",
//    "transactionId": "14302058",
//    "orderId": "638440657783943347",
//    "paymentMethod": "VnPay",
//    "paymentId": "14302058",
//    "success": true,
//    "token": "ccfb6acef21b5785e3787e4089dcb428d33335255af7c204546aa3f33926dd25d252aef65aa869ceb4ee2810b1cec317ee1d39e32b4567acc8a0a45db00fd1ab",
//    "vnPayResponseCode": "00"
//}