﻿using PRN231_GroupProject_LearningOnline.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_GroupProject_LearningOnline.temp
{
    public partial class CourseEnroll
    {
        public int CourseEnrollId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public int LessonCurrent { get; set; } = 1;
        public int CourseStatus { get; set; }
        public string? Grade { get; set; }
        public float? AverageGrade { get; set; }
        public string? StudentFeeId { get; set; }
        [JsonIgnore]
        public virtual Course? Course { get; set; } = null!;
        [JsonIgnore]
        public virtual User? User { get; set; } = null!;
        [JsonIgnore]
        public virtual StudentFee? StudentFee { get; set; } = null!;
    }
}
