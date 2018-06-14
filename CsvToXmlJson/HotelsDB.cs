namespace CsvToXmlJson
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HotelsDB : DbContext
    {
        public HotelsDB()
            : base("name=HotelsDB")
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Hotels> Hotels { get; set; }
        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>()
                .Property(e => e.ClientName)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.IsRegular)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hotels>()
                .Property(e => e.HotelName)
                .IsUnicode(false);

            modelBuilder.Entity<Hotels>()
                .Property(e => e.Adress)
                .IsUnicode(false);

            modelBuilder.Entity<Hotels>()
                .Property(e => e.IsActive)
                .IsUnicode(false);

            modelBuilder.Entity<Hotels>()
                .Property(e => e.Rating)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Hotels>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Hotels)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reservations>()
                .Property(e => e.Breakfast)
                .IsUnicode(false);

            modelBuilder.Entity<Reservations>()
                .Property(e => e.Gym)
                .IsUnicode(false);

            modelBuilder.Entity<Reservations>()
                .Property(e => e.SwimPool)
                .IsUnicode(false);

            modelBuilder.Entity<Rooms>()
                .HasMany(e => e.Reservations)
                .WithRequired(e => e.Rooms)
                .WillCascadeOnDelete(false);
        }
    }
}
