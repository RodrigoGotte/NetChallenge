using System;

namespace NetChallenge.Exceptions.DomainExceptions
{
    public class BookOfficeIsNotValid : Exception
    {
        public BookOfficeIsNotValid() :
            base("The office is invalid because it was already created in this location")
        { }
    }
}
