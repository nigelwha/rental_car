using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using _02.Models;

namespace _02.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authorization> Authorizations { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<CarStatus> CarStatuses { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Fine> Fines { get; set; } = null!;
        public virtual DbSet<LoyaltyDiscount> LoyaltyDiscounts { get; set; } = null!;
        public virtual DbSet<MaintenanceRecord> MaintenanceRecords { get; set; } = null!;
        public virtual DbSet<Model> Models { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Person> Persons { get; set; } = null!;
        public virtual DbSet<Rental> Rentals { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=Ilya");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.HasKey(e => e.AuthId)
                    .HasName("authorizations_pkey");

                entity.ToTable("authorizations");

                entity.HasIndex(e => e.Login, "authorizations_login_key")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "authorizations_user_id_key")
                    .IsUnique();

                entity.Property(e => e.AuthId).HasColumnName("auth_id");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .HasColumnName("password_hash");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Authorization)
                    .HasForeignKey<Authorization>(d => d.UserId)
                    .HasConstraintName("authorizations_user_id_fkey");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brands");

                entity.HasIndex(e => e.Name, "brands_name_key")
                    .IsUnique();

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("cars");

                entity.HasIndex(e => e.LicensePlate, "cars_license_plate_key")
                    .IsUnique();

                entity.HasIndex(e => e.StatusId, "idx_cars_status");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.BaseRentalRatePerDay)
                    .HasPrecision(10, 2)
                    .HasColumnName("base_rental_rate_per_day");

                entity.Property(e => e.Color)
                    .HasMaxLength(20)
                    .HasColumnName("color");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(9)
                    .HasColumnName("license_plate");

                entity.Property(e => e.ManufactureYear).HasColumnName("manufacture_year");

                entity.Property(e => e.Mileage).HasColumnName("mileage");

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cars_model_id_fkey");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cars_status_id_fkey");
            });

            modelBuilder.Entity<CarStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("car_statuses_pkey");

                entity.ToTable("car_statuses");

                entity.HasIndex(e => e.Name, "car_statuses_name_key")
                    .IsUnique();

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.HasIndex(e => e.PersonId, "customers_person_id_key")
                    .IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DriverLicenseNumber)
                    .HasMaxLength(20)
                    .HasColumnName("driver_license_number");

                entity.Property(e => e.LicenseCategory)
                    .HasMaxLength(5)
                    .HasColumnName("license_category")
                    .HasDefaultValueSql("'B'::character varying");

                entity.Property(e => e.LicenseIssueDate).HasColumnName("license_issue_date");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("registration_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.PersonId)
                    .HasConstraintName("customers_person_id_fkey");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.EmployeeCode, "employees_employee_code_key")
                    .IsUnique();

                entity.HasIndex(e => e.PersonId, "employees_person_id_key")
                    .IsUnique();

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EmployeeCode)
                    .HasMaxLength(20)
                    .HasColumnName("employee_code");

                entity.Property(e => e.HireDate)
                    .HasColumnName("hire_date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .HasColumnName("position");

                entity.Property(e => e.RentalDiscountRate)
                    .HasPrecision(5, 2)
                    .HasColumnName("rental_discount_rate")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.PersonId)
                    .HasConstraintName("employees_person_id_fkey");
            });

            modelBuilder.Entity<Fine>(entity =>
            {
                entity.ToTable("fines");

                entity.Property(e => e.FineId).HasColumnName("fine_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.IssuedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("issued_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Reason)
                    .HasMaxLength(50)
                    .HasColumnName("reason");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'Issued'::character varying");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.Fines)
                    .HasForeignKey(d => d.RentalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fines_rental_id_fkey");
            });

            modelBuilder.Entity<LoyaltyDiscount>(entity =>
            {
                entity.HasKey(e => e.DiscountId)
                    .HasName("loyalty_discounts_pkey");

                entity.ToTable("loyalty_discounts");

                entity.Property(e => e.DiscountId).HasColumnName("discount_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.DiscountPercent)
                    .HasPrecision(5, 2)
                    .HasColumnName("discount_percent");

                entity.Property(e => e.Reason)
                    .HasMaxLength(100)
                    .HasColumnName("reason");

                entity.Property(e => e.ValidFrom).HasColumnName("valid_from");

                entity.Property(e => e.ValidUntil).HasColumnName("valid_until");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.LoyaltyDiscounts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("loyalty_discounts_customer_id_fkey");
            });

            modelBuilder.Entity<MaintenanceRecord>(entity =>
            {
                entity.HasKey(e => e.MaintenanceId)
                    .HasName("maintenance_records_pkey");

                entity.ToTable("maintenance_records");

                entity.Property(e => e.MaintenanceId).HasColumnName("maintenance_id");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.Cost)
                    .HasPrecision(10, 2)
                    .HasColumnName("cost");

                entity.Property(e => e.ServiceDate).HasColumnName("service_date");

                entity.Property(e => e.WorkDescription)
                    .HasMaxLength(100)
                    .HasColumnName("work_description");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.MaintenanceRecords)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("maintenance_records_car_id_fkey");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.ToTable("models");

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.Class)
                    .HasMaxLength(20)
                    .HasColumnName("class");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Models)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("models_brand_id_fkey");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.OperationType)
                    .HasMaxLength(20)
                    .HasColumnName("operation_type");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("payment_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(20)
                    .HasColumnName("payment_method");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                entity.HasOne(d => d.Rental)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.RentalId)
                    .HasConstraintName("payments_rental_id_fkey");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ReservationId)
                    .HasConstraintName("payments_reservation_id_fkey");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("persons");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("rentals");

                entity.HasIndex(e => new { e.CarId, e.PickupDate, e.ActualReturnDate }, "idx_rentals_car_dates");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.ActualReturnDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("actual_return_date");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.ExpectedReturnDate).HasColumnName("expected_return_date");

                entity.Property(e => e.FinalCost)
                    .HasPrecision(10, 2)
                    .HasColumnName("final_cost");

                entity.Property(e => e.MileageAtPickup).HasColumnName("mileage_at_pickup");

                entity.Property(e => e.MileageAtReturn).HasColumnName("mileage_at_return");

                entity.Property(e => e.PickupDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("pickup_date");

                entity.Property(e => e.RentalPeriod).HasColumnName("rental_period");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                entity.Property(e => e.ReturnCondition).HasColumnName("return_condition");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rentals_car_id_fkey");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("rentals_reservation_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rentals_user_id_fkey");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("reservations");

                entity.HasIndex(e => new { e.StartDate, e.EndDate }, "idx_reservations_dates");

                entity.Property(e => e.ReservationId).HasColumnName("reservation_id");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.EstimatedTotalCost)
                    .HasPrecision(10, 2)
                    .HasColumnName("estimated_total_cost");

                entity.Property(e => e.PrepaymentAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("prepayment_amount");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservations_car_id_fkey");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservations_status_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservations_user_id_fkey");
            });

            modelBuilder.Entity<ReservationStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("reservation_statuses_pkey");

                entity.ToTable("reservation_statuses");

                entity.HasIndex(e => e.Name, "reservation_statuses_name_key")
                    .IsUnique();

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.HasIndex(e => e.Name, "roles_name_key")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.EmployeeCode, "users_employee_code_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate).HasColumnName("birth_date");

                entity.Property(e => e.DriverLicenseNumber)
                    .HasMaxLength(20)
                    .HasColumnName("driver_license_number");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.EmployeeCode)
                    .HasMaxLength(20)
                    .HasColumnName("employee_code");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.HireDate).HasColumnName("hire_date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.LicenseCategory)
                    .HasMaxLength(5)
                    .HasColumnName("license_category")
                    .HasDefaultValueSql("'B'::character varying");

                entity.Property(e => e.LicenseIssueDate).HasColumnName("license_issue_date");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .HasColumnName("phone");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("registration_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_role_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
