using System.Windows;
using System.Windows.Controls;

namespace _02.Views.Pages
{
    public partial class CarsPage : UserControl
    {
        public CarsPage()
        {
            InitializeComponent();
        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.CarsViewModel vm)
                vm.ApplyFilters();
        }

        private void ResetFilters_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.CarsViewModel vm)
                vm.ResetFilters();
        }
    }
}