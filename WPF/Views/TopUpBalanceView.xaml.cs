using System.Windows;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class TopUpBalanceView : Window
    {
        private TopUpBalanceViewModel viewModel;
        private Window mainWindow;

        public TopUpBalanceView(UserClass user, Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            viewModel = new TopUpBalanceViewModel(user);
            DataContext = viewModel;
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }

        private void OnConfirmTopUp(object sender, RoutedEventArgs e)
        {
            viewModel.ConfirmTopUp();
            this.Close();
        }

        private void OnReturnToMainMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TopUpBalanceView_Closed(object sender, System.EventArgs e)
        {
            if (mainWindow != null && mainWindow.IsLoaded)
            {
                mainWindow.Show(); // Показываем основное окно после закрытия окна пополнения баланса
            }
        }
    }
}