using NetChallenge.Dto.Input;
using NetChallenge.Infrastructure.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Location
    {                               
        public  string Name {  get; set; }
        public  string City { get; set; }

        public Location(string name, string city) 
        {            
            Name = name;
            City = city;
        }        
    }

    public class AddLocations
    {       
        public Location Add(AddLocationRequest request)
        {
            try
            {
               
                var list =  Datastore.locations.ToList();
                if (request.Name == string.Empty || request.Name == null || list.Where(x => x.Name == request.Name).Count() == 1 )
                {
                    throw new ArgumentNullException(nameof(request.Name));
                }
                if (request.Neighborhood == string.Empty || request.Neighborhood == null)
                {
                    throw new ArgumentNullException(nameof(request.Neighborhood));
                }              
                return new Location(request.Name, request.Neighborhood);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, nameof(request));
            }
        }
    }
    

}