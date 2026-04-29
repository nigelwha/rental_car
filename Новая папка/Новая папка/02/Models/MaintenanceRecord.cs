using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class MaintenanceRecord
    {
        public int MaintenanceId { get; set; }
        public int CarId { get; set; }
        public string WorkDescription { get; set; } = null!;
        public DateOnly ServiceDate { get; set; }
        public decimal Cost { get; set; }

        public virtual Car Car { get; set; } = null!;
    }
}
