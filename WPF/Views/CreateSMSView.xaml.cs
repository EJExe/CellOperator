using System.Windows;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class CreateSMSView : Window
    {
        private Window mainWindow;

        public CreateSMSView(UserClass user, Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            DataContext = new CreateSMSViewModel(user);
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }

        private void CreateSMSView_Closed(object sender, System.EventArgs e)
        {
            if (mainWindow != null && mainWindow.IsLoaded)
            {
                mainWindow.Show(); // Показываем основное окно после закрытия окна создания SMS
            }
        }
    }
}