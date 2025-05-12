namespace Sakan_project.models
{
    public class Booking
    {
        
        
            public int Id { get; set; }
            public string PropertyType { get; set; }
            public int PropertyId { get; set; }
            public int Day { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public string Status { get; set; }
            public int AmountPaid { get; set; }
            public virtual ICollection<Studentbooking> StudentsBookings { get; set; }
            public virtual ICollection<Bookingapartmentroom> BookingApartmentRooms { get; set; }
        }

    }

