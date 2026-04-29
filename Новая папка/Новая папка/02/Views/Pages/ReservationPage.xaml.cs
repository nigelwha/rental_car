using System.Windows;
using System.Windows.Controls;
using _02.ViewModels;

namespace _02.Views.Pages
{
    public partial class ReservationPage : UserControl
    {
        public ReservationPage()
        {
            InitializeComponent();
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ReservationViewModel vm)
            {
                if (vm.MakeReservation())
                {
                    MessageBox.Show("Бронирование успешно создано!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Проверьте выбранные данные.", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}