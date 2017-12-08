using B4.PE3.DellobelI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Abstract
{
    public interface ILocationGroupsService
    {
        Task DeleteLocationGroupList(Guid LocationGroupId);
        Task<LocationGroup> GetLocationGroupList(Guid LocationGroupId);
        Task<IEnumerable<LocationGroup>> GetLocationGrouplistForUser(Guid userid);
        Task SaveLocationGroupList(LocationGroup locationGroup);
    }
}
