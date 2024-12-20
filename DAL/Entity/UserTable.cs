namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserTable")]
    public partial class UserTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTable()
        {
            CallTable = new HashSet<CallTable>();
            SMSTable = new HashSet<SMSTable>();
            DataTable = new HashSet<DataTable>();
            YslygiTable = new HashSet<YslygiTable>();
        }

        [Key]
        [Column("<<PK>> Number_Of_Phone")]
        [StringLength(50)]
        public string C__PK___Number_Of_Phone { get; set; }

        [Column("<<FK>> Kod_Tarifa")]
        public int C__FK___Kod_Tarifa { get; set; }

        [Column("<<FK>> Kod_Type_Of_User")]
        public int C__FK___Kod_Type_Of_User { get; set; }

        [Required]
        [StringLength(50)]
        public string FIO { get; set; }

        public int? Money_On_Bank { get; set; }

        [Required]
        [StringLength(50)]
        public string Loging { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int? Minuts_Left { get; set; }

        public int? Gb_Left { get; set; }

        public int? SMS_Left { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CallTable> CallTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSTable> SMSTable { get; set; }

        public virtual TypeOfUserTable TypeOfUserTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataTable> DataTable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YslygiTable> YslygiTable { get; set; }
    }
}
