using System.Windows;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class ServicesView : Window
    {
        private Window mainWindow;

        public ServicesView(UserClass user, Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            DataContext = new ServicesViewModel(user);
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }

        private void ServicesView_Closed(object sender, System.EventArgs e)
        {
            if (mainWindow != null && mainWindow.IsLoaded)
            {
                mainWindow.Show(); // Показываем основное окно после закрытия окна услуг
            }
        }
    }
}