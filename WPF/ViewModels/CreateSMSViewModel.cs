using DAL.Entity;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WPF.Models;

namespace WPF.ViewModels
{
    public class CreateSMSViewModel : INotifyPropertyChanged
    {
        private UserClass currentUser;
        private DBAccess dbAccess;

        public string ReceiverPhoneNumber { get; set; }
        public string MessageText { get; set; }

        public Command SendSMSCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command ExitToSettingsCommand { get; set; }

        public CreateSMSViewModel(UserClass user)
        {
            currentUser = user;
            dbAccess = new DBAccess();

            SendSMSCommand = new Command(obj => SendSMS());
            CancelCommand = new Command(obj => Cancel());
            ExitToSettingsCommand = new Command(obj => ExitToSettings());
        }

        private void ExitToSettings()
        {
            CloseWindow();
        }

        private void SendSMS()
        {
            if (string.IsNullOrEmpty(ReceiverPhoneNumber) || string.IsNullOrEmpty(MessageText))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            var sms = new SMSTable
            {
                C__FK__Number_Otpravitela = currentUser.PhoneNumber,
                C__FK__Number_Poluchatela = ReceiverPhoneNumber,
                Text = MessageText,
                Data_Of_Sent = DateTime.Now.Date,
                Time = DateTime.Now.TimeOfDay
            };

            dbAccess.SaveSMS(sms);

            MessageBox.Show("Сообщение успешно отправлено.");
            CloseWindow();
        }

        private void Cancel()
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