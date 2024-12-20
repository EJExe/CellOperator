using DAL.Entity;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    public class ServicesViewModel : INotifyPropertyChanged
    {
        private DBAccess dbAccess;
        private UserClass currentUser;

        public ObservableCollection<TypeOfYslygiTable> AllServices { get; set; }
        public ObservableCollection<TypeOfYslygiTable> UserServices { get; set; }

        public Command ConnectServiceCommand { get; set; }
        public Command DisconnectServiceCommand { get; set; }
        public Command ReturnToMainMenuCommand { get; set; }

        public ServicesViewModel(UserClass user)
        {
            dbAccess = new DBAccess();
            currentUser = user;
            LoadAllServices();
            LoadUserServices(user);

            ConnectServiceCommand = new Command(obj => ConnectService((TypeOfYslygiTable)obj));
            DisconnectServiceCommand = new Command(obj => DisconnectService((TypeOfYslygiTable)obj));
            ReturnToMainMenuCommand = new Command(obj => ReturnToMainMenu());
        }

        private void LoadAllServices()
        {
            // Загрузка всех услуг из базы данных
            AllServices = new ObservableCollection<TypeOfYslygiTable>(dbAccess.GetAllServices());
        }

        private void LoadUserServices(UserClass user)
        {
            // Загрузка услуг, подключенных пользователем
            UserServices = new ObservableCollection<TypeOfYslygiTable>(dbAccess.GetUserServices(user.PhoneNumber));
        }

        private void ConnectService(TypeOfYslygiTable service)
        {
            if (UserServices.Any(s => s.C__PK__Kod_Type_Of_Yslygi == service.C__PK__Kod_Type_Of_Yslygi))
            {
                MessageBox.Show("Услуга уже подключена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dbAccess.ConnectService(currentUser.PhoneNumber, service.C__PK__Kod_Type_Of_Yslygi);
            UserServices.Add(service);
        }

        private void DisconnectService(TypeOfYslygiTable service)
        {
            if (!UserServices.Any(s => s.C__PK__Kod_Type_Of_Yslygi == service.C__PK__Kod_Type_Of_Yslygi))
            {
                MessageBox.Show("Услуга не подключена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            dbAccess.DisconnectService(currentUser.PhoneNumber, service.C__PK__Kod_Type_Of_Yslygi);
            UserServices.Remove(service);
        }

        private void ReturnToMainMenu()
        {
            // Логика для возврата в главное меню
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (window != null)
            {
                window.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}