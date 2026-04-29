using _02.ViewModels;
using _02.Views;
using System.Windows;

namespace _02
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationViewModel.LogOut();
            var authWindow = new AuthorizationWindow();
            authWindow.Show();
            this.Close();
        }

        protected override void OnClosed(System.EventArgs e)
        {
            base.OnClosed(e);
            if (AuthorizationViewModel.CurrentUser != null)
            {
                AuthorizationViewModel.LogOut();
            }
        }
    }
}