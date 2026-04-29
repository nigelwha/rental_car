using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Fine
    {
        public int FineId { get; set; }
        public int RentalId { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string Reason { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? Status { get; set; }

        public virtual Rental Rental { get; set; } = null!;
    }
}
