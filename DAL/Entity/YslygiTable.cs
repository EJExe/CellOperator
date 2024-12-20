namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YslygiTable")]
    public partial class YslygiTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Kod_Yslygi { get; set; }

        [Column("<<FK>>Number_User")]
        [Required]
        [StringLength(50)]
        public string C__FK__Number_User { get; set; }

        [Column("<<FK>>Kod_TypeOfYslygi")]
        public int C__FK__Kod_TypeOfYslygi { get; set; }

        public virtual TypeOfYslygiTable TypeOfYslygiTable { get; set; }

        public virtual UserTable UserTable { get; set; }
    }
}
