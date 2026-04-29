using _02.Interfaces;
using _02.ViewModels;
using System.Windows;

namespace _02.Views
{
    public partial class AuthorizationWindow : Window, IGettingPassword
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            if (DataContext is AuthorizationViewModel viewModel)
                viewModel.GettingPassword = this;
        }

        public string GetPassword() => PasswordBox.Password.Trim();

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthorizationViewModel viewModel)
            {
                if (viewModel.LogIn())
                {
                    var currentUser = AuthorizationViewModel.CurrentUser;
                    MessageBox.Show(
                        $"Добро пожаловать, {currentUser?.FirstName} {currentUser?.LastName}!",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    PasswordBox.Clear();
                }
            }
        }
    }
}