using System.Windows;
using WPF.Extensions;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class UserManagementView : Window
    {
        public UserManagementView()
        {
            InitializeComponent();
            DataContext = new UserManagementViewModel();
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }
    }
}