using Buro.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace _02.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _viewModelsCollection = new();
        private ViewModelBase? _selectedViewModel;

        public static _02.Models.User? CurrentUser => AuthorizationViewModel.CurrentUser;

        public ObservableCollection<ViewModelBase> ViewModelsCollection
        {
            get => _viewModelsCollection;
            set { _viewModelsCollection = value; OnPropertyChanged(); }
        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set { if (_selectedViewModel != value) { _selectedViewModel = value; OnPropertyChanged(); } }
        }

        public string UserFullName => $"{CurrentUser?.LastName} {CurrentUser?.FirstName}";
        public string UserRole => CurrentUser?.Role?.Name ?? "Роль не указана";

        public MainWindowViewModel()
        {
            Title = "Главное окно";
            InitializeViewModels();
        }

        private void InitializeViewModels()
        {
            ViewModelsCollection = new ObservableCollection<ViewModelBase>
            {
                new CarsViewModel()
            };

            // В зависимости от роли добавляем разделы
            var role = CurrentUser?.Role?.Name;
            if (role == "Клиент")
            {
                ViewModelsCollection.Add(new ReservationViewModel());
                ViewModelsCollection.Add(new FinesViewModel());
            }
            else if (role == "Менеджер" || role == "Администратор")
            {
                ViewModelsCollection.Add(new RentalsViewModel());
                ViewModelsCollection.Add(new FinesViewModel());
                ViewModelsCollection.Add(new ClientsViewModel());
            }
            if (role == "Администратор")
            {
                ViewModelsCollection.Add(new RegisterEmployeeViewModel());
            }

            SelectedViewModel = ViewModelsCollection.FirstOrDefault();
        }
    }
}