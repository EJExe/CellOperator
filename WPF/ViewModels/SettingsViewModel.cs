using System.ComponentModel;
using System.Windows;
using System;
using System.IO;
using WPF.Models;
using WPF.ViewModels;
using System.Linq;
using WPF.Views;
using System.Diagnostics;
using System.Data.SqlClient;
using DAL.Entity;

public class SettingsViewModel : INotifyPropertyChanged
{
    private UserClass currentUser;
    private DBAccess dbAccess;

    // Флаг для определения текущего режима
    private bool isPrintMode;
    public bool IsPrintMode
    {
        get { return isPrintMode; }
        set
        {
            isPrintMode = value;
            OnPropertyChanged(nameof(IsPrintMode));
        }
    }

    private bool isSettingsMode;
    public bool IsSettingsMode
    {
        get { return isSettingsMode; }
        set
        {
            isSettingsMode = value;
            OnPropertyChanged(nameof(isSettingsMode));
        }
    }

    private bool isDetailsMode;
    public bool IsDetailsMode
    {
        get { return isDetailsMode; }
        set
        {
            isDetailsMode = value;
            OnPropertyChanged(nameof(IsDetailsMode));
        }
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Command EditProfileCommand { get; set; }
    public Command PrintCallsAndSMSCommand { get; set; }
    public Command DetailsCommand { get; set; }
    public Command SMSCommand { get; set; }
    public Command ReturnToMainMenuCommand { get; set; }
    public Command CallCommand { get; set; }
    public Command PrintReportCommand { get; set; } 
    public Command BackToSettingsCommand { get; set; } 
    public Command PrintDetailsReportcontCommand { get; set; }

    public SettingsViewModel(UserClass user, Window mainWindow)
    {
        currentUser = user;
        dbAccess = new DBAccess();
        IsSettingsMode = true;
        IsDetailsMode = false;
        IsPrintMode = false;
        StartDate = DateTime.Now.AddDays(-7);
        EndDate = DateTime.Now;

        CallCommand = new Command(obj => StartCall());
        SMSCommand = new Command(obj => CreateSMS());
        EditProfileCommand = new Command(obj => EditProfile());
        PrintCallsAndSMSCommand = new Command(obj => SwitchToPrintMode());
        DetailsCommand = new Command(obj => SwitchToDetailsMode());
        PrintReportCommand = new Command(obj => PrintReport());
        BackToSettingsCommand = new Command(obj => SwitchToSettingsMode());
        ReturnToMainMenuCommand = new Command(obj => ReturnToMainMenu());
        PrintDetailsReportcontCommand = new Command(obj => PrintDetailsReportcont());
    }

    private void SwitchToPrintMode()
    {
        IsPrintMode = true;
        IsSettingsMode = false;
        IsDetailsMode = false;
    }

    private void SwitchToDetailsMode()
    {
        IsDetailsMode = true;
        IsSettingsMode = false;
        IsPrintMode = false;
    }

    private void SwitchToSettingsMode()
    {
        IsPrintMode = false;
        IsSettingsMode = true;
        IsDetailsMode = false;
    }

    private void PrintReport()
    {
        var calls = dbAccess.GetCallsByPeriod(currentUser.PhoneNumber, StartDate, EndDate);
        var sms = dbAccess.GetSMSByPeriod(currentUser.PhoneNumber, StartDate, EndDate);

        string report = "Звонки:\n";
        foreach (var call in calls)
        {
            report += $"Дата: {call.Data_Of_Call}, Длительность: {call.Amount} сек, Стоимость: {call.Cost}\n";
        }

        report += "\nСМС:\n";
        foreach (var smsItem in sms)
        {
            report += $"Дата: {smsItem.Data_Of_Sent}, Текст: {smsItem.Text}\n";
        }

        // Вывод отчета в текстовое поле или другой элемент интерфейса
        MessageBox.Show(report, "Распечатка звонков и смс за период");

        // Сохранение отчета в PDF
        PdfGenerator pdfGenerator = new PdfGenerator();
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ReportCallsAndSMS.pdf");
        pdfGenerator.GenerateInvoice(filePath, report);
    }

    private void StartCall()
    {
        if ( currentUser.Mins > 0 || currentUser.Balance > 0)
        {
            CallView callView = new CallView(currentUser, Application.Current.MainWindow);
            callView.ShowDialog();
        }
        else { MessageBox.Show("Не достаточно средств!", "Ошибка", MessageBoxButton.OK); }
    }

    private void CreateSMS()
    {
        if ( currentUser.SMS > 0 )
        {
            currentUser.SMS -= 1;
            CreateSMSView createSMSView = new CreateSMSView(currentUser, Application.Current.MainWindow);
            dbAccess.UpdateUserSMS(currentUser.PhoneNumber, currentUser.SMS); // Сохраняем изменения в БД
            createSMSView.ShowDialog();
        }
        else { MessageBox.Show("Упсс :/", "Ошибка", MessageBoxButton.OK); }
    }

    private void ReturnToMainMenu()
    {
        var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
        if (window != null)
        {
            window.Close();
        }
    }

    private void EditProfile()
    {
        EditProfileView editProfileView = new EditProfileView(currentUser, Application.Current.MainWindow);
        editProfileView.ShowDialog();
    }

    private void PrintDetailsReportcont()
    {
        using (BdContext context = new BdContext())
        {
            // Создание экземпляра отчета
            PrintDetailsReport report = new PrintDetailsReport(context);

            // Генерация отчета для указанного номера телефона
            report.GenerateReport(currentUser.PhoneNumber,StartDate,EndDate);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}