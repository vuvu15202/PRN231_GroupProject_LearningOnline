﻿namespace PRN231_GroupProject_LearningOnline.Models.VNPay
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
        public int ProjectId {  get; set; }
    }
}
