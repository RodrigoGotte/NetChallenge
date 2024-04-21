using NetChallenge.Dto.Input;
using NetChallenge.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Office
    {        
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string LocationName { get; set; }                
        public string[] Resources { get; set; }
        public Office( string name, int capacity, string[] resources, string localname)
        {            
            Name = name;
            Capacity = capacity;
            Resources = resources;
            LocationName = localname;
        }
    }

    public class OfficeValidations 
    {
        public Office Add(AddOfficeRequest office) 
        {
            var locations = new LocationRepository().AsEnumerable();
            var officesCreated = new OfficeRepository().AsEnumerable();
            
            try 
            {
                if(office.MaxCapacity > 0 && office.Name != string.Empty && office.Name != null) 
                { 
                    if (OfficeNameIsNotValid(officesCreated, office)) 
                    {
                        throw new InvalidOperationException();
                    }
                    if (!LocationExists(locations, office)) 
                    {
                        throw new Exception();
                    }
                    return new Office
                  (
                      office.Name,
                      office.MaxCapacity,
                      office.AvailableResources.ToArray(),
                      office.LocationName
                  );
                }                                                                 
                else
                    throw new ArgumentException();
            }
            catch (Exception ex)             
            {
                throw new Exception(ex.Message);
            }

        }
        private bool LocationExists(IEnumerable<Location> locations, AddOfficeRequest office)
        {
            return locations.Where(x => x.Name == office.LocationName).Any();
        }

        private bool OfficeNameIsNotValid(IEnumerable<Office> officesCreated, AddOfficeRequest office)
        {
            return officesCreated.Where(x => x.Name == office.Name && office.LocationName == x.LocationName).Any();
        }
    }
    
}