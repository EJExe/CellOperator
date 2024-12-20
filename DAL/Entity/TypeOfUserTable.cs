namespace DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeOfUserTable")]
    public partial class TypeOfUserTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeOfUserTable()
        {
            UserTable = new HashSet<UserTable>();
        }

        [Key]
        [Column("<<PK>>Kod_Type_Of_User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int C__PK__Kod_Type_Of_User { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTable> UserTable { get; set; }
    }
}
