using System.Windows;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class CallView : Window
    {
        private Window mainWindow;

        public CallView(UserClass user, Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            DataContext = new CallViewModel(user);
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }

        private void CallView_Closed(object sender, System.EventArgs e)
        {
            if (mainWindow != null && mainWindow.IsLoaded)
            {
                mainWindow.Show(); // Показываем основное окно после закрытия окна звонка
            }
        }
    }
}