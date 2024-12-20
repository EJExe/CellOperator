namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SMSTable")]
    public partial class SMSTable
    {
        [Key]
        [Column("<<PK>>Kod_SMS")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C__PK__Kod_SMS { get; set; }

        [Column("<<FK>>Number_Poluchatela")]
        [Required]
        [StringLength(50)]
        public string C__FK__Number_Poluchatela { get; set; }

        [Column("<<FK>>Number_Otpravitela")]
        [Required]
        [StringLength(50)]
        public string C__FK__Number_Otpravitela { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Of_Sent { get; set; }

        public TimeSpan Time { get; set; }

        public string Text { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
