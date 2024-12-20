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
using SystemOpMobComm.ViewModels;
using WPF.Extensions;
using WPF.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : Window
    {
        private UserClass currentUser;
        public UserClass CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }

        public Command TopUpBalanceCommand { get; set; }
        public Command ChangeTariffCommand { get; set; }
        public Command EditProfileCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public string PhoneNumber { get; set; }

        public MainMenuView(UserClass user)
        {
            InitializeComponent();
            PhoneNumber = user.PhoneNumber;
            DataContext = new MainMenuViewModel(user,this);
            CurrentUser = user;
            TopUpBalanceCommand = new Command(obj => TopUpBalance());
            ChangeTariffCommand = new Command(obj => ChangeTariff());
            EditProfileCommand = new Command(obj => EditProfile());
            LogoutCommand = new Command(obj => Logout());
            // Центрируем окно на экране
            this.CenterWindowOnScreen();
        }


        private void TopUpBalance()
        {
            // Логика пополнения баланса
        }

        private void ChangeTariff()
        {
            // Логика изменения тарифа
        }

        private void EditProfile()
        {
            // Логика редактирования профиля
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
