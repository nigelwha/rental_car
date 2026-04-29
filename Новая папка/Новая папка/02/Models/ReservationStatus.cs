using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class ReservationStatus
    {
        public ReservationStatus()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
