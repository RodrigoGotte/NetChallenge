using System;

namespace NetChallenge.Exceptions.ServiceExceptions
{
    public class ModelStateNotValid : Exception
    {
        public ModelStateNotValid() : 
            base("The request not have the information required to do the task") 
        {        
        }
    }
}
