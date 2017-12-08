using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Models
{
    public class User
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<LocationGroup> PinnedLocations { get; set; }
    }
}
