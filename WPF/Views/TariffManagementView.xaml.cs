using System.Windows;
using WPF.Extensions;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class TariffManagementView : Window
    {
        public TariffManagementView(int userType)
        {
            InitializeComponent();
            DataContext = new TariffManagementViewModel(userType);
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }
    }
}