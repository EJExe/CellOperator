using DAL.Entity;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    public class CallViewModel : INotifyPropertyChanged
    {
        private UserClass currentUser;
        private DBAccess dbAccess;
        private Stopwatch callTimer;
        private DateTime callStartTime;
        private TarifClass userTariff;

        public string ReceiverPhoneNumber { get; set; }
        public ObservableCollection<TypeOfCallTable> CallTypes { get; set; }
        public TypeOfCallTable SelectedCallType { get; set; }

        public Command StartCallCommand { get; set; }
        public Command EndCallCommand { get; set; }
        public Command ExitToSettingsCommand { get; set; }

        public CallViewModel(UserClass user)
        {
            currentUser = user;
            dbAccess = new DBAccess();
            callTimer = new Stopwatch();

            // Загрузка тарифа пользователя
            userTariff = dbAccess.GetTariffById(currentUser.IDTarif.Value);

            CallTypes = new ObservableCollection<TypeOfCallTable>
            {
                new TypeOfCallTable { C__PK__Kod_Type_Of_Call = 1, Name = "Городской" },
                new TypeOfCallTable { C__PK__Kod_Type_Of_Call = 2, Name = "Межгород" },
                new TypeOfCallTable { C__PK__Kod_Type_Of_Call = 3, Name = "Международный" }
            };

            StartCallCommand = new Command(obj => StartCall());
            EndCallCommand = new Command(obj => EndCall());
            ExitToSettingsCommand = new Command(obj => ExitToSettings());
        }

        private void StartCall()
        {
            if (string.IsNullOrEmpty(ReceiverPhoneNumber) || SelectedCallType == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            callStartTime = DateTime.Now;
            callTimer.Start();
        }

        private void EndCall()
        {
            callTimer.Stop();
            TimeSpan callDuration = callTimer.Elapsed;

            int totalSeconds = (int)callDuration.TotalSeconds;

            int pricePerSecond = 0;

            // Определяем стоимость секунды для выбранного типа звонка
            switch (SelectedCallType.C__PK__Kod_Type_Of_Call)
            {
                case 1:
                    pricePerSecond = (userTariff.PriceGorod ?? 0); // Переводим стоимость минуты в стоимость секунды
                    break;
                case 2:
                    pricePerSecond = (userTariff.PriceMejGorod ?? 0);
                    break;
                case 3:
                    pricePerSecond = (userTariff.PriceMejNarod ?? 0);
                    break;
            }

            int costInRubles = 0;

            if (totalSeconds > currentUser.Mins)
            {
                // Вычитаем доступные секунды
                int usedSeconds = (int)currentUser.Mins;
                currentUser.Mins = 0;

                // Рассчитываем стоимость оставшихся секунд
                int remainingSeconds = totalSeconds - usedSeconds;
                costInRubles = remainingSeconds * pricePerSecond;

                // Вычитаем стоимость из баланса пользователя
                currentUser.Balance -= costInRubles;
            }
            else
            {
                // Вычитаем секунды из баланса
                currentUser.Mins -= totalSeconds;
            }

            dbAccess.UpdateUserMins(currentUser.PhoneNumber, currentUser.Mins,currentUser.Balance); // Сохраняем изменения в БД
            

            var call = new CallTable
            {
                C__FK__Number_Sender = currentUser.PhoneNumber,
                C__FK__Number_Getter = ReceiverPhoneNumber,
                C__FK__Type_Of_Call = SelectedCallType.C__PK__Kod_Type_Of_Call,
                Data_Of_Call = callStartTime.Date,
                Time_Of_Call = callStartTime.TimeOfDay,
                Amount = (int)callDuration.TotalSeconds,
                Cost = CalculateCost(callDuration)
            };

            dbAccess.SaveCall(call);

            // Вывод времени разговора в диалоговое окно
            MessageBox.Show($"Звонок завершен. Время разговора: {callDuration.TotalSeconds} секунд.");
            CloseWindow();
        }

        private int CalculateCost(TimeSpan callDuration)
        {
            int pricePerMinute = 0;

            switch (SelectedCallType.C__PK__Kod_Type_Of_Call)
            {
                case 1:
                    pricePerMinute = userTariff.PriceGorod ?? 0;
                    break;
                case 2:
                    pricePerMinute = userTariff.PriceMejGorod ?? 0;
                    break;
                case 3:
                    pricePerMinute = userTariff.PriceMejNarod ?? 0;
                    break;
            }

            return (int)(pricePerMinute * callDuration.TotalMinutes);
        }

        private void ExitToSettings()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
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