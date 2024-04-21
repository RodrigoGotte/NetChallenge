using NetChallenge.Dto.Input;
using NetChallenge.Infrastructure;
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

    public class LocationValidations
    {       
        public Location Add(AddLocationRequest request)
        {
            try
            {
                var list = new LocationRepository().AsEnumerable(); 
                //TODO:MEJORAR ESTO
                if (request.Name != string.Empty && request.Name != null && request.Neighborhood != string.Empty && request.Neighborhood != null)
                {
                    if (LocationNameExist(list,request)) 
                    {
                        throw new ArgumentException();
                    }
                    return new Location(request.Name, request.Neighborhood);
                }
                else
                {
                    throw new ArgumentNullException(nameof(request.Name));
                }             
            }
            //TODO CUSTOM EXCEPTIONS
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, nameof(request));
            }
        }

        private bool LocationNameExist(IEnumerable<Location> list, AddLocationRequest request)
        {
            return list.Where(x => x.Name == request.Name).Any();
        }
    }
    

}