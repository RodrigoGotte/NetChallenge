using System.Collections.Generic;
using NetChallenge.Abstractions;
using NetChallenge.Domain;

namespace NetChallenge.Infrastructure
{
    public class OfficeRepository : IOfficeRepository
    {
        public IEnumerable<Office> AsEnumerable()
        {
            return Datastore.Datastore.offices;
        }

        public void Add(Office item)
        {
            Datastore.Datastore.offices.Add(item);
        }
    }
}