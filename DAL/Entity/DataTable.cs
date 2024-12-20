namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DataTable")]
    public partial class DataTable
    {
        [Key]
        [Column(Order = 0)]
        public DateTime data_nachala { get; set; }

        public DateTime? data_okonchania { get; set; }

        [Key]
        [Column("<<FK>>Kod_Tarifa", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C__FK__Kod_Tarifa { get; set; }

        [Key]
        [Column("<<FK>>Numbe_Of_Phone", Order = 2)]
        [StringLength(50)]
        public string C__FK__Numbe_Of_Phone { get; set; }

        public virtual TarifTable TarifTable { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
