using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Mock
{
    public class LocationGroupInMemoryService : ILocationGroupsService
    {
        private static List<LocationGroup> locationGroupList;
        private static List<LocationGroup> LocationGroupList
        {
            get
            {
                if (locationGroupList == null)
                    locationGroupList = InitializeLocationGroupList();
                return locationGroupList;
            }
        }

        private static List<LocationGroup> InitializeLocationGroupList()
        {
            var locations = new List<LocationGroup>
            {
                new LocationGroup{
                    Id = Guid.NewGuid(),
                    OwnerId = Guid.Empty, //the first user
                    Title = "LocatieLijst Nieuwpoort",
                    Description = "Mijn regio",
                    //IsFavorite = true,

                }
            };


            //items for first locationGroup
            locations[0].LocationItems = new List<Location>
            {
                new Location{
                    LocationId = Guid.NewGuid(), LocationName="Thuis",
                    Position = 1, Longitude = 51.134717, Latitude = 2.761236,
                    TimeLocation = Convert.ToDateTime(" 02 December 2017 08:03:38 "),
                    LocationGroup = locations[0],
                    LocationGroupId = locations[0].Id
                },
                new Location{
                    LocationId = Guid.NewGuid(), LocationName="AlbertMonument",
                    Position = 2, Longitude = 51.136070, Latitude = 2.755743,
                    TimeLocation = Convert.ToDateTime(" 25 November 2017 12:05:14"),
                    LocationGroup = locations[0],
                    LocationGroupId = locations[0].Id
                },
               new Location{
                    LocationId = Guid.NewGuid(), LocationName="Vismijn",
                    Position = 3, Longitude = 51.133607,  Latitude = 2.748845,
                    TimeLocation = Convert.ToDateTime(" 18 November 2017 17:30:00 "),
                    LocationGroup = locations[0],
                    LocationGroupId = locations[0].Id
                }
            };
            return locations;
        }

        public async Task<IEnumerable<LocationGroup>> GetLocationGrouplistForUser(Guid userid)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            return LocationGroupList.Where(b => b.OwnerId == userid);
        }

       
        public async Task<LocationGroup> GetLocationGroupList(Guid locationGroupId)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            return LocationGroupList.FirstOrDefault(b => b.Id == locationGroupId);
        }



        public async Task SaveLocationGroupList(LocationGroup locationGroup)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            var savedLocationGroup = LocationGroupList.FirstOrDefault(l => l.Id == locationGroup.Id);
            if (savedLocationGroup == null) //this is a new locationGroup
            {
                savedLocationGroup = locationGroup;
                savedLocationGroup.Id = Guid.NewGuid();
                LocationGroupList.Add(savedLocationGroup);
            }
            savedLocationGroup.Title = locationGroup.Title;
            savedLocationGroup.Description = locationGroup.Description;
            savedLocationGroup.IsFavorite = locationGroup.IsFavorite;
            savedLocationGroup.OwnerId = locationGroup.OwnerId;
            savedLocationGroup.LocationItems = locationGroup.LocationItems;
        }

        public async Task DeleteLocationGroupList(Guid locationGroupId)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            var locationGroup = LocationGroupList.FirstOrDefault(l => l.Id == locationGroupId);
            LocationGroupList.Remove(locationGroup);
        }

     
    }
}
