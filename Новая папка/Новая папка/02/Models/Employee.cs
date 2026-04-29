using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public string Position { get; set; } = null!;
        public string EmployeeCode { get; set; } = null!;
        public DateOnly HireDate { get; set; }
        public decimal? RentalDiscountRate { get; set; }

        public virtual Person Person { get; set; } = null!;
    }
}
