﻿namespace PRN231_GroupProject_LearningOnline.Models.VNPay
{
    //người dùng gửi tới server thông tin thanh toán
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
        public int ProjectId {  get; set; }
    }
}
