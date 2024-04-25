using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using NetChallenge.Exceptions.DomainExceptions;
using NetChallenge.Exceptions.DomainExceptions.OfficeExceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Location
    {
        public string Name { get; set; }
        public string Neighborhood { get; set; }

        public Location(string name, string neighborhood)
        {
            Name = name;
            Neighborhood = neighborhood;
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
                    throw new LocationNameExist();
                }
                return new Location(request.Name, request.Neighborhood);                                         
            }
            catch (LocationNameExist) { throw; }
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