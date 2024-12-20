using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.Extensions;
using WPF.ViewModels;

namespace WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminMainMenuView.xaml
    /// </summary>
    public partial class AdminMainMenuView : Window
    {
        public Command LogoutCommand { get; set; }
        public AdminMainMenuView()
        {
            InitializeComponent();
            LogoutCommand = new Command(obj => Logout());
            // Центрируем окно на экране
            this.CenterWindowOnScreen();

        }

        private void Logout()
        {
            // Логика выхода из системы
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
