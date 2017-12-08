using SQLite.Net.Attributes;
using System;

namespace B4.PE3.DellobelI.Domain.Models
{
    public class AppSettings
    {
        [PrimaryKey]
        public Guid CurrentUserId { get; set; }

        public bool EnableListSharing { get; set; }
        public bool EnableNotifications { get; set; }
    }
}
