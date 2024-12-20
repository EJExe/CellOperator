using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        private DBAccess dbAccess;
        public ObservableCollection<UserClass> Users { get; set; }
        private UserClass selectedUser;
        public UserClass SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public UserManagementViewModel()
        {
            dbAccess = new DBAccess();
            LoadUsers();
            ConfirmChangesCommand = new Command(obj => ConfirmChanges());
            AddUserCommand = new Command(obj => AddUser());
            DeleteUserCommand = new Command(obj => DeleteUser());
            TopUpBalanceCommand = new Command(obj => TopUpBalance());
            ExitCommand = new Command(obj => Exit());
        }

        private void LoadUsers()
        {
            Users = new ObservableCollection<UserClass>(dbAccess.GetAllUsers());
            Debug.WriteLine($"Loaded {Users.Count} users into the view model.");
        }

        public Command ConfirmChangesCommand { get; set; }
        public Command AddUserCommand { get; set; }
        public Command DeleteUserCommand { get; set; }
        public Command TopUpBalanceCommand { get; set; }
        public Command ExitCommand { get; set; }

        private void ConfirmChanges()
        {
            if (SelectedUser != null)
            {
                // Логика сохранения изменений
                dbAccess.UpdateUserProfile(SelectedUser.PhoneNumber, SelectedUser.Name, SelectedUser.Loging, SelectedUser.Password);
                MessageBox.Show("Changes confirmed!");
            }
            else
            {
                MessageBox.Show("No user selected!");
            }
        }

        private void AddUser()
        {
            try
            {
                UserClass newUser = new UserClass
                {
                    Name = "New User",
                    PhoneNumber = "New Phone Number",
                    Balance = 0,
                    IDTarif=2,
                    TariffName = " "

                };

                dbAccess.AddUser(newUser);
                Users.Add(newUser);
                MessageBox.Show("New user added!");
                Debug.WriteLine("New user added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
                Debug.WriteLine($"Error adding user: {ex.Message}");
            }
        }

        private void DeleteUser()
        {
            if (SelectedUser != null)
            {
                // Логика удаления выбранного пользователя
                dbAccess.DeleteUser(SelectedUser.PhoneNumber);
                Users.Remove(SelectedUser);
                MessageBox.Show("User deleted!");
            }
            else
            {
                MessageBox.Show("No user selected!");
            }
        }

        private void TopUpBalance()
        {
            if (SelectedUser != null)
            {
                // Логика пополнения баланса
                string phoneNumber = SelectedUser.PhoneNumber;
                int amount = 100; // Пример суммы пополнения
                dbAccess.UpdateUserBalance(phoneNumber, amount);
                SelectedUser.Balance += amount;
                MessageBox.Show("Balance topped up!");
            }
            else
            {
                MessageBox.Show("No user selected!");
            }
        }

        private void Exit()
        {
            // Закрытие окна
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