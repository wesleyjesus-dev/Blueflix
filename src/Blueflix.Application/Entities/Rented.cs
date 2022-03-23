using System.ComponentModel.DataAnnotations;

namespace Blueflix.Application.Entities
{
    public class Rented : Notification
    {
        public Guid RentedId { get; set; }
        public Guid ClientId { get; set; }
        public string ImdbId { get; set; }
        public DateTime LeaseDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; private set; }

        public bool AvailableForLease()
        {
            var availableForLease = DateTime.Today.Date > ReturnDate.Date;
            if (!availableForLease) Notifications.Add("The film is not available for rental");
            return availableForLease;
        }
    }
}
