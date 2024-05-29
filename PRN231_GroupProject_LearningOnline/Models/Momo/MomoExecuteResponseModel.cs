namespace PRN231_GroupProject_LearningOnline.Models.Momo;

//thanh tóan xong momo trả về callback model này
public class MomoExecuteResponseModel
{
    public string OrderId { get; set; }
    public string Amount { get; set; }
    public string OrderInfo { get; set; }
    public int ErrorCode { get; set; }
    public string LocalMessage { get; set; }
    public int ProjectId { get; set; }
}