using System.Collections.Generic;
using NetChallenge.Abstractions;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class BookingRepository : IBookingRepository
    {
        public IEnumerable<Booking> AsEnumerable()
        {
            return Datastore.Datastore.bookings;
        }

        public void Add(Booking item)
        {
            Datastore.Datastore.bookings.Add(item);
        }
    }
}