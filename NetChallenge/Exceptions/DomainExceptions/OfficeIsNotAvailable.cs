using System;

namespace NetChallenge.Exceptions.DomainExceptions.OfficeExceptions
{
    public class OfficeIsNotAvailable : Exception
    {
        public OfficeIsNotAvailable() :
            base("The office is not available!") { }
    }
}
