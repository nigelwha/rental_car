using System;
using System.Collections.Generic;

namespace _02.Models
{
    public partial class User
    {
        public User()
        {
            Rentals = new HashSet<Rental>();
            Reservations = new HashSet<Reservation>();
        }

        public int UserId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? DriverLicenseNumber { get; set; }
        public DateOnly? LicenseIssueDate { get; set; }
        public string? LicenseCategory { get; set; }
        public int RoleId { get; set; }
        public string? EmployeeCode { get; set; }
        public DateOnly? HireDate { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Authorization? Authorization { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
