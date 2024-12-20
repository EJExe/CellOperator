using DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
    public class UserClass
    {
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public int? IDTarif { get; set; }
        public TarifClass Tariff { get; set; }

        public string Loging { get; set; }
        public string Password { get; set; }

        public int? Mins { get; set; }
        public int? GB { get; set; }
        public int? SMS { get; set; }

        public int? Balance { get; set; }

        public string TariffName { get; set; }
        public int? UserType { get; set; }

        public UserClass()
        {

        }

        public UserClass(UserTable user)
        {
            Name = user.FIO;
            Balance = user.Money_On_Bank;
            Loging = user.Loging;
            Password = user.Password;
            PhoneNumber = user.C__PK___Number_Of_Phone;
            IDTarif = user.C__FK___Kod_Tarifa;
            Tariff = new DBAccess().GetTariffById(IDTarif.Value);
            Mins = user.Minuts_Left;
            GB = user.Gb_Left;
            SMS = user.SMS_Left;
            UserType = user.C__FK___Kod_Type_Of_User; // Заполняем тип пользователя
            TariffName = new DBAccess().GetTariffByIdStrange(IDTarif.Value); 
        }
    }
}
