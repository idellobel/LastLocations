using B4.PE3.DellobelI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Abstract
{
    public interface ILocationsService
    {
        Task<IEnumerable<Location>> GetAll();
        Task<Location> GetById(Guid id);
        Task<Location> GetRecentstLocation();
        Task Save(Location location);
        Task DeleteLocation(Guid LocationId);
        
    }
}
