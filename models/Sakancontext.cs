using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sakan_project.models
{
    public class Sakancontext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Owners> Owners { get; set; }
        public DbSet<Students> Students { get; set; }

        public DbSet<Universities> Universities { get; set; }

        public DbSet<Colleges> Colleges { get; set; }
        public DbSet<Studentbooking> Studentbookings { get; set; }
        public DbSet<Bookingapartmentroom> Booking_apartment_rooms { get; set; }
        public DbSet<Rooms> Rooms { get; set; }

        public Sakancontext() : base()
        {

        }
        public Sakancontext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Composite Primary Key
            modelBuilder.Entity<Bookingapartmentroom>()
                .HasKey(bar => new { bar.BookingId, bar.RoomId });

            // Define Foreign Keys
            modelBuilder.Entity<Bookingapartmentroom>()
                .HasOne(bar => bar.Booking)
                .WithMany(b => b.BookingApartmentRooms)
                .HasForeignKey(bar => bar.BookingId);

            modelBuilder.Entity<Bookingapartmentroom>()
                .HasOne(bar => bar.Room)
                .WithMany(r => r.BookingApartmentRooms)
                .HasForeignKey(bar => bar.RoomId);

            modelBuilder.Entity<Bookingapartmentroom>()
                .HasOne(bar => bar.Apartment)
                .WithMany(a => a.BookingApartmentRooms)
                .HasForeignKey(bar => bar.ApartmentId);



            modelBuilder.Entity<Studentbooking>()
                .HasKey(sb => new { sb.BookingId });

            modelBuilder.Entity<Studentbooking>()
                .HasOne(sb => sb.Booking)
                .WithMany(b => b.StudentsBookings)
                .HasForeignKey(sb => sb.BookingId);

            modelBuilder.Entity<Studentbooking>()
                .HasOne(sb => sb.Student)
                .WithMany(s => s.StudentsBookings)
                .HasForeignKey(sb => sb.StudentId);

            base.OnModelCreating(modelBuilder);

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(" Server=DESKTOP-6O0IEQG\\SQLEXPRESS; Database=SAKAN_PROJECT1;Integrated Security=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

    }





}
