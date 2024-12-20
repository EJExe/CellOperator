using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    public class TarifMenuViewModel : INotifyPropertyChanged
    {
        private DBAccess dbAccess;
        public ObservableCollection<TarifClass> Tariffs { get; set; }
        private UserClass currentUser;

        public event Action<UserClass> TariffChanged;

        public TarifMenuViewModel(UserClass user)
        {
            dbAccess = new DBAccess();
            currentUser = user;
            LoadTariffs(user.UserType.Value);
        }

        private void LoadTariffs(int userType)
        {
            Tariffs = new ObservableCollection<TarifClass>(dbAccess.GetAllTariffs(userType));
        }

        public void ChangeTariff(TarifClass selectedTariff)
        {
            MessageBoxResult result = MessageBox.Show($"Do you want to change your tariff to {selectedTariff.Name}?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dbAccess.UpdateUserTariff(currentUser.PhoneNumber, selectedTariff.Id);
                MessageBox.Show("Tariff changed successfully!");

                // Обновляем текущего пользователя
                currentUser.IDTarif = selectedTariff.Id;
                currentUser.TariffName = selectedTariff.Name;

                // Уведомляем о изменении тарифа
                TariffChanged?.Invoke(currentUser);
            }
        }

        public void UpdateTariff()
        {
            // Обновляем текущего пользователя
            TarifClass tariff = dbAccess.GetTariffById(currentUser.IDTarif.Value);
            if (tariff != null)
            {
                currentUser.TariffName = tariff.Name;
            }

            // Уведомляем о изменении тарифа
            TariffChanged?.Invoke(currentUser);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}