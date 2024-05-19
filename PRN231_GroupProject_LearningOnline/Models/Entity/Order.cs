
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public partial class Order
    {
        public string OrderId { get; set; } = null!;
        public int? ProjectId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? BankCode { get; set; }
        public string Amount { get; set; } = null!;
        public string OrderInfo { get; set; } = null!;
        public string ErrorCode { get; set; } = null!;
        public string LocalMessage { get; set; } = null!;
        public DateTime? DateOfDonation { get; set; }

        [JsonIgnore]
        public virtual FundraisingProject? Project { get; set; }
    }

    public partial class OrderDTO
    {
        public string OrderId { get; set; } = null!;
        public int? ProjectId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? BankCode { get; set; }
        public string Amount { get; set; } = null!;
        public string OrderInfo { get; set; } = null!;
        public string ErrorCode { get; set; } = null!;
        public string LocalMessage { get; set; } = null!;
        public string? DateOfDonation { get; set; } 

        public virtual FundraisingProjectDTO? Project { get; set; }
    }
}
