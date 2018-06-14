namespace CsvToXmlJson
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hotels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hotels()
        {
            Rooms = new HashSet<Rooms>();
        }

        [Key]
        public int HotelId { get; set; }

        [Required]
        [StringLength(20)]
        public string HotelName { get; set; }

        public int? StarsCount { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreationDate { get; set; }

        [StringLength(20)]
        public string Adress { get; set; }

        [StringLength(20)]
        public string IsActive { get; set; }

        public int? AvaliableRoomsCount { get; set; }

        public int? SeasonalDiscount { get; set; }

        public int? RegularDiscount { get; set; }

        public int Capacity { get; set; }

        public decimal Rating { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
