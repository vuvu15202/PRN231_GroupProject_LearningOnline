﻿using PRN231_GroupProject_LearningOnline.temp;
using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public partial class StudentFee
    {
        public string StudentFeeId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string? BankCode { get; set; }
        public string Amount { get; set; } = null!;
        public string OrderInfo { get; set; } = null!;
        public string ErrorCode { get; set; } = null!;
        public string LocalMessage { get; set; } = null!;
        public DateTime? DateOfPaid { get; set; }
        public int? CourseEnrollId { get; set; }
        public virtual CourseEnroll? CourseEnroll { get; set; } = null!;


    }

    public partial class StudentFeeDTO
    {
        public string StudentFeeId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string? BankCode { get; set; }
        public string Amount { get; set; } = null!;
        public string OrderInfo { get; set; } = null!;
        public string ErrorCode { get; set; } = null!;
        public string LocalMessage { get; set; } = null!;
        public string? DateOfPaid { get; set; }

    }
}