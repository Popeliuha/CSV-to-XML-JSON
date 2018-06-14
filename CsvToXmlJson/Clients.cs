namespace CsvToXmlJson
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            Reservations = new HashSet<Reservations>();
        }

        [Key]
        public int ClientId { get; set; }

        [Required]
        [StringLength(20)]
        public string ClientName { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [Required]
        [StringLength(6)]
        public string Gender { get; set; }

        [Required]
        [StringLength(10)]
        public string IsRegular { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
