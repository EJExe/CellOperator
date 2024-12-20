namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeOfTarifTable")]
    public partial class TypeOfTarifTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeOfTarifTable()
        {
            TarifTable = new HashSet<TarifTable>();
        }

        [Key]
        [Column("<<PK>Kod_Type_Of_Tarif")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C__PK_Kod_Type_Of_Tarif { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TarifTable> TarifTable { get; set; }
    }
}
