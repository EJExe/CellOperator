using System;
using System.ComponentModel;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    public class EditProfileViewModel : INotifyPropertyChanged
    {
        private UserClass currentUser;
        private DBAccess dbAccess;

        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public EditProfileViewModel(UserClass user)
        {
            currentUser = user;
            dbAccess = new DBAccess();

            // Инициализация полей данными текущего пользователя
            FullName = user.Name;
            Login = user.Loging;
            Password = user.Password;
        }

        public void ConfirmEdit()
        {
            // Логика редактирования профиля
            if (ValidateProfileDetails())
            {
                dbAccess.UpdateUserProfile(currentUser.PhoneNumber, FullName, Login, Password);
                MessageBox.Show("Профиль Обновлен!");
            }
            else
            {
                MessageBox.Show("Не корректные данные!");
            }
        }

        private bool ValidateProfileDetails()
        {
            // Простая валидация данных профиля
            return !string.IsNullOrEmpty(FullName) &&
                   !string.IsNullOrEmpty(Login);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}