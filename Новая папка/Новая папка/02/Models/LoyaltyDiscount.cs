using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class LoyaltyDiscount
    {
        public int DiscountId { get; set; }
        public int CustomerId { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateOnly ValidFrom { get; set; }
        public DateOnly ValidUntil { get; set; }
        public string? Reason { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
