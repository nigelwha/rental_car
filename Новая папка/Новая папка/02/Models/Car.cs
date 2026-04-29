using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class Car
    {
        public Car()
        {
            MaintenanceRecords = new HashSet<MaintenanceRecord>();
            Rentals = new HashSet<Rental>();
            Reservations = new HashSet<Reservation>();
        }

        public int CarId { get; set; }
        public int ModelId { get; set; }
        public string LicensePlate { get; set; } = null!;
        public int ManufactureYear { get; set; }
        public int Mileage { get; set; }
        public int StatusId { get; set; }
        public decimal BaseRentalRatePerDay { get; set; }
        public string? Color { get; set; }

        public virtual Model Model { get; set; } = null!;
        public virtual CarStatus Status { get; set; } = null!;
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
