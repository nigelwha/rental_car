using _02.Services;
using System.Windows;

namespace _02
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceContainer.Instance.Add<IDialogService>(new DialogService());
        }
    }
}
