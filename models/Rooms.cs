using System.ComponentModel.DataAnnotations.Schema;

namespace Sakan_project.models
{
    public class Rooms
    {
        
            public int Id { get; set; }
            public int PricePerMonth { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public string Title { get; set; }
            public bool IsAvailable { get; set; }

        [ForeignKey("Apartment")]
            public int ApartmentId { get; set; }
            public virtual Apartments Apartment { get; set; }
            public virtual ICollection<Bookingapartmentroom> BookingApartmentRooms { get; set; }
        }


    }

