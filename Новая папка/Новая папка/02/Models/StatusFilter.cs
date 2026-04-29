using System.ComponentModel;

namespace _02.Models
{
    public class StatusFilter : INotifyPropertyChanged
    {
        private bool _isChecked;
        public CarStatus Status { get; set; } = null!;
        public bool IsChecked
        {
            get => _isChecked;
            set { _isChecked = value; OnPropertyChanged(nameof(IsChecked)); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}