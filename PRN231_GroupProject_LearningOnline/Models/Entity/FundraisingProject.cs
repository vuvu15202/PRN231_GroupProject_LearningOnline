using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;

namespace PRN231_GroupProject_LearningOnline.Models.Entity
{
    public partial class FundraisingProject
    {
        public FundraisingProject()
        {
            Orders = new HashSet<Order>();
        }

        public int ProjectId { get; set; }
        public byte Type { get; set; }
        public string Title { get; set; } = null!;
        public string Story { get; set; } = null!;
        public string? Image { get; set; }
        public int? TargetAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Discontinued { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public partial class FundraisingProjectDTO
    {
        public FundraisingProjectDTO()
        {
            //Orders = new HashSet<Order>();
        }

        public int ProjectId { get; set; }
        public byte Type { get; set; }
        public string TypeText { get; set; }
        public string Title { get; set; } = null!;
        public string Story { get; set; } = null!;
        public string? Image { get; set; }
        public int? TargetAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Discontinued { get; set; }
        public int? Total { get; set; }
        public virtual ICollection<OrderDTO>? Orders { get; set; }
    }
}
