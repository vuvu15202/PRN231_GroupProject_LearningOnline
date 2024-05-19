using PRN231_GroupProject_LearningOnline.Models.Momo;

namespace PRN231_GroupProject_LearningOnline.Services;

public interface IMomoService
{
    Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
    MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
}