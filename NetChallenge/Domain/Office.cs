using System;
using System.ComponentModel.DataAnnotations;

namespace NetChallenge.Domain
{
    public class Office
    {        

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, Int64.MaxValue)]
        public int Capacity { get; set; }

        public string LocalName { get; set; }
                
        public string[] Resources { get; set; }

        public Office( string name, int capacity, string[] resources, string localname)
        {            
            Name = name;
            Capacity = capacity;
            Resources = resources;
            LocalName = localname;
        }
    }

    
}