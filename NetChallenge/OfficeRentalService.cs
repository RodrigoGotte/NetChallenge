using System;
using System.Collections.Generic;
using System.Linq;
using NetChallenge.Abstractions;
using NetChallenge.Domain;
using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;

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
                var response = new AddLocations().AddValidation(request);
                _locationRepository.Add(response);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }            
        }

        public void AddOffice(AddOfficeRequest request)
        {
            throw new NotImplementedException();
        }

        public void BookOffice(BookOfficeRequest request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookingDto> GetBookings(string locationName, string officeName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocationDto> GetLocations()
        {
            var list = _locationRepository.AsEnumerable();
            var response = new List<LocationDto>();
            foreach (var location in list)
            {
                response.Add(new LocationDto
                {
                    Name = location.Name,
                    Neighborhood = location.City
                });
            }
            return response;           
        }

        public IEnumerable<OfficeDto> GetOffices(string locationName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OfficeDto> GetOfficeSuggestions(SuggestionsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}