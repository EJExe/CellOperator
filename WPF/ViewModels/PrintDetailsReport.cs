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
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Controls;

namespace WPF.ViewModels
{
    public class PrintDetailsReport
    {
        private readonly BdContext _context;

        public PrintDetailsReport(BdContext context)
        {
            _context = context;
        }

        public void GenerateReport(string phoneNumber, DateTime StartDate, DateTime EndDate)
        {
            DBAccess db = new DBAccess();
            var calls = db.GetCallsByPeriod(phoneNumber, StartDate, EndDate);
            var sms = db.GetSMSByPeriod(phoneNumber, StartDate, EndDate);

            int? callsTotal = 0;
            int tariffsTotal = 0;
            int servicesTotal = 0;
            int? total = 0;

            string rep = "Детализация счета\n";

            // Извлечение данных из BalanceCounterHistoryTable
            var balanceHistory = _context.BalanceHistoryTable
                .Where(b => b.Numbe_Of_Phone == phoneNumber && b.Data >= StartDate)
                .ToList();

            if (balanceHistory.Count == 0)
            {
                MessageBox.Show("Нет данных для указанного номера телефона.");
                return;
            }

            // Звонки
            rep += "Звонки:\n";
            foreach (var call in calls)
            {
                rep += $"Дата: {call.Data_Of_Call}, Длительность: {call.Amount} сек, Стоимость: {call.Cost}\n";
                callsTotal += call.Cost;
            }
            if (callsTotal > 0)
            {
                rep += $"\nВыплаты по Звонкам: {callsTotal}\n";
                total += callsTotal;
            }

            // СМС
            rep += "\nСМС:\n";
            foreach (var smsItem in sms)
            {
                rep += $"Дата: {smsItem.Data_Of_Sent}, Текст: {smsItem.Text}\n";
            }

            // Тарифы и услуги
            rep += "\nТариф(Ы): \n";
            foreach (var entry in balanceHistory)
            {
                if (entry.Kod_Tarifa != null)
                {
                    rep += $"Дата: {entry.Data.ToShortDateString()}, Сумма: {entry.Amount}, Тариф: {db.GetTariffByIdStrange(entry.Kod_Tarifa)}\n";
                    tariffsTotal += entry.Amount;
                }
            }
            if (tariffsTotal > 0)
            {
                rep += $"\nВыплаты за Тарифы: {tariffsTotal}\n";
                total += tariffsTotal;
            }

            rep += "\nУслуги: \n";
            foreach (var entry in balanceHistory)
            {
                if (entry.Kod_Yslygi != null)
                {
                    rep += $"Дата: {entry.Data.ToShortDateString()}, Сумма: {entry.Amount}, Услуга: {db.GetServiceByIdStrange(entry.Kod_Yslygi)}\n";
                    servicesTotal += entry.Amount;
                }
            }
            
            if (servicesTotal > 0)
            {
                rep += $"\nВыплаты за Услуги: {servicesTotal}\n";
                total += servicesTotal;
            }

            // Общая сумма
            if (total > 0)
            {
                rep += $"\nВсе Выплаты: {total}\n";
            }

            // Генерация PDF
            PdfGenerator pdfGenerator = new PdfGenerator();
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Details.pdf");
            pdfGenerator.GenerateInvoice(filePath, rep);
            MessageBox.Show(rep);
        }
    }
}
