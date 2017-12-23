using B4.PE3.DellobelI.Domain.Models;
using SQLiteNetExtensions.Extensions;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.SQLite
{
    public class LocationGroupsSQLiteService : SQLiteServiceBase, ILocationGroupsService
    {

        public async Task DeleteLocationGroupList(Guid locationGroupId)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.Delete<LocationGroup>(locationGroupId);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<LocationGroup> GetLocationGroupList(Guid locationGroupId)
        {
            return await Task.Run<LocationGroup>(() =>
            {
                try
                {
                    LocationGroup locationGroup = connection.Table<LocationGroup>().Where(e => e.Id == locationGroupId).FirstOrDefault();
                    if (locationGroup != null)
                     connection.GetChildren<LocationGroup>(locationGroup, true);
                    return locationGroup;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<IEnumerable<LocationGroup>>GetLocationGrouplistForUser(Guid userid)
        {
            return await Task.Run<IEnumerable<LocationGroup>>(() =>
            {
                try
                {
                    var locationGroups = connection.GetAllWithChildren<LocationGroup>(b => b.OwnerId == userid, false).ToList();
                    return locationGroups;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        //public Task<IEnumerable<LocationGroup>> GetLocationGrouplistMemory()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task SaveLocationGroupList(LocationGroup locationGroup)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplaceWithChildren(locationGroup);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}

