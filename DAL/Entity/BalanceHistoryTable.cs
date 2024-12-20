namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BalanceHistoryTable")]
    public partial class BalanceHistoryTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? Kod_Tarifa { get; set; }

        public int? Kod_Yslygi { get; set; }

        [Required]
        [StringLength(50)]
        public string Numbe_Of_Phone { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        public int Amount { get; set; }
    }
}
