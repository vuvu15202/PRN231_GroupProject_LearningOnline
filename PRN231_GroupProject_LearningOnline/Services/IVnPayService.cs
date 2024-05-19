using PRN231_GroupProject_LearningOnline.Models.VNPay;

namespace PRN231_GroupProject_LearningOnline.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
