using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Customer
    {
        public Customer()
        {
            LoyaltyDiscounts = new HashSet<LoyaltyDiscount>();
        }

        public int CustomerId { get; set; }
        public int PersonId { get; set; }
        public string DriverLicenseNumber { get; set; } = null!;
        public DateOnly LicenseIssueDate { get; set; }
        public string? LicenseCategory { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<LoyaltyDiscount> LoyaltyDiscounts { get; set; }
    }
}
