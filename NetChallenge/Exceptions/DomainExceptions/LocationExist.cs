using System;
using System.Collections.Generic;
using System.Text;

namespace NetChallenge.Exceptions.DomainExceptions
{
    public class LocationExist : Exception
    {
        public LocationExist() : base("The location already exists!") { }
    }
}
