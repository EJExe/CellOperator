namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CallTable")]
    public partial class CallTable
    {
        [Key]
        [Column("<<PK>>Kod_Call")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C__PK__Kod_Call { get; set; }

        [Column("<<FK>>Number_Sender")]
        [Required]
        [StringLength(50)]
        public string C__FK__Number_Sender { get; set; }

        [Column("<<FK>>Number_Getter")]
        [Required]
        [StringLength(50)]
        public string C__FK__Number_Getter { get; set; }

        [Column("<<FK>>Type_Of_Call")]
        public int C__FK__Type_Of_Call { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Of_Call { get; set; }

        public TimeSpan Time_Of_Call { get; set; }

        public int Amount { get; set; }

        public int? Cost { get; set; }

        public virtual TypeOfCallTable TypeOfCallTable { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
