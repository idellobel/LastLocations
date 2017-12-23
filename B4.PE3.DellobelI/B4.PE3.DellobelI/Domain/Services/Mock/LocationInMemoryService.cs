using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Mock
{
    public class LocationInMemoryService : ILocationsService
    {
        static ObservableCollection<Location> inMemLocations = new ObservableCollection<Location>();
       
        /// <summary> 
        /// /// Gets all locations in memory collection /// 
        /// </summary>
        public async Task<IEnumerable<Location>> GetAll()
        {
            await Task.Delay(0);
            return inMemLocations;
        }

        /// <summary> 
        /// Gets a locations from memory collection based on Id /// 
        /// </summary> 
        public async Task<Location> GetById(Guid id)
        {
            await Task.Delay(0);
            return inMemLocations.FirstOrDefault(cm => cm.LocationId == id);
        }

        /// <summary> 
        /// Gets most recent location from memory collection  /// 
        /// </summary> 
        public async Task<Location> GetRecentstLocation()
        {
            await Task.Delay(0);
            return inMemLocations
                .OrderByDescending(c => c.TimeLocation.Date)
                .ThenBy(c => c.TimeLocation.TimeOfDay)
                .LastOrDefault();
        }
        /// <summary> 
        /// /// Saves a location to memory collection. Updates if existing, Adds if non-existing /// 
        /// </summary> 

        public async Task Save(Location location)
        {

            var savedlocation = await GetById(location.LocationId);
            if (savedlocation == null)
            {
                savedlocation = new Location();
                inMemLocations.Add(savedlocation);
            }
            savedlocation.LocationId = location.LocationId;
            savedlocation.LocationName = location.LocationName ;
            savedlocation.TimeLocation = location.TimeLocation;
            savedlocation.Latitude = location.Latitude;
            savedlocation.Longitude = location.Longitude;
        }

        public async Task DeleteLocation(Guid LocationId)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            var locationToDelete = inMemLocations.FirstOrDefault(l => l.LocationId == LocationId);
            inMemLocations.Remove(locationToDelete);
        }

    }
}
