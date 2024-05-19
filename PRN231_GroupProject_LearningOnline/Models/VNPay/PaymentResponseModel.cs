namespace PRN231_GroupProject_LearningOnline.Models.VNPay
{
    public class PaymentResponseModel
    {
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public string Vnp_Amount { get; set; }
        public string Vnp_BankCode {get; set;}
        public int ProjectId {  get; set; }
    }
}
