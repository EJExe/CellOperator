namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeOfYslygiTable")]
    public partial class TypeOfYslygiTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeOfYslygiTable()
        {
            YslygiTable = new HashSet<YslygiTable>();
        }

        [Key]
        [Column("<<PK>>Kod_Type_Of_Yslygi")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C__PK__Kod_Type_Of_Yslygi { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CostPerMonth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YslygiTable> YslygiTable { get; set; }
    }
}
