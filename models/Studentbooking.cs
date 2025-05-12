namespace Sakan_project.models
{
    public class Studentbooking
    {
        public int BookingId { get; set; }
        public int StudentId { get; set; }
        public virtual Booking Booking { get; set; }
        public  virtual Students Student { get; set; }
    }

}

