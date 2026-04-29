using _02.Data;
using _02.Models;
using _02.Services;
using Buro.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace _02.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private readonly ApplicationDbContext _context;
        public ObservableCollection<Car> AvailableCars { get; set; } = new();
        private Car? _selectedCar;
        private DateTime _startDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today.AddDays(1);
        private decimal _estimatedCost;

        public Car? SelectedCar
        {
            get => _selectedCar;
            set { _selectedCar = value; OnPropertyChanged(); CalculateCost(); }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); CalculateCost(); }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); CalculateCost(); }
        }

        public decimal EstimatedCost
        {
            get => _estimatedCost;
            set { _estimatedCost = value; OnPropertyChanged(); }
        }

        public ReservationViewModel()
        {
            Title = "Бронирование автомобиля";
            _context = new ApplicationDbContext();
            LoadAvailableCars();
        }

        private void LoadAvailableCars()
        {
            var cars = _context.Cars
                .Where(c => c.Status.Name == "Available")
                .ToList();
            AvailableCars = new ObservableCollection<Car>(cars);
        }

        private void CalculateCost()
        {
            if (SelectedCar != null && EndDate > StartDate)
            {
                int days = (EndDate - StartDate).Days;
                EstimatedCost = SelectedCar.BaseRentalRatePerDay * days;
            }
            else
            {
                EstimatedCost = 0;
            }
        }

        public bool MakeReservation()
        {
            if (SelectedCar == null || StartDate >= EndDate)
                return false;

            var reservation = new Reservation
            {
                UserId = AuthorizationViewModel.CurrentUser!.UserId,
                CarId = SelectedCar.CarId,
                StartDate = DateOnly.FromDateTime(StartDate),
                EndDate = DateOnly.FromDateTime(EndDate),
                StatusId = 1, // Awaiting Payment
                PrepaymentAmount = 0,
                EstimatedTotalCost = EstimatedCost
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return true;
        }
    }
}