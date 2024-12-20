using System.Windows;
using SystemOpMobComm.ViewModels;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class MainWindow : Window
    {
        private bool isTariffMenuOpen = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new AuthorizationViewModel(this);
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }

        private void ChangeTariff(UserClass user)
        {
            isTariffMenuOpen = true;
            TarifMenuView tarifMenuView = new TarifMenuView(user, this);
            this.Hide(); // Скрываем основное окно
            tarifMenuView.ShowDialog();
            isTariffMenuOpen = false;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (isTariffMenuOpen)
            {
                e.Cancel = true; // Отменяем закрытие основного окна, если открыто окно смены тарифа
            }
            base.OnClosing(e);
        }
    }
}