using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Models
{
    public class Location
    {
        [PrimaryKey]
        public Guid LocationId { get; set; }

        [NotNull]
        public double Latitude { get; set; }

        [NotNull]
        public double Longitude { get; set; }

        public double Altitude { get; set; }
        public DateTime TimeLocation { get; set; }

        public string LocationName { get; set; }
        public int Position { get; set; }

        [ForeignKey(typeof(LocationGroup))]
        public Guid LocationGroupId { get; set; }

        [ManyToOne(nameof(LocationGroupId))]
        public LocationGroup LocationGroup { get; set; }

        
    }
}
