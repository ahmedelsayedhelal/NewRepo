using System.ComponentModel.DataAnnotations.Schema;

namespace Sakan_project.models
{
    public class Bookingapartmentroom


 
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }

        public int ApartmentId { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Rooms Room { get; set; }
        public virtual Apartments Apartment { get; set; }
    }
}

