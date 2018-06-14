namespace CsvToXmlJson
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reservations
    {
        [Key]
        public int ReservationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReservationDate { get; set; }

        public int ClientId { get; set; }

        public int RoomId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDay { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(10)]
        public string Breakfast { get; set; }

        [Required]
        [StringLength(10)]
        public string Gym { get; set; }

        [Required]
        [StringLength(10)]
        public string SwimPool { get; set; }

        public virtual Clients Clients { get; set; }

        public virtual Rooms Rooms { get; set; }
    }
}
