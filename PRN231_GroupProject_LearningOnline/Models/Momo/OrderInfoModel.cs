namespace PRN231_GroupProject_LearningOnline.Models.Momo;
//người dùng gửi tới server thông tin thanh toán
public class OrderInfoModel
{
    public string FullName { get; set; }
    public string OrderId { get; set; }
    public string OrderInfo { get; set; }
    public double Amount { get; set; }
    public int ProjectId { get; set; }
}