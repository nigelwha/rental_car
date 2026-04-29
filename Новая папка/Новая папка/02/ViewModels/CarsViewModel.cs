using _02.Data;
using _02.Models;
using _02.Services;
using Buro.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace _02.ViewModels
{
    public class CarsViewModel : ViewModelBase
    {
        private ObservableCollection<Car> _cars = new();
        private List<Car> _allCars = new();
        private string _searchString = string.Empty;
        private readonly ApplicationDbContext _context;

        // Фильтры
        private ObservableCollection<StatusFilter> _statusFilters = new();
        private ObservableCollection<string?> _classFilters = new();
        private string? _selectedClass;
        private decimal? _priceFrom;
        private decimal? _priceTo;

        // Сортировка
        private int _selectedSort;
        private ObservableCollection<string> _sortOptions = new();

        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set { _cars = value; OnPropertyChanged(); }
        }

        public string SearchString
        {
            get => _searchString;
            set { _searchString = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public ObservableCollection<StatusFilter> StatusFilters
        {
            get => _statusFilters;
            set { _statusFilters = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string?> ClassFilters
        {
            get => _classFilters;
            set { _classFilters = value; OnPropertyChanged(); }
        }

        public string? SelectedClass
        {
            get => _selectedClass;
            set { _selectedClass = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public decimal? PriceFrom
        {
            get => _priceFrom;
            set { _priceFrom = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public decimal? PriceTo
        {
            get => _priceTo;
            set { _priceTo = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public int SelectedSort
        {
            get => _selectedSort;
            set { _selectedSort = value; OnPropertyChanged(); ApplyFilters(); }
        }

        public ObservableCollection<string> SortOptions
        {
            get => _sortOptions;
            set { _sortOptions = value; OnPropertyChanged(); }
        }

        public bool ResetFilterActive =>
            StatusFilters.Any(f => f.IsChecked) ||
            SelectedClass != null ||
            PriceFrom.HasValue ||
            PriceTo.HasValue;

        public CarsViewModel()
        {
            Title = "Каталог автомобилей";
            _context = new ApplicationDbContext();
            LoadFilters();
            LoadCars();
        }

        private void LoadFilters()
        {
            // Статусы автомобилей
            var statuses = _context.CarStatuses.ToList();
            StatusFilters = new ObservableCollection<StatusFilter>(
                statuses.Select(s => new StatusFilter { Status = s, IsChecked = false }));

            // Классы автомобилей (берём из Model.Class)
            var classes = _context.Cars
                .Select(c => c.Model.Class)
                .Where(c => c != null)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
            ClassFilters = new ObservableCollection<string?>(classes);

            // Варианты сортировки
            SortOptions = new ObservableCollection<string>
            {
                "Без сортировки",
                "Госномер (А→Я)",
                "Госномер (Я→А)",
                "Марка и модель (А→Я)",
                "Марка и модель (Я→А)",
                "Год выпуска (по возрастанию)",
                "Год выпуска (по убыванию)",
                "Стоимость в сутки (по возрастанию)",
                "Стоимость в сутки (по убыванию)"
            };
        }

        private void LoadCars()
        {
            _allCars = _context.Cars
                .Include(c => c.Model)
                    .ThenInclude(m => m.Brand)
                .Include(c => c.Status)
                .ToList();
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            var result = _allCars.AsEnumerable();

            // Поиск (госномер, марка, модель)
            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                var s = SearchString.Trim().ToLower();
                result = result.Where(c =>
                    c.LicensePlate.ToLower().Contains(s) ||
                    c.Model.Brand.Name.ToLower().Contains(s) ||
                    c.Model.Name.ToLower().Contains(s));
            }

            // Статусы
            var activeStatuses = StatusFilters
                .Where(f => f.IsChecked)
                .Select(f => f.Status.Name)
                .ToList();
            if (activeStatuses.Count > 0)
            {
                result = result.Where(c => activeStatuses.Contains(c.Status.Name));
            }

            // Класс
            if (!string.IsNullOrEmpty(SelectedClass))
            {
                result = result.Where(c => c.Model.Class == SelectedClass);
            }

            // Цена
            if (PriceFrom.HasValue)
                result = result.Where(c => c.BaseRentalRatePerDay >= PriceFrom.Value);
            if (PriceTo.HasValue)
                result = result.Where(c => c.BaseRentalRatePerDay <= PriceTo.Value);

            // Сортировка
            switch (SelectedSort)
            {
                case 1: result = result.OrderBy(c => c.LicensePlate); break;
                case 2: result = result.OrderByDescending(c => c.LicensePlate); break;
                case 3: result = result.OrderBy(c => c.Model.Brand.Name).ThenBy(c => c.Model.Name); break;
                case 4: result = result.OrderByDescending(c => c.Model.Brand.Name).ThenByDescending(c => c.Model.Name); break;
                case 5: result = result.OrderBy(c => c.ManufactureYear); break;
                case 6: result = result.OrderByDescending(c => c.ManufactureYear); break;
                case 7: result = result.OrderBy(c => c.BaseRentalRatePerDay); break;
                case 8: result = result.OrderByDescending(c => c.BaseRentalRatePerDay); break;
                default: break;
            }

            var list = result.ToList();
            Cars = new ObservableCollection<Car>(list);

            if (list.Count == 0 && (!string.IsNullOrWhiteSpace(SearchString) || activeStatuses.Count > 0))
            {
                var dialog = ServiceContainer.Instance.Get<IDialogService>();
                dialog?.ShowDialog("По вашему запросу ничего не найдено.", "Поиск", DialogType.InfoType);
            }

            OnPropertyChanged(nameof(ResetFilterActive));
        }

        public void ResetFilters()
        {
            foreach (var filter in StatusFilters)
                filter.IsChecked = false;
            SelectedClass = null;
            PriceFrom = null;
            PriceTo = null;
            ApplyFilters();
        }
    }
}