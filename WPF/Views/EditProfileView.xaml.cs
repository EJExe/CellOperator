using System.Windows;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class EditProfileView : Window
    {
        private EditProfileViewModel viewModel;
        private Window mainWindow;

        public EditProfileView(UserClass user, Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            viewModel = new EditProfileViewModel(user);
            DataContext = viewModel;
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }

        private void OnConfirmEdit(object sender, RoutedEventArgs e)
        {
            viewModel.ConfirmEdit();
            this.Close();
        }

        private void OnReturnToMainMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditProfileView_Closed(object sender, System.EventArgs e)
        {
            if (mainWindow != null && mainWindow.IsLoaded)
            {
                mainWindow.Show(); // Показываем основное окно после закрытия окна редактирования профиля
            }
        }
    }
}