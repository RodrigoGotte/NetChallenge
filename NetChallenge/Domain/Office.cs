using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using NetChallenge.Exceptions.DomainExceptions;
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
                    throw new OfficeIsNotValid();
                }
                if (!LocationExists(locationsCreated, office))
                {
                    throw new LocationExist();
                }
                return new Office
                    (
                    office.Name,
                    office.MaxCapacity,
                    office.AvailableResources.ToArray(),
                    office.LocationName
                    );
            }
            catch (OfficeIsNotValid) { throw new OfficeIsNotValid(); }
            catch (LocationExist) { throw new LocationExist(); }
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

    public class SuggestedOffices
    {
        internal IEnumerable<OfficeDto> GetOfficesForCapacity(SuggestionsRequest request, IEnumerable<OfficeDto> officesCreated)
        {

            var response = officesCreated.Where(x =>
                                                    (x.MaxCapacity - request.CapacityNeeded == -1 )
                                                    || (Math.Abs(x.MaxCapacity - request.CapacityNeeded) < 5 
                                                    && request.CapacityNeeded <= x.MaxCapacity))
                                                    .OrderByDescending(x=>x.MaxCapacity);           
            return response;
        }

        internal IEnumerable<OfficeDto> GetOfficesForNeighborhood(SuggestionsRequest request,IEnumerable<OfficeDto> officesCreated, IEnumerable<LocationDto> locationsCreated)
        {
            var response = officesCreated;
            if (locationsCreated.Any(x => x.Neighborhood == request.PreferedNeigborHood)) 
            { 
                 response = officesCreated.Join
                              (locationsCreated,
                                OfficeDto => OfficeDto.LocationName,
                                locationDto => locationDto.Name,
                                (x, y) => new
                                {
                                    OfficeDto = x, LocationDto = y
                                }).OrderByDescending(x => x.LocationDto.Neighborhood == request.PreferedNeigborHood)
                                .Select(x => x.OfficeDto) ;
            }
            return response;
        }

        internal IEnumerable<OfficeDto> GetOfficesForResources(SuggestionsRequest request, IEnumerable<OfficeDto> officesCreated)
        {
            var response = officesCreated.Where(x =>
                                                x.AvailableResources.Any(
                                                                        y => request.ResourcesNeeded.Any(r => r == y)));
            if (response.Any()) 
            { 
                var officeResourcesLessCantity = response.Select(x => x.AvailableResources.Count()).Min();
                response.Where(x => x.AvailableResources.Count() == officeResourcesLessCantity);
            }
            return response;
        }
    }


}