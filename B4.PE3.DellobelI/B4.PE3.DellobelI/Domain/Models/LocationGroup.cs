using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace B4.PE3.DellobelI.Domain.Models
{
    public class LocationGroup
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(User))]
        public Guid OwnerId { get; set; }

        [ManyToOne(nameof(OwnerId), CascadeOperations = CascadeOperation.CascadeRead)]
        public User Owner { get; set; }

        [NotNull, MaxLength(50)]
        public string Title { get; set; }

        [NotNull,MaxLength(50)]
        public string Description { get; set; }

       [OneToMany(CascadeOperations =CascadeOperation.All)]
        public List<Location> LocationItems { get; set; }
        
    }
}
