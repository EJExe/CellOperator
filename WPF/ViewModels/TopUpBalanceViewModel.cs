using System;
using System.ComponentModel;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    public class TopUpBalanceViewModel : INotifyPropertyChanged
    {

        private UserClass currentUser;
        private DBAccess dbAccess;

        public string CardNumber { get; set; }
        public string CvcCode { get; set; }
        public string ExpiryDate { get; set; }
        public int Amount { get; set; }

        public TopUpBalanceViewModel(UserClass user)
        {
            currentUser = user;
            dbAccess = new DBAccess();
        }

        

        public void ConfirmTopUp()
        {
            // Логика пополнения баланса
            if (ValidateCardDetails())
            {
                dbAccess.UpdateUserBalance(currentUser.PhoneNumber, Amount);
                MessageBox.Show("Баланс Пополнен!");
            }
            else
            {
                MessageBox.Show("Не корректные данные!");
            }
        }

        private bool ValidateCardDetails()
        {
            // Простая валидация карточных данных
            return !string.IsNullOrEmpty(CardNumber) &&
                   !string.IsNullOrEmpty(CvcCode) &&
                   !string.IsNullOrEmpty(ExpiryDate) &&
                   Amount > 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}