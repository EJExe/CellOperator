using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Models
{
    public class DBAccess
    {
        private void DBException()
        {
            MessageBox.Show("Ошибка подключения к базе, приложение будет закрыто", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            Environment.Exit(0);
        }


        private BdContext setDB()
        {
            BdContext db = new BdContext();
            try
            {
                if (db.Database.Exists())
                {
                    return db;
                }
                else
                {
                    DBException();
                    return null;
                }
            }
            catch (System.InvalidOperationException)
            {
                DBException();
                return null;
            }
        }

        public List<CallTable> GetCallsByPeriod(string phoneNumber, DateTime startDate, DateTime endDate)
        {
            BdContext DB = setDB();
            return DB.CallTable
                     .Where(c => c.C__FK__Number_Sender == phoneNumber && c.Data_Of_Call >= startDate && c.Data_Of_Call <= endDate)
                     .ToList();
        }

        public List<SMSTable> GetSMSByPeriod(string phoneNumber, DateTime startDate, DateTime endDate)
        {
            BdContext DB = setDB();
            return DB.SMSTable
                     .Where(s => s.C__FK__Number_Otpravitela == phoneNumber && s.Data_Of_Sent >= startDate && s.Data_Of_Sent <= endDate)
                     .ToList();
        }

        public List<CallTable> GetCallDetails(string phoneNumber)
        {
            BdContext DB = setDB();
            return DB.CallTable
                     .Where(c => c.C__FK__Number_Sender == phoneNumber)
                     .ToList();
        }

        public List<DataTable> GetTariffHistory(string phoneNumber)
        {
            BdContext DB = setDB();
            return DB.DataTable
                     .Where(d => d.C__FK__Numbe_Of_Phone == phoneNumber)
                     .ToList();
        }

        public List<SMSTable> GetSMSDetails(string phoneNumber)
        {
            BdContext DB = setDB();
            return DB.SMSTable
                     .Where(s => s.C__FK__Number_Otpravitela == phoneNumber)
                     .ToList();
        }

        public TarifClass GetTariffById(int tarifId)
        {
            BdContext DB = setDB();
            TarifTable tarif = DB.TarifTable.Where(i => i.C__PK___Kod_Tarifa == tarifId).FirstOrDefault();
            if (tarif != null)
            {
                return new TarifClass(tarif);
            }
            else
            {
                return null;
            }
        }

        public string GetTariffByIdStrange(int? tarifId)
        {
            BdContext DB = setDB();
            TarifTable tarif = DB.TarifTable.Where(i => i.C__PK___Kod_Tarifa == tarifId).FirstOrDefault();

            if (tarif != null)
            {
                return tarif.Name ;
            }
            else
            {
                return null;
            }
        }

        public void GenerateBalanceHistoryReport()
        {
            // Получаем данные из таблицы BalanceCounterHistoryTable
            List<BalanceHistoryTable> balanceHistory = GetAllBalanceHistory();

            // Генерируем отчет
            StringBuilder report = new StringBuilder();
            report.AppendLine("TariffID\tServiceID\tPhoneNumber\tDate\tAmount");
            report.AppendLine("---------------------------------");

            foreach (var entry in balanceHistory)
            {
                if ( entry.Kod_Tarifa != 1) {
                    // Обрабатываем null-значения для TariffID и ServiceID
                    string tariffId = entry.Kod_Tarifa.HasValue ? entry.Kod_Tarifa.Value.ToString() : "NULL";
                    string serviceId = entry.Kod_Yslygi.HasValue ? entry.Kod_Yslygi.Value.ToString() : "NULL";

                    report.AppendLine($"{tariffId}\t{serviceId}\t{entry.Numbe_Of_Phone}\t{entry.Data.ToShortDateString()}\t{entry.Amount}");
                }
            }

            // Выводим отчет в MessageBox
            MessageBox.Show(report.ToString(), "BalanceCounterHistoryTable Report");
        }

        public string GetServiceByIdStrange(int? id)
        {
            //BdContext DB = setDB();
            BdContext DB = new BdContext();
            TypeOfYslygiTable service = DB.TypeOfYslygiTable
                .FirstOrDefault(s => s.C__PK__Kod_Type_Of_Yslygi == id); // Ищем услугу по идентификатору

            if (service != null)
            {
                return service.Name; // Возвращаем название услуги
            }
            else
            {
                return "Услуга не найдена"; // Возвращаем сообщение, если услуга не найдена
            }
        }

        public string GetServiceByIdStrangeSup(YslygiTable service)
        {
            //BdContext DB = setDB();
            BdContext DB = new BdContext();
            TypeOfYslygiTable ysl = DB.TypeOfYslygiTable.Where(i => i.C__PK__Kod_Type_Of_Yslygi == service.C__FK__Kod_TypeOfYslygi).FirstOrDefault();
            if (ysl != null)
            {
                return ysl.Name; // Возвращаем название услуги
            }
            else
            {
                return "Услуга не найдена"; // Возвращаем сообщение, если услуга не найдена
            }
        }

        public UserClass GetUserByLogin(string num)
        {
            BdContext DB = setDB();
            Debug.WriteLine($"Searching for user with phone number: {num}");

            UserTable user = DB.UserTable.Where(i => i.Loging == num).FirstOrDefault();
            if (user != null)
            {
                Debug.WriteLine("User found");
                return new UserClass(user);
            }
            else
            {
                Debug.WriteLine("User not found");
                return null;
            }
        }

        public List<TypeOfYslygiTable> GetAllServices()
        {
            BdContext DB = setDB();
            return DB.TypeOfYslygiTable.ToList();
        }

        public void DeductTariffCost(string phoneNumber, int tariffId)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Номер телефона не указан.");
                return;
            }

            BdContext DB = setDB();
            UserTable user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == phoneNumber);
            if (user == null)
            {
                MessageBox.Show("Пользователь с таким номером телефона не найден.");
                return;
            }

            // Проверяем, является ли тариф пользователя тарифом 3
            if (user.C__FK___Kod_Tarifa == 3)
            {
                return;
            }

            TarifTable tarif = DB.TarifTable.FirstOrDefault(t => t.C__PK___Kod_Tarifa == tariffId);
            if (tarif == null)
            {
                MessageBox.Show("Тариф с указанным ID не найден.");
                return;
            }

            int tariffCost = tarif.Cost_PerMonth ?? 0;

            // Списание средств, даже если баланс уходит в минус
            user.Money_On_Bank -= tariffCost;
            DB.SaveChanges();

            int newId = FindFirstAvailableIdUniq<BalanceHistoryTable>(DB, s => s.ID);
            // Запись в таблицу BalanceCounterHistory
            DB.BalanceHistoryTable.Add(new BalanceHistoryTable
            {
                ID = newId,
                Kod_Tarifa = tariffId, // Указываем тариф
                Kod_Yslygi = null,    // Указываем NULL для услуги
                Numbe_Of_Phone = phoneNumber,
                Data = DateTime.Now,
                Amount = tariffCost
            });
            DB.SaveChanges();

        }

        public void DeductServiceCost(string phoneNumber, int serviceId)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Номер телефона не указан.");
                return;
            }

            BdContext DB = setDB();
            UserTable user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == phoneNumber);
            if (user == null)
            {
                MessageBox.Show("Пользователь с таким номером телефона не найден.");
                return;
            }

            // Проверяем, является ли тариф пользователя тарифом 3
            if (user.C__FK___Kod_Tarifa == 3)
            {
                return;
            }

            TypeOfYslygiTable service = DB.TypeOfYslygiTable.FirstOrDefault(s => s.C__PK__Kod_Type_Of_Yslygi == serviceId);
            if (service == null)
            {
                MessageBox.Show("Услуга с указанным ID не найдена.");
                return;
            }

            int serviceCost = service.CostPerMonth;

            // Списание средств, даже если баланс уходит в минус
            user.Money_On_Bank -= serviceCost;
            DB.SaveChanges();

            int newId = FindFirstAvailableIdUniq<BalanceHistoryTable>(DB, s => s.ID);
            // Запись в таблицу BalanceCounterHistory
            DB.BalanceHistoryTable.Add(new BalanceHistoryTable
            {
                ID= newId,
                Kod_Tarifa = null,    // Указываем NULL для тарифа
                Kod_Yslygi = serviceId, // Указываем услугу
                Numbe_Of_Phone = phoneNumber,
                Data = DateTime.Now,
                Amount = serviceCost
            });
            DB.SaveChanges();

        }

        public List<string> GetAllPhoneNumbers()
        {
            BdContext DB = setDB();
            return DB.UserTable
                     .Select(u => u.C__PK___Number_Of_Phone)
                     .ToList();
        }

        public List<BalanceHistoryTable> GetAllBalanceHistory()
        {
            BdContext DB = setDB();
            return DB.BalanceHistoryTable.ToList();
        }

        public List<BalanceHistoryTable> GetBalanceHistoryByUser(string phoneNumber)
        {
            BdContext DB = setDB();
            return DB.BalanceHistoryTable
                     .Where(b => b.Numbe_Of_Phone == phoneNumber)
                     .ToList();
        }

        public List<TypeOfYslygiTable> GetUserServices(string phoneNumber)
        {
            BdContext DB = setDB();
            var user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == phoneNumber);
            if (user != null)
            {
                var userServices = DB.YslygiTable
                    .Where(y => y.C__FK__Number_User == phoneNumber)
                    .Select(y => y.C__FK__Kod_TypeOfYslygi)
                    .ToList();

                return DB.TypeOfYslygiTable
                    .Where(t => userServices.Contains(t.C__PK__Kod_Type_Of_Yslygi))
                    .ToList();
            }
            return new List<TypeOfYslygiTable>();
        }

        public void ConnectService(string phoneNumber, int serviceId)
        {
            BdContext DB = setDB();
            var user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == phoneNumber);
            if (user != null)
            {
                var existingService = DB.YslygiTable.FirstOrDefault(y => y.C__FK__Number_User == phoneNumber && y.C__FK__Kod_TypeOfYslygi == serviceId);
                if (existingService == null)
                {
                    // Найти первую незанятую цифру (Id)
                    int newId = FindFirstAvailableId(DB);

                    DB.YslygiTable.Add(new YslygiTable
                    {
                        Kod_Yslygi = newId, // Устанавливаем найденный уникальный Id
                        C__FK__Number_User = phoneNumber,
                        C__FK__Kod_TypeOfYslygi = serviceId,
                                                
                    });
                    DB.SaveChanges();
                }
            }
        }

        // Метод для поиска первой незанятой цифры (Id)
        private int FindFirstAvailableId(BdContext DB)
        {
            // Получаем все существующие Id из таблицы YslygiTable
            var existingIds = DB.YslygiTable.Select(y => y.Kod_Yslygi).ToList();

            // Ищем первую незанятую цифру, начиная с 0
            int candidateId = 0;
            while (existingIds.Contains(candidateId))
            {
                candidateId++;
            }

            return candidateId;
        }

        public void DisconnectService(string phoneNumber, int serviceId)
        {
            BdContext DB = setDB();
            var user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == phoneNumber);
            if (user != null)
            {
                var serviceToRemove = DB.YslygiTable.FirstOrDefault(y => y.C__FK__Number_User == phoneNumber && y.C__FK__Kod_TypeOfYslygi == serviceId);
                if (serviceToRemove != null)
                {
                    DB.YslygiTable.Remove(serviceToRemove);
                    DB.SaveChanges();
                }
            }
        }
        public List<TarifClass> GetAllTariffs(int userType)
        {
            BdContext DB = setDB();
            return DB.TarifTable
                    .Where(t => t.C___FK___Kod_Type_Of_Tarif == userType && t.C___FK___Kod_Type_Of_Tarif != 0)
                    .Select(t => new TarifClass
                    {
                        Id = t.C__PK___Kod_Tarifa,
                        Name = t.Name,
                        PricePerMonth = t.Cost_PerMonth,
                        PriceGorod = t.Price_1minn_city,
                        PriceMejGorod = t.Price_1minn_mejgorod,
                        PriceMejNarod = t.Price_1minn_mejdunarod,
                        SMS_Per_Month = t.SMS_Per_Month,
                        GB_Per_Month = t.GB_Per_Month,
                        Min_Per_Month = t.Min_Per_Month,
                        TariffType = t.C___FK___Kod_Type_Of_Tarif,
                        PricePerConnection = t.Cost_Of_Connection
                    }).ToList();
        }

        public List<TarifClass> GetAllTariffsForTable( )
        {
            BdContext DB = setDB();
            return DB.TarifTable
                    .Where(t => t.C___FK___Kod_Type_Of_Tarif != 0)
                    .Select(t => new TarifClass
                    {
                        Id = t.C__PK___Kod_Tarifa,
                        Name = t.Name,
                        PricePerMonth = t.Cost_PerMonth,
                        PriceGorod = t.Price_1minn_city,
                        PriceMejGorod = t.Price_1minn_mejgorod,
                        PriceMejNarod = t.Price_1minn_mejdunarod,
                        SMS_Per_Month = t.SMS_Per_Month,
                        GB_Per_Month = t.GB_Per_Month,
                        Min_Per_Month = t.Min_Per_Month,
                        TariffType = t.C___FK___Kod_Type_Of_Tarif,
                        PricePerConnection = t.Cost_Of_Connection

                    }).ToList();
        }

        public void UpdateUserTariff(string phoneNumber, int tarifId)
        {
            BdContext DB = setDB();
            UserTable user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == phoneNumber);
            if (user != null)
            {
                user.C__FK___Kod_Tarifa = tarifId;
                DB.SaveChanges();
            }
        }

        public int FindFirstAvailableIdUniq<T>(DbContext dbContext, Func<T, int> idSelector) where T : class
        {
            var existingIds = dbContext.Set<T>().Select(idSelector).ToList();
            int candidateId = 0;
            while (existingIds.Contains(candidateId))
            {
                candidateId++;
            }

            return candidateId;
        }

        public void SaveSMS(SMSTable sms)
        {
            BdContext DB = setDB();
            int newId = FindFirstAvailableIdUniq<SMSTable>(DB, s => s.C__PK__Kod_SMS);
            sms.C__PK__Kod_SMS = newId;
            DB.SMSTable.Add(sms);
            DB.SaveChanges();
        }

        public void SaveCall(CallTable call)
        {
            BdContext DB = setDB();
            int newId = FindFirstAvailableIdUniq<CallTable>(DB, s => s.C__PK__Kod_Call);
            call.C__PK__Kod_Call = newId;
            DB.CallTable.Add(call);
            DB.SaveChanges();
        }

        public int Save(BdContext DB)
        {
            return DB.SaveChanges();
        }

        public void UpdateUserBalance(string phoneNumber, int amount)
        {
            BdContext DB = setDB();
            UserTable user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == phoneNumber);
            if (user != null)
            {
                user.Money_On_Bank += amount;
                DB.SaveChanges();
            }
        }

        public void UpdateUserProfile(string oldPhoneNumber, string fullName, string login, string password)
        {
            BdContext DB = setDB();
            UserTable user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == oldPhoneNumber);
            if (user != null)
            {
                // Присоединяем существующий объект к контексту данных
                DB.UserTable.Attach(user);

                if (!string.IsNullOrEmpty(fullName))
                {
                    user.FIO = fullName;
                }

                if (!string.IsNullOrEmpty(login) && login != user.Loging)
                {
                    user.Loging = login;
                }

                if (!string.IsNullOrEmpty(password) && password != user.Password)
                {
                    user.Password = password;
                }

                DB.SaveChanges();
            }
        }

        public void UpdateUserSMS(string oldPhoneNumber, int? SMS)
        {
            BdContext DB = setDB();
            UserTable user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == oldPhoneNumber);
            if (user != null)
            {
                // Присоединяем существующий объект к контексту данных
                DB.UserTable.Attach(user);
                user.SMS_Left = SMS;

                DB.SaveChanges();
            }
        }

        public void UpdateUserMins(string oldPhoneNumber, int? Mins,int? bal)
        {
            BdContext DB = setDB();
            UserTable user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == oldPhoneNumber);
            if (user != null)
            {
                // Присоединяем существующий объект к контексту данных
                DB.UserTable.Attach(user);
                user.Minuts_Left = Mins;
                user.Money_On_Bank = bal;

                DB.SaveChanges();
            }
        }

        public void UpdateTariff(TarifClass tariff)
        {
            BdContext DB = setDB();
            TarifTable tarif = DB.TarifTable.FirstOrDefault(t => t.C__PK___Kod_Tarifa == tariff.Id);
            if (tarif != null)
            {
                tarif.Name = tariff.Name;
                tarif.Cost_PerMonth = tariff.PricePerMonth;
                tarif.Price_1minn_city = tariff.PriceGorod;
                tarif.Price_1minn_mejgorod = tariff.PriceMejGorod;
                tarif.Price_1minn_mejdunarod = tariff.PriceMejNarod;
                DB.SaveChanges();
            }
        }

        public void AddTariff(TarifClass tariff)
        {
            BdContext DB = setDB();
            int maxId = DB.TarifTable.Max(t => t.C__PK___Kod_Tarifa);
            TarifTable newTarif = new TarifTable
            {
                C__PK___Kod_Tarifa = maxId + 1,
                Name = tariff.Name,
                Cost_PerMonth = tariff.PricePerMonth,
                Price_1minn_city = tariff.PriceGorod,
                Price_1minn_mejgorod = tariff.PriceMejGorod,
                Price_1minn_mejdunarod = tariff.PriceMejNarod
            };
            DB.TarifTable.Add(newTarif);
            DB.SaveChanges();
            tariff.Id = newTarif.C__PK___Kod_Tarifa; // Обновляем ID в модели
        }

        public void DeleteTariff(int tariffId)
        {
            BdContext DB = setDB();
            TarifTable tarif = DB.TarifTable.FirstOrDefault(t => t.C__PK___Kod_Tarifa == tariffId);
            if (tarif != null)
            {
                DB.TarifTable.Remove(tarif);
                DB.SaveChanges();
            }
        }

        public bool IsPhoneNumberExists(string phoneNumber)
        {
            BdContext DB = setDB();
            return DB.UserTable.Any(u => u.C__PK___Number_Of_Phone == phoneNumber);
        }

        public List<UserClass> GetAllUsers()
        {
            BdContext DB = setDB();
            var users = DB.UserTable
            .Join(
            DB.TarifTable, // Вторая таблица для join
            user => user.C__FK___Kod_Tarifa, // Ключ из UserTable
            tariff => tariff.C__PK___Kod_Tarifa, // Ключ из TariffTable
            (user, tariff) => new UserClass
            {
                PhoneNumber = user.C__PK___Number_Of_Phone,
                Name = user.FIO,
                Balance = user.Money_On_Bank,
                IDTarif = user.C__FK___Kod_Tarifa,
                TariffName = tariff.Name // Добавляем название тарифа
            }
        ).ToList();
            Debug.WriteLine($"Fetched {users.Count} users from the database.");
            return users;
        }

        public void AddUser(UserClass user)
        {
            BdContext DB = setDB();

            // Проверка на обязательные поля
            if (string.IsNullOrEmpty(user.PhoneNumber))
            {
                throw new ArgumentException("Phone number is required");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentException("Name is required");
            }

            UserTable newUser = new UserTable
            {
                C__PK___Number_Of_Phone = user.PhoneNumber,
                FIO = user.Name,
                Money_On_Bank = user.Balance
                
            };

            DB.UserTable.Add(newUser);
            DB.SaveChanges();
        }

        public void DeleteUser(string phoneNumber)
        {
            BdContext DB = setDB();
            UserTable user = DB.UserTable.FirstOrDefault(u => u.C__PK___Number_Of_Phone == phoneNumber);
            if (user != null)
            {
                DB.UserTable.Remove(user);
                DB.SaveChanges();
            }
        }

    }
}
