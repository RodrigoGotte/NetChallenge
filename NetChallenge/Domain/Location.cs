using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Location
    {
        public string Name { get; set; }
        public string City { get; set; }

        public Location(string name, string city)
        {
            Name = name;
            City = city;
        }
    }

    public class LocationValidations
    {
        public Location Add(AddLocationRequest request, IEnumerable<LocationDto> locationsCreated)
        {
            try
            {                
                if (LocationNameExist(locationsCreated, request))
                {
                    throw new ArgumentException();
                }
                return new Location(request.Name, request.Neighborhood);                                         
            }
            //TODO CUSTOM EXCEPTIONS
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, nameof(request));
            }
        }

        private bool LocationNameExist(IEnumerable<LocationDto> locationCreated, AddLocationRequest request)
        {
            return locationCreated.Any(x => x.Name == request.Name);
        }
    }
    

}