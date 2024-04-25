using NetChallenge.Dto.Input;
using NetChallenge.Dto.Output;
using NetChallenge.Exceptions.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetChallenge.Domain
{
    public class Booking
    {
        public string LocationName { get; set; }
        public DateTime Reservation { get; set; }
        public TimeSpan Duration { get; set; }
        public string User { get; set; }
        public string OfficeName { get; set; }

        public Booking(string locationName, DateTime reservation,TimeSpan duration, string user, string officeName) 
        {
            LocationName = locationName;
            Reservation = reservation;
            Duration = duration;
            User = user;
            OfficeName = officeName;
        }
    }

    public class BookingValidations 
    {
        public Booking Add(BookOfficeRequest book, IEnumerable<BookingDto> bookingsCreated, IEnumerable<OfficeDto> officesCreated) 
        {
            try
            {                
                if (BookOfficeIsNotValid(officesCreated,book))
                {
                    throw new BookOfficeIsNotValid();                                        
                }                    
                if (OfficeIsNotAvailable(bookingsCreated,book))                 
                {                
                    throw new OfficeIsNotAvailable();                    
                }
                return new Booking
                    (
                    book.LocationName,
                    book.DateTime,
                    book.Duration,
                    book.UserName,
                    book.OfficeName
                    );
            }
            catch (BookOfficeIsNotValid) { throw new BookOfficeIsNotValid(); }
            catch (OfficeIsNotAvailable) { throw new OfficeIsNotAvailable(); }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }            
        }

        private bool OfficeIsNotAvailable(IEnumerable<BookingDto> bookingsCreated, BookOfficeRequest book)
        {
           
           return bookingsCreated.Any(x =>
                    x.DateTime == book.DateTime ||
                   (x.DateTime + x.Duration > book.DateTime && book.DateTime > x.DateTime)||
                   (book.DateTime + book.Duration > x.DateTime && x.DateTime > book.DateTime)); 
        }

        private bool BookOfficeIsNotValid(IEnumerable<OfficeDto> officesCreated, BookOfficeRequest book)
        {
            return  !officesCreated.Any(x => x.Name == book.OfficeName);
        }
    } 
}