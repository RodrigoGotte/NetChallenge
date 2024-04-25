using System;

namespace NetChallenge.Exceptions.DomainExceptions
{
    public class LocationNameExist : Exception
    {
        public LocationNameExist() : base("This neighborhood is already existing") { }
    }
}
