using System.Windows;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class SettingsView : Window
    {
        private Window mainWindow;

        public SettingsView(UserClass user, Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            DataContext = new SettingsViewModel(user, mainWindow);
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }

        private void SettingsView_Closed(object sender, System.EventArgs e)
        {
            if (mainWindow != null && mainWindow.IsLoaded)
            {
                mainWindow.Show(); // Показываем основное окно после закрытия окна настроек
            }
        }

    }
}