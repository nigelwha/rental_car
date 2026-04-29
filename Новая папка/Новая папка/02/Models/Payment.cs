using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? ReservationId { get; set; }
        public int? RentalId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string OperationType { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;

        public virtual Rental? Rental { get; set; }
        public virtual Reservation? Reservation { get; set; }
    }
}
