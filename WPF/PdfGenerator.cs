using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using DAL.Entity;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
public class PdfGenerator
{
    public void GenerateInvoice(string filePath, string details)
    {
        Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
        dlg.FileName = "Report"; // Default file name
        dlg.DefaultExt = ".pdf"; // Default file extension
        dlg.Filter = "Text documents (.pdf)|*.pdf"; // Filter files by extension
        Nullable<bool> result = dlg.ShowDialog();

        if (result == true)
        {
            // Save document
            string filename = dlg.FileName;
            filePath = filename;
        }

        try
        {
            // Проверка пути к файлу
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                MessageBox.Show("Указан неверный путь к файлу.");
                return;
            }

            // Удаление существующего файла, если он существует
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Создание документа
            Document document = new Document();

            // Добавление раздела в документ
            Section section = document.AddSection(); // Добавляем раздел в документ

            // Создание основного абзаца
            Paragraph main = section.AddParagraph(); // Добавляем абзац в раздел
            main.AddText("Отчет\n");
            main.AddText(details);
            main.Format.Font.Size = 18;
            main.Format.Alignment = ParagraphAlignment.Center;

            // Рендеринг документа в PDF
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            pdfRenderer.RenderDocument();

            // Сохранение PDF-файла
            pdfRenderer.PdfDocument.Save(filePath);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private List<CallTable> GetCallsByPeriod(string phoneNumber, DateTime startDate, DateTime endDate)
        {
            // Логика для получения звонков из базы данных
            return new List<CallTable>();
        }

        private List<SMSTable> GetSMSByPeriod(string phoneNumber, DateTime startDate, DateTime endDate)
        {
            // Логика для получения SMS из базы данных
            return new List<SMSTable>();
        }
}
