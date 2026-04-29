using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Rental
    {
        public Rental()
        {
            Fines = new HashSet<Fine>();
            Payments = new HashSet<Payment>();
        }

        public int RentalId { get; set; }
        public int? ReservationId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateOnly ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public int MileageAtPickup { get; set; }
        public int? MileageAtReturn { get; set; }
        public int? RentalPeriod { get; set; }
        public string? ReturnCondition { get; set; }
        public decimal? FinalCost { get; set; }

        public virtual Car Car { get; set; } = null!;
        public virtual Reservation? Reservation { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Fine> Fines { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
