using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Models
{
    public class Location
    {
        public Guid LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CurrentLocation { get; set; }
        public string LocationName { get; set; }
        public int Position { get; set; }
        public Guid LocationGroupId { get; set; }
        public LocationGroup LocationGroup { get; set; }
    }
}
