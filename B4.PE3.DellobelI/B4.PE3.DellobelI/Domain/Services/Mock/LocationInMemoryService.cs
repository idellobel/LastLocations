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
        //{
            //new Location{
        //            LocationId = Guid.NewGuid(), LocationName="Thuis",
        //            Position = 1, Longitude = 51.134717, Latitude = 2.761236,
        //            TimeLocation = DateTime.UtcNow

        //        },
        //        new Location{
        //            LocationId = Guid.NewGuid(), LocationName="AlberMonument",
        //            Position = 2, Longitude = 51.136070, Latitude = 2.755743,
        //            TimeLocation = DateTime.UtcNow

        //        },
        //       new Location{
        //            LocationId = Guid.NewGuid(), LocationName="Vismijn",
        //            Position = 3, Longitude = 51.133607,  Latitude = 2.748845,
        //            TimeLocation = DateTime.UtcNow

        //        }
        //};
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
            return inMemLocations.FirstOrDefault(cm => cm.Id == id);
        }

        /// <summary> 
        /// Gets first location from memory collection  /// 
        /// </summary> 
        public async Task<Location> GetFirst()
        {
            await Task.Delay(0);
            return inMemLocations.First();
        }
        /// <summary> 
        /// /// Saves a location to memory collection. Updates if existing, Adds if non-existing /// 
        /// </summary> 

        public async Task Save(Location location)
        {

            var savedlocation = await GetById(location.Id);
            if (savedlocation == null)
            {
                savedlocation = new Location();
                inMemLocations.Add(savedlocation);
            }
            savedlocation.Id = location.Id;
            savedlocation.LocationName = location.LocationName ;
            savedlocation.TimeLocation = location.TimeLocation;
            savedlocation.Latitude = location.Latitude;
            savedlocation.Longitude = location.Longitude;
        }

        public async Task DeleteLocation(Guid LocationId)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            var locationToDelete = inMemLocations.FirstOrDefault(l => l.Id == LocationId);
            inMemLocations.Remove(locationToDelete);
        }

    }
}
