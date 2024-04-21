using System;
using System.ComponentModel.DataAnnotations;

namespace NetChallenge.Domain
{
    public class Booking
    {               
        public DateTime Reservation {  get; set; }

        [Required]
        [Range(0, Int64.MaxValue)]
        public decimal Duration { get; set; }
        
        [Required]
        public string User { get; set; }
        public string OfficeName { get; set; }

        public Booking( DateTime reservation,int duration, string user, string officeName) 
        {
            Reservation = reservation;
            Duration = duration;
            User = user;
            OfficeName = officeName;
        }
    }
}