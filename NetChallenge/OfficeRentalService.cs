using System;
using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using NetChallenge.Exceptions.ServiceExceptions;

namespace NetChallenge
{
    public class OfficeRentalService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IBookingRepository _bookingRepository;

        public OfficeRentalService(ILocationRepository locationRepository, IOfficeRepository officeRepository, IBookingRepository bookingRepository)
        {
            _locationRepository = locationRepository;
            _officeRepository = officeRepository;
            _bookingRepository = bookingRepository;
        }

        public void AddLocation(AddLocationRequest request)
        {
            try
            {
                if (request.Name != string.Empty && request.Name != null && request.Neighborhood != string.Empty && request.Neighborhood != null)
                {
                    var response = new LocationValidations().Add(request, GetLocations());
                    _locationRepository.Add(response);
                }
                else
                {
                    throw new RequestNotValid();
                }
            }
            catch (RequestNotValid)
            {
                throw new RequestNotValid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddOffice(AddOfficeRequest request)
        {
            try
            {
                if (request.MaxCapacity > 0 && request.Name != string.Empty && request.Name != null)
                {
                    var response = new OfficeValidations().Add(request, GetLocations(), GetOffices(request.LocationName));
                    _officeRepository.Add(response);
                }
                else
                {
                    throw new RequestNotValid();
                }
            }
            catch (RequestNotValid)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void BookOffice(BookOfficeRequest request)
        {
            try
            {
                if (request.UserName != string.Empty && request.UserName != null && request.Duration > TimeSpan.Zero)
                {
                    var response = new BookingValidations().Add(request, GetBookings(request.LocationName, request.OfficeName), GetOffices(request.LocationName));
                    _bookingRepository.Add(response);
                }
                else
                {
                    throw new RequestNotValid();
                }
            }
            catch (RequestNotValid)
            {
                throw new RequestNotValid();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<BookingDto> GetBookings(string locationName, string officeName)
        {
            var bookingsCreated = _bookingRepository.AsEnumerable().Where(x => x.LocationName == locationName && x.OfficeName == officeName);
            var response = MapperToDto(bookingsCreated);
            return response;
        }

        public IEnumerable<LocationDto> GetLocations()
        {
            var locationCreated = _locationRepository.AsEnumerable();
            var response = MapperToDto(locationCreated);            
            return response;
        }

        public IEnumerable<OfficeDto> GetOffices(string locationName)
        {
            var officeCreated = _officeRepository.AsEnumerable().Where(x => locationName == x.LocationName);
            var response = MapperToDto(officeCreated);
            return response;
        }

        public IEnumerable<OfficeDto> GetOfficeSuggestions(SuggestionsRequest request)
        {
            IEnumerable<OfficeDto> officesCreated = MapperToDto(_officeRepository.AsEnumerable());
            IEnumerable<OfficeDto> response = new List<OfficeDto>();
            var service = new SuggestedOffices();
            if (request.CapacityNeeded <= 1 && request.PreferedNeigborHood == null && request.ResourcesNeeded.Count() == 0) 
            {
                return officesCreated.OrderBy(x => x.AvailableResources.Count());
            }
            if (request.PreferedNeigborHood != null)
            {
                response = response.Concat(service.GetOfficesForNeighborhood(request, officesCreated, GetLocations()));
            }
            if (request.CapacityNeeded > 1)
            {
                response = service.GetOfficesForCapacity(request, officesCreated);
            }
            if (request.ResourcesNeeded.Any()) 
            {
                response = response.Concat(service.GetOfficesForResources(request, officesCreated));
            }
            
            return response.Distinct();
        }

        private IEnumerable<OfficeDto> MapperToDto(IEnumerable<Office> domain) 
        {
            var response = new List<OfficeDto>();
            foreach (var office in domain)
                response.Add(new OfficeDto
                {
                    Name = office.Name,
                    LocationName = office.LocationName,
                    AvailableResources = office.Resources,
                    MaxCapacity = office.Capacity
                });
            return response;


        }
        private IEnumerable<LocationDto> MapperToDto(IEnumerable<Location> domain)         
        {                
            var response = new List<LocationDto>();                   
            foreach (var location in domain)                    
            {
                response.Add(new LocationDto
                {
                    Name = location.Name,
                    Neighborhood = location.Neighborhood 
                });
            }
            return response;
        }

        private IEnumerable<BookingDto> MapperToDto(IEnumerable<Booking> domain)
        {
            var response = new List<BookingDto>();
            foreach (var booking in domain)
            {
                response.Add(new BookingDto
                {
                    LocationName = booking.LocationName,
                    DateTime = booking.Reservation,
                    Duration = booking.Duration,
                    OfficeName = booking.OfficeName,
                    UserName = booking.User
                });
            }
            return response;
        }
    }
}