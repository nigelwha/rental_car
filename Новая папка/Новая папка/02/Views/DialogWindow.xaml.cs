using _02.Services;
using System.Windows;
using System.Windows.Controls;

namespace _02.Views
{
    public partial class DialogWindow : Window
    {
        public DialogType TypeDialog { get; private set; }

        public DialogWindow(string message, string title, DialogType type)
        {
            TypeDialog = type;
            InitializeComponent();

            TitleBlock.Text = title;
            MessageBlock.Text = message;

            if (type == DialogType.InfoType || type == DialogType.ErrorType)
            {
                var btn = new Button { Content = "OK" };
                btn.Click += OkButton_Click;
                btn.Style = type == DialogType.InfoType
                    ? (Style)Resources["OkButtonStyle"]
                    : (Style)Resources["OkErrorButtonStyle"];
                ButtonsContent.Content = btn;
            }
            else // QuestionType
            {
                var panel = new StackPanel { Orientation = Orientation.Horizontal };

                var yesBtn = new Button { Content = "Да", Margin = new Thickness(0, 0, 8, 0) };
                yesBtn.Style = (Style)Resources["YesButtonStyle"];
                yesBtn.Click += YesButton_Click;

                var noBtn = new Button { Content = "Нет" };
                noBtn.Style = (Style)Resources["NoButtonStyle"];
                noBtn.Click += NoButton_Click;

                panel.Children.Add(yesBtn);
                panel.Children.Add(noBtn);
                ButtonsContent.Content = panel;
            }
        }

        private void YesButton_Click(object sender, RoutedEventArgs e) { DialogResult = true; Close(); }
        private void NoButton_Click(object sender, RoutedEventArgs e) { DialogResult = false; Close(); }
        private void OkButton_Click(object sender, RoutedEventArgs e) { DialogResult = null; Close(); }
    }
}