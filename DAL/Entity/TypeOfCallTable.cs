namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeOfCallTable")]
    public partial class TypeOfCallTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeOfCallTable()
        {
            CallTable = new HashSet<CallTable>();
        }

        [Key]
        [Column("<<PK>>Kod_Type_Of_Call")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C__PK__Kod_Type_Of_Call { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CallTable> CallTable { get; set; }
    }
}
