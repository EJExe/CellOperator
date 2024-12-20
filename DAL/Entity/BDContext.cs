using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entity
{
    public partial class BdContext : DbContext
    {
        public BdContext()
            : base("name=BdContext")
        {
        }

        public virtual DbSet<BalanceHistoryTable> BalanceHistoryTable { get; set; }
        public virtual DbSet<CallTable> CallTable { get; set; }
        public virtual DbSet<SMSTable> SMSTable { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TarifTable> TarifTable { get; set; }
        public virtual DbSet<TypeOfCallTable> TypeOfCallTable { get; set; }
        public virtual DbSet<TypeOfTarifTable> TypeOfTarifTable { get; set; }
        public virtual DbSet<TypeOfUserTable> TypeOfUserTable { get; set; }
        public virtual DbSet<TypeOfYslygiTable> TypeOfYslygiTable { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
        public virtual DbSet<YslygiTable> YslygiTable { get; set; }
        public virtual DbSet<DataTable> DataTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BalanceHistoryTable>()
                .Property(e => e.Numbe_Of_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CallTable>()
                .Property(e => e.C__FK__Number_Sender)
                .IsUnicode(false);

            modelBuilder.Entity<CallTable>()
                .Property(e => e.C__FK__Number_Getter)
                .IsUnicode(false);

            modelBuilder.Entity<SMSTable>()
                .Property(e => e.C__FK__Number_Poluchatela)
                .IsUnicode(false);

            modelBuilder.Entity<SMSTable>()
                .Property(e => e.C__FK__Number_Otpravitela)
                .IsUnicode(false);

            modelBuilder.Entity<SMSTable>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<TarifTable>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TarifTable>()
                .HasMany(e => e.DataTable)
                .WithRequired(e => e.TarifTable)
                .HasForeignKey(e => e.C__FK__Kod_Tarifa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfCallTable>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TypeOfCallTable>()
                .HasMany(e => e.CallTable)
                .WithRequired(e => e.TypeOfCallTable)
                .HasForeignKey(e => e.C__FK__Type_Of_Call)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfTarifTable>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TypeOfTarifTable>()
                .HasMany(e => e.TarifTable)
                .WithRequired(e => e.TypeOfTarifTable)
                .HasForeignKey(e => e.C___FK___Kod_Type_Of_Tarif)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfUserTable>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TypeOfUserTable>()
                .HasMany(e => e.UserTable)
                .WithRequired(e => e.TypeOfUserTable)
                .HasForeignKey(e => e.C__FK___Kod_Type_Of_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfYslygiTable>()
                .HasMany(e => e.YslygiTable)
                .WithRequired(e => e.TypeOfYslygiTable)
                .HasForeignKey(e => e.C__FK__Kod_TypeOfYslygi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.C__PK___Number_Of_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.Loging)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.CallTable)
                .WithRequired(e => e.UserTable)
                .HasForeignKey(e => e.C__FK__Number_Sender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.SMSTable)
                .WithRequired(e => e.UserTable)
                .HasForeignKey(e => e.C__FK__Number_Otpravitela)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.DataTable)
                .WithRequired(e => e.UserTable)
                .HasForeignKey(e => e.C__FK__Numbe_Of_Phone)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTable>()
                .HasMany(e => e.YslygiTable)
                .WithRequired(e => e.UserTable)
                .HasForeignKey(e => e.C__FK__Number_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YslygiTable>()
                .Property(e => e.C__FK__Number_User)
                .IsUnicode(false);

            modelBuilder.Entity<DataTable>()
                .Property(e => e.C__FK__Numbe_Of_Phone)
                .IsUnicode(false);
        }
    }
}
