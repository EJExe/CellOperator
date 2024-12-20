using System;
using System.ComponentModel;
using System.Windows;
using WPF.Models;
using WPF.Views;

namespace WPF.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        public MainMenuView currentwindow;
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

        private DBAccess dbAccess;

        public MainMenuViewModel(UserClass user, Window thisWindow)
        {
            dbAccess = new DBAccess();
            CurrentUser = user;
            if (user.IDTarif.HasValue)
            {
                TarifClass tariff = dbAccess.GetTariffById(user.IDTarif.Value);
                if (tariff != null)
                {
                    CurrentUser.TariffName = tariff.Name;
                }
            }

            TopUpBalanceCommand = new Command(obj => TopUpBalance(thisWindow));
            ChangeTariffCommand = new Command(obj => ChangeTariff(thisWindow));
            EditProfileCommand = new Command(obj => EditProfile(thisWindow));
            OpenSettingsCommand = new Command(obj => OpenSettings(thisWindow));
            ConnectServicesCommand = new Command(obj => OpenServices(thisWindow));
            LogoutCommand = new Command(obj => Logout(thisWindow));
        }

        public Command TopUpBalanceCommand { get; set; }
        public Command ChangeTariffCommand { get; set; }
        public Command EditProfileCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command OpenSettingsCommand { get; set; }
        public Command ConnectServicesCommand { get; set; }

        private void TopUpBalance(Window mainWindow)
        {
            TopUpBalanceView topUpBalanceView = new TopUpBalanceView(CurrentUser, mainWindow);
            currentwindow.Hide(); // Скрываем основное окно
            topUpBalanceView.ShowDialog();
            currentwindow.Show();

            // Подписываемся на событие изменения баланса
            if (topUpBalanceView.DataContext is TopUpBalanceViewModel topUpBalanceViewModel)
            {
                topUpBalanceViewModel.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "Amount")
                    {
                        CurrentUser.Balance += topUpBalanceViewModel.Amount;
                    }
                };
            }
        }

        private void ChangeTariff(Window mainWindow)
        {
            TarifMenuView tarifMenuView = new TarifMenuView(CurrentUser, mainWindow);
            currentwindow.Hide(); // Скрываем основное окно
            tarifMenuView.ShowDialog();
            currentwindow.Show();

            // Подписываемся на событие изменения тарифа
            if (tarifMenuView.DataContext is TarifMenuViewModel tarifMenuViewModel)
            {
                tarifMenuViewModel.TariffChanged += (updatedUser) =>
                {
                    CurrentUser = updatedUser;
                };
            }
        }

        private void EditProfile(Window mainWindow)
        {
            EditProfileView editProfileView = new EditProfileView(CurrentUser, mainWindow);
            currentwindow.Hide(); // Скрываем основное окно
            editProfileView.ShowDialog();
            currentwindow.Show();

            
        }

        private void OpenSettings(Window mainWindow)
        {
            SettingsView settingsView = new SettingsView(CurrentUser, mainWindow);
            currentwindow.Hide(); // Скрываем основное окно
            settingsView.ShowDialog();
            currentwindow.Show();
        }

        private void OpenServices(Window mainWindow)
        {
            ServicesView servicesView = new ServicesView(CurrentUser, mainWindow);
            currentwindow.Hide(); // Скрываем основное окно
            servicesView.ShowDialog();
            currentwindow.Show();
        }

        private void Logout(Window mainWindow)
        {
            MainWindow authorizationWindow = new MainWindow();
            authorizationWindow.Show(); // Открываем окно авторизации
            currentwindow.Close(); // Закрываем текущее окно
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}