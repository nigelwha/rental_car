using _02.Views;

namespace _02.Services
{
    public class DialogService : IDialogService
    {
        public DialogResult ShowDialog(string message, string title, DialogType type)
        {
            var dialog = new DialogWindow(message, title, type);
            bool? result = dialog.ShowDialog();

            if (result == null) return DialogResult.Ok;
            return result == true ? DialogResult.Yes : DialogResult.No;
        }
    }
}