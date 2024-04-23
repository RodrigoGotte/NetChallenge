using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Office
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string LocationName { get; set; }
        public string[] Resources { get; set; }

        public Office(string name, int capacity, string[] resources, string localname)
        {
            Name = name;
            Capacity = capacity;
            Resources = resources;
            LocationName = localname;
        }
    }

    public class OfficeValidations
    {
        public Office Add(AddOfficeRequest office, IEnumerable<LocationDto> locationsCreated, IEnumerable<OfficeDto> officesCreated)
        {
            try
            {
                if (OfficeNameIsNotValid(officesCreated, office))
                {
                    throw new InvalidOperationException();
                }
                if (!LocationExists(locationsCreated, office))
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private bool LocationExists(IEnumerable<LocationDto> locations, AddOfficeRequest office)
        {
            return locations.Any(x => x.Name == office.LocationName);
        }

        private bool OfficeNameIsNotValid(IEnumerable<OfficeDto> officesCreated, AddOfficeRequest office)
        {
            return officesCreated.Any(x => x.Name == office.Name && office.LocationName == x.LocationName);
        }
    }
    
}