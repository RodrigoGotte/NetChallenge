using NetChallenge.Domain;
using System.Collections.Generic;

namespace NetChallenge.Infrastructure.Datastore
{
    public static class Datastore
    {
        public static List<Location> locations = new List<Location>();

        public static List<Office> offices = new List<Office>();

        public static List<Booking> bookings = new List<Booking>();
    }
}
