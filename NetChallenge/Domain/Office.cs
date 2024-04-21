using NetChallenge.Dto.Input;
using NetChallenge.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Office
    {        

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, Int64.MaxValue)]
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
                if (officesCreated.Where(x=> x.Name == office.Name && office.LocationName == x.LocationName).Count() == 0 && office.MaxCapacity > 0 && office.Name != string.Empty && office.Name != null && locations.Where(x => x.Name == office.LocationName).Count() == 1)
                {
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
    }
    
}