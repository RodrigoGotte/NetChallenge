using System;

namespace NetChallenge.Exceptions.DomainExceptions
{
    public class LocationExist : Exception
    {
        public LocationExist() : base("The location already exists!") { }
    }
}
