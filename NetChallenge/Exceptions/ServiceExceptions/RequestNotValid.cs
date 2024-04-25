using System;

namespace NetChallenge.Exceptions.ServiceExceptions
{
    public class RequestNotValid : Exception
    {
        public RequestNotValid() : 
            base("The request not have the information required to do the task") 
        {        
        }
    }
}
