using NetChallenge.Dto.Input;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetChallenge.Domain
{
    public class Booking
    {               
        public DateTime Reservation {  get; set; }

        [Required]
        [Range(0, Int64.MaxValue)]
        public TimeSpan Duration { get; set; }
        
        [Required]
        public string User { get; set; }
        public string OfficeName { get; set; }

        public Booking( DateTime reservation,TimeSpan duration, string user, string officeName) 
        {
            Reservation = reservation;
            Duration = duration;
            User = user;
            OfficeName = officeName;
        }
    }

    public class BookingValidations 
    {

        public Booking Add(BookOfficeRequest book) 
        {
            try
            {
                if (book.UserName != string.Empty || book.Duration > TimeSpan.Zero) 
                { 
                    if (OfficeisValid())
                    {
                        throw new InvalidOperationException();
                    }
                    if (OfficeIsAvailable()) 
                    {
                        throw new Exception();
                    }
                }
                return null;
            }
            catch(Exception ex)
            { 
                throw new Exception(ex.Message);
            }            
        }

        private bool OfficeIsAvailable()
        {
            throw new NotImplementedException();
        }

        private bool OfficeisValid()
        {
            throw new NotImplementedException();
        }
    } 
}