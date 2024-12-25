using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    public class TariffManagementViewModel : INotifyPropertyChanged
    {
        private DBAccess dbAccess;
        public ObservableCollection<TarifClass> Tariffs { get; set; }
        private TarifClass selectedTariff;
        public TarifClass SelectedTariff
        {
            get { return selectedTariff; }
            set
            {
                selectedTariff = value;
                OnPropertyChanged("SelectedTariff");
            }
        }

        public TariffManagementViewModel(int userType)
        {
            dbAccess = new DBAccess();
            LoadTariffs(userType);
            ConfirmChangesCommand = new Command(obj => ConfirmChanges());
            AddTariffCommand = new Command(obj => AddTariff());
            DeleteTariffCommand = new Command(obj => DeleteTariff());
            ExitCommand = new Command(obj => Exit());
        }

        private void LoadTariffs(int userType)
        {
            Tariffs = new ObservableCollection<TarifClass>(dbAccess.GetAllTariffsForTable());
        }

        public Command ConfirmChangesCommand { get; set; }
        public Command AddTariffCommand { get; set; }
        public Command DeleteTariffCommand { get; set; }
        public Command ExitCommand { get; set; }

        private void ConfirmChanges()
        {
            if (SelectedTariff != null)
            {
                // Логика сохранения изменений
                dbAccess.UpdateTariff(SelectedTariff);
                MessageBox.Show("Изменения Применены!");
            }
            else
            {
                MessageBox.Show("Не выбран Тариф!");
            }
        }

        private void AddTariff()
        {
            // Логика добавления нового тарифа
            TarifClass newTariff = new TarifClass
            {
                Name = "New Tariff",
                PricePerMonth = 0,
                PriceGorod = 0,
                PriceMejGorod = 0,
                PriceMejNarod = 0
            };
            dbAccess.AddTariff(newTariff);
            Tariffs.Add(newTariff);
            MessageBox.Show("Новый тариф добавлен!");
        }

        private void DeleteTariff()
        {
            if (SelectedTariff != null)
            {
                // Логика удаления выбранного тарифа
                dbAccess.DeleteTariff(SelectedTariff.Id);
                Tariffs.Remove(SelectedTariff);
                MessageBox.Show("Тариф Удален");
            }
            else
            {
                MessageBox.Show("Тариф не выбран!");
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