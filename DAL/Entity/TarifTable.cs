namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TarifTable")]
    public partial class TarifTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TarifTable()
        {
            DataTable = new HashSet<DataTable>();
        }

        [Key]
        [Column("<<PK>> Kod_Tarifa")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C__PK___Kod_Tarifa { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? Cost_PerMonth { get; set; }

        public int? Cost_Of_Connection { get; set; }

        public int? Price_1minn_city { get; set; }

        public int? Price_1minn_mejgorod { get; set; }

        public int? Price_1minn_mejdunarod { get; set; }

        [Column("[<<FK>>]Kod_Type_Of_Tarif")]
        public int C___FK___Kod_Type_Of_Tarif { get; set; }

        public int? SMS_Per_Month { get; set; }

        public int? GB_Per_Month { get; set; }

        public int? Min_Per_Month { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataTable> DataTable { get; set; }

        public virtual TypeOfTarifTable TypeOfTarifTable { get; set; }
    }
}
