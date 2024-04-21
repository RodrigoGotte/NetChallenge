using System.Collections.Generic;
using NetChallenge.Abstractions;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class LocationRepository : ILocationRepository
    {
        public IEnumerable<Location> AsEnumerable()
        {
            return Datastore.Datastore.locations;
        }

        public void Add(Location item)
        {
            Datastore.Datastore.locations.Add(item);
        }
    }
}