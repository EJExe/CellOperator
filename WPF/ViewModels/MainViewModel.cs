using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.Models;
using WPF.Views;

namespace WPF.ViewModels
{
    //public class MainViewModel
    //{
    //    public MainViewModel(Window window, UserClass user)
    //    {
            
    //        MainMenuView = new Command(obj =>
    //        {
    //            MainMenuView window1 = new MainMenuView(user);
    //            window1.WindowState = window.WindowState;
    //            window1.Top = window.Top;
    //            window1.Left = window.Left;
    //            window1.Height = window.Height;
    //            window1.Width = window.Width;
    //            window1.Show();
    //            window.Close();
    //        });

    //        ChangeTarifIN = new Command(obj =>
    //        {
    //            TarifMenuView window1 = new TarifMenuView(user,window);
    //            window1.WindowState = window.WindowState;
    //            window1.Top = window.Top;
    //            window1.Left = window.Left;
    //            window1.Height = window.Height;
    //            window1.Width = window.Width;
    //            window1.Show();
    //            window.Close();
    //        });

    //        ChangeTarifOUT = new Command(obj =>
    //        {
    //            MainMenuView window1 = new MainMenuView(user);
    //            window1.WindowState = window.WindowState;
    //            window1.Top = window.Top;
    //            window1.Left = window.Left;
    //            window1.Height = window.Height;
    //            window1.Width = window.Width;
    //            window1.Show();
    //            window.Close();
    //        });

    //        Exit = new Command(obj =>
    //        {
    //            MainWindow window1 = new MainWindow();
    //            window1.Show();
    //            window.Close();
    //        });
    //    }

    //    public Command Exit { get; set; }
    //    public Command MainMenuView { get; set; }
    //    public Command ChangeTarifIN {  get; set; }
    //    public Command ChangeTarifOUT { get; set; }
    //}
}
