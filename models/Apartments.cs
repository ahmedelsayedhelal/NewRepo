using System.ComponentModel.DataAnnotations.Schema;

namespace Sakan_project.models
{
    public class Apartments
    {
        
        
            public int Id { get; set; }
            public int PricePerMonth { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public string Title { get; set; }
            public string Location { get; set; }

             [ForeignKey("Owner")]
            public int OwnerId { get; set; }
            public virtual Owners Owner { get; set; }
            public virtual ICollection<Rooms> Rooms { get; set; }
            public virtual ICollection<Bookingapartmentroom> BookingApartmentRooms { get; set; }
        }

    }

