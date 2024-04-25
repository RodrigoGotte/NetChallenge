using System;

namespace NetChallenge.Exceptions.DomainExceptions
{
    public class OfficeIsNotValid : Exception
    {
        public OfficeIsNotValid() :
            base("The office is invalid because it was already created in this location")
        { }
    }
}
