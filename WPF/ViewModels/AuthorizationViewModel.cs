using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Models;
using WPF.ViewModels;
using WPF.Views;



namespace SystemOpMobComm.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        private int wrong;
        public int Wrong
        {
            get { return wrong; }
            set
            {
                wrong = value;
                OnPropertyChanged("Wrong");
            }
        }

        private DBAccess db;
        private MainWindow window;
        private MainMenuView mainMenuView;
        public AuthorizationViewModel(MainWindow autorization)
        {
            window = autorization;
            db = new DBAccess();
            wrong = 0;
            setEnterCommand();
        }

        private void setEnterCommand()
        {
            Enter = new Command(obj =>
            {
                PasswordBox password = obj as PasswordBox;
                if (password != null)
                {
                    UserClass user = db.GetUserByLogin(login);
                    if (user != null)
                    {
                        if (user.Password == password.Password)
                        {
                            if (user.IDTarif == 0)
                            {
                                AdminMainMenuViewModel admmodel = new AdminMainMenuViewModel(user);
                                AdminMainMenuView adminMainMenuView = new AdminMainMenuView();
                                adminMainMenuView.DataContext = admmodel;  
                                admmodel.currentwindow = adminMainMenuView;
                                adminMainMenuView.Show();

                            }
                            else
                            {
                                MainMenuViewModel mainMenuViewModel = new MainMenuViewModel(user, mainMenuView);
                                MainMenuView reg = new MainMenuView(user);
                                reg.DataContext = mainMenuViewModel;
                                mainMenuViewModel.currentwindow = reg;
                                reg.Show();
                            }
                            window.Close();
                        }
                        else
                        {
                            password.Password = "";
                            Wrong = 2;
                        }
                    }
                    else
                    {
                        Wrong = 1;
                    }
                }
            },
                  obj => { return login != ""; });
        }

        public Command Enter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}