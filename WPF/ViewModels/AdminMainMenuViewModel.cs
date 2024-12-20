using DAL.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPF.Models;
using WPF.Views;

namespace WPF.ViewModels
{
    public class AdminMainMenuViewModel : INotifyPropertyChanged
    {
        private DBAccess dbAccess;
        public AdminMainMenuView currentwindow;
        private UserClass currentUser;
        private ObservableCollection<BalanceHistoryTable> balanceHistory;
        private ObservableCollection<UserClass> phoneNumbers;
        private UserClass selectedPhoneNumber;
        private Visibility buttonsVisibility = Visibility.Visible;
        private Visibility tableVisibility = Visibility.Collapsed;
        private bool showBalanceManagementButtons = false; // Добавлено для управления видимостью кнопок списаний
        

        public ObservableCollection<BalanceHistoryTable> BalanceHistory
        {
            get => balanceHistory;
            set
            {
                balanceHistory = value;
                OnPropertyChanged(nameof(BalanceHistory));
            }
        }

        public ObservableCollection<UserClass> PhoneNumbers
        {
            get => phoneNumbers;
            set
            {
                phoneNumbers = value;
                OnPropertyChanged(nameof(PhoneNumbers));
            }
        }

        public UserClass SelectedPhoneNumber
        {
            get => selectedPhoneNumber;
            set
            {
                selectedPhoneNumber = value;
                OnPropertyChanged(nameof(SelectedPhoneNumber));
            }
        }

        public Visibility ButtonsVisibility
        {
            get => buttonsVisibility;
            set
            {
                buttonsVisibility = value;
                OnPropertyChanged(nameof(ButtonsVisibility));
            }
        }

        public Visibility TableVisibility
        {
            get => tableVisibility;
            set
            {
                tableVisibility = value;
                OnPropertyChanged(nameof(TableVisibility));
            }
        }

        public bool ShowBalanceManagementButtons
        {
            get => showBalanceManagementButtons;
            set
            {
                showBalanceManagementButtons = value;
                OnPropertyChanged(nameof(ShowBalanceManagementButtons));
            }
        }

        public AdminMainMenuViewModel(UserClass user)
        {
            dbAccess = new DBAccess(); // Инициализация dbAccess
            currentUser = user;
            LogoutCommand = new Command(obj => Logout(currentwindow));
            OpenTariffsCommand = new Command(obj => OpenTariffs(currentwindow));
            OpenUsersCommand = new Command(obj => OpenUsers(currentwindow));
            DeductBalanceCommand = new Command(obj => DeductBalance());
            LoadPhoneNumbers();

            ViewAllBalanceHistoryCommand = new Command(obj => ViewAllBalanceHistory());
            ViewUserBalanceHistoryCommand = new Command(obj => ViewUserBalanceHistory());
            ReturnToMenuCommand = new Command(obj => ReturnToMenu());
            ViewUsersCommand = new Command(obj => ViewUsers());
            ViewTariffsCommand = new Command(obj => ViewTariffs());
            ShowBalanceManagementCommand = new Command(obj => ShowBalanceManagement()); // Команда для кнопки "Список списаний"
            
        }

        public Command DeductBalanceCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command OpenTariffsCommand { get; set; }
        public Command OpenUsersCommand { get; set; }
        public Command ViewAllBalanceHistoryCommand { get; set; }
        public Command ViewUserBalanceHistoryCommand { get; set; }
        public Command ReturnToMenuCommand { get; set; }
        public Command ViewUsersCommand { get; set; }
        public Command ViewTariffsCommand { get; set; }
        public Command ShowBalanceManagementCommand { get; set; } // Команда для кнопки "Список списаний"

        public Command GenerateReportCommand { get; set; }

        private void Logout(Window mainWindow)
        {
            MainWindow authorizationWindow = new MainWindow();
            authorizationWindow.Show(); // Открываем окно авторизации
            currentwindow.Close(); // Закрываем текущее окно
        }

        private void LoadPhoneNumbers()
        {
            // Загружаем все номера телефонов пользователей
            var users = dbAccess.GetAllUsers();
            PhoneNumbers = new ObservableCollection<UserClass>(users);
        }

        private void ViewAllBalanceHistory()
        {
            // Получаем все записи из BalanceCounterHistoryTable
            dbAccess.GenerateBalanceHistoryReport();
            ButtonsVisibility = Visibility.Collapsed;
            TableVisibility = Visibility.Visible;
        }

        private void ViewUserBalanceHistory()
        {
            if (SelectedPhoneNumber == null)
            {
                MessageBox.Show("Пожалуйста, выберите пользователя из списка.");
                return;
            }

            // Получаем записи для выбранного пользователя
            BalanceHistory = new ObservableCollection<BalanceHistoryTable>(dbAccess.GetBalanceHistoryByUser(SelectedPhoneNumber.PhoneNumber));
            ButtonsVisibility = Visibility.Collapsed;
            TableVisibility = Visibility.Visible;
        }

        private void ReturnToMenu()
        {
            // Возвращаемся к меню
            ButtonsVisibility = Visibility.Visible;
            TableVisibility = Visibility.Collapsed;
            BalanceHistory = null;
            ShowBalanceManagementButtons = false; // Скрываем кнопки для управления списаниями
        }

        private void DeductBalance()
        {
            // Получаем всех пользователей из базы данных
            var allUsers = dbAccess.GetAllUsers();

            foreach (var user in allUsers)
            {
                // Пропускаем пользователей с тарифом 3
                if (user.IDTarif == 3)
                {
                    continue;
                }

                // Списание средств за тариф
                if (user.IDTarif != null)
                {
                    dbAccess.DeductTariffCost(user.PhoneNumber, user.IDTarif.Value);
                }

                // Списание средств за услуги
                var userServices = dbAccess.GetUserServices(user.PhoneNumber);
                foreach (var service in userServices)
                {
                    dbAccess.DeductServiceCost(user.PhoneNumber, service.C__PK__Kod_Type_Of_Yslygi);
                }
            }

            MessageBox.Show("Средства успешно списаны у всех пользователей.");
        }

        private void OpenTariffs(Window mainWindow)
        {
            TariffManagementView tariffManagementView = new TariffManagementView(currentUser.UserType.Value);
            tariffManagementView.ShowDialog();
        }

        private void OpenUsers(Window mainWindow)
        {
            UserManagementView userManagementView = new UserManagementView();
            userManagementView.ShowDialog();
        }

        private void ViewUsers()
        {
            var users = dbAccess.GetAllUsers();
            BalanceHistory = new ObservableCollection<BalanceHistoryTable>(users.Select(u => new BalanceHistoryTable
            {
                Numbe_Of_Phone = u.PhoneNumber,
                Amount = u.Balance ?? 0
            }).ToList());
            ButtonsVisibility = Visibility.Collapsed;
            TableVisibility = Visibility.Visible;
        }

        private void ViewTariffs()
        {
            var tariffs = dbAccess.GetAllTariffs(currentUser.UserType.Value);
            BalanceHistory = new ObservableCollection<BalanceHistoryTable>(tariffs.Select(t => new BalanceHistoryTable
            {
                Kod_Tarifa = t.Id,
                Amount = t.PricePerMonth ?? 0
            }).ToList());
            ButtonsVisibility = Visibility.Collapsed;
            TableVisibility = Visibility.Visible;
        }

        private void ShowBalanceManagement()
        {
            // Показываем кнопки для управления списаниями
            ShowBalanceManagementButtons = true;
            ButtonsVisibility = Visibility.Collapsed; // Скрываем кнопки меню администратора
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}