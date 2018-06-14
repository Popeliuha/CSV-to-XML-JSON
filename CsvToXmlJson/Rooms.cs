namespace CsvToXmlJson
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rooms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rooms()
        {
            Reservations = new HashSet<Reservations>();
        }

        [Key]
        public int RoomId { get; set; }

        public int RoomNumber { get; set; }

        public int СomfortLevel { get; set; }

        public int Capability { get; set; }

        public int HotelId { get; set; }

        public int Price { get; set; }

        public virtual Hotels Hotels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
