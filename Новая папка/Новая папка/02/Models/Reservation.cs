using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            Payments = new HashSet<Payment>();
            Rentals = new HashSet<Rental>();
        }

        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int StatusId { get; set; }
        public decimal PrepaymentAmount { get; set; }
        public decimal EstimatedTotalCost { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual ReservationStatus Status { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
