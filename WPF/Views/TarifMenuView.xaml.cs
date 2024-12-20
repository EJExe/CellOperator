using System.Windows;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class TarifMenuView : Window
    {
        private TarifMenuViewModel viewModel;
        private Window mainWindow;

        public TarifMenuView(UserClass user, Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            viewModel = new TarifMenuViewModel(user);
            DataContext = viewModel;

            // Центрируем окно на экране
            this.CenterWindowOnScreen();

            // Подписываемся на событие Closed
            this.Closed += TarifMenuView_Closed;
            
        }

        private void OnTariffSelected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedTariff = (sender as FrameworkElement).DataContext as TarifClass;
            if (selectedTariff != null)
            {
                viewModel.ChangeTariff(selectedTariff);
            }
        }

        private void OnReturnToMainMenu(object sender, RoutedEventArgs e)
        {
            viewModel.UpdateTariff(); // Обновляем тариф перед возвратом в главное меню
            this.Close();
        }

        private void TarifMenuView_Closed(object sender, System.EventArgs e)
        {
            if (mainWindow != null && mainWindow.IsLoaded)
            {
                mainWindow.Show(); // Показываем основное окно после закрытия окна смены тарифа
            }
        }
    }
}