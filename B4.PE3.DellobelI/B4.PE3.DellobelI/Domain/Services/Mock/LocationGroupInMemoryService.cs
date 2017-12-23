using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Mock
{
    public class LocationGroupInMemoryService : ILocationGroupsService
    {
        private static List<LocationGroup> locationGroupLists;
        private static List<LocationGroup> LocationGroupLists
        {
            get
            {
                if (locationGroupLists == null)
                    locationGroupLists = InitializeLocationGroupList();
                return locationGroupLists;
            }
        }

        private static List<LocationGroup> InitializeLocationGroupList()
        {
            var locations = new List<LocationGroup>
            {
                new LocationGroup{
                    Id = Guid.NewGuid(),
                    OwnerId = Guid.Empty, //the first user
                    Title = "Nieuwpoort",
                    Description = "Mijn regio"

                   

                },

                 new LocationGroup{
                    Id = Guid.NewGuid(),
                    OwnerId = Guid.Empty, //same user
                    Title = "Brugge",
                    Description = "Regio IVO/HOWEST"
                 },

                  new LocationGroup{
                    Id = Guid.NewGuid(),
                    OwnerId = Guid.Empty,  //same user
                    Title = "Brussel",
                    Description = "Hoofdstad"
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
                    TimeLocation = Convert.ToDateTime(" 14 November 2017 17:30:00 "),
                    LocationGroup = locations[0],
                    LocationGroupId = locations[0].Id
                }
            };

               //items for second locationGroup
            locations[1].LocationItems = new List<Location>
            {
                new Location{
                    LocationId = Guid.NewGuid(), LocationName="IVO/HOWEST",
                    Position = 1, Longitude = 51.192507, Latitude = 3.214015,
                    TimeLocation = Convert.ToDateTime(" 18 November 2017 18:03:40 "),
                    LocationGroup = locations[1],
                    LocationGroupId = locations[1].Id
                },
                new Location{
                    LocationId = Guid.NewGuid(), LocationName="Marktplein",
                    Position = 2, Longitude = 51.208988, Latitude = 3.224328,
                    TimeLocation = Convert.ToDateTime(" 28 November 2017 08:12:14"),
                    LocationGroup = locations[1],
                    LocationGroupId = locations[1].Id
                },
               new Location{
                    LocationId = Guid.NewGuid(), LocationName="Kinepolis",
                    Position = 3, Longitude = 51.179972,  Latitude = 3.202001,
                    TimeLocation = Convert.ToDateTime(" 12 December 2017 14:30:58 "),
                    LocationGroup = locations[1],
                    LocationGroupId = locations[1].Id
                }
            };

            //items for third locationGroup
            locations[2].LocationItems = new List<Location>
            {
                new Location{
                    LocationId = Guid.NewGuid(), LocationName="Grote Markt",
                    Position = 1, Longitude = 50.847052, Latitude = 4.352418,
                    TimeLocation = Convert.ToDateTime(" 06 December 2017 11:30:40 "),
                    LocationGroup = locations[2],
                    LocationGroupId = locations[2].Id
                },
                new Location{
                    LocationId = Guid.NewGuid(), LocationName="Luchthaven",
                    Position = 2, Longitude = 50.898498, Latitude = 4.483535,
                    TimeLocation = Convert.ToDateTime(" 16 November 2017 12:05:14"),
                    LocationGroup = locations[2],
                    LocationGroupId = locations[2].Id
                },
               new Location{
                    LocationId = Guid.NewGuid(), LocationName="Atomium",
                    Position = 3, Longitude = 50.895170,  Latitude = 4.341302,
                    TimeLocation = Convert.ToDateTime(" 26 November 2017 17:30:00 "),
                    LocationGroup = locations[2],
                    LocationGroupId = locations[2].Id
                }
            };
            return locations;
        }
        //public async Task<IEnumerable<LocationGroup>> GetLocationGrouplistMemory()
        //{
        //    await Task.Delay(Constant.Mocking.FakeDelay);
        //    return LocationGroupLists;
        //}


        public async Task<IEnumerable<LocationGroup>> GetLocationGrouplistForUser(Guid userid)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            return LocationGroupLists.Where(b => b.OwnerId == userid);
        }

       
        public async Task<LocationGroup> GetLocationGroupList(Guid locationGroupId)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            return LocationGroupLists.FirstOrDefault(b => b.Id == locationGroupId);
        }



        public async Task SaveLocationGroupList(LocationGroup locationGroup)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            var savedLocationGroup = LocationGroupLists.FirstOrDefault(l => l.Id == locationGroup.Id);
            if (savedLocationGroup == null) //this is a new locationGroup
            {
                savedLocationGroup = locationGroup;
                savedLocationGroup.Id = Guid.NewGuid();
                LocationGroupLists.Add(savedLocationGroup);
            }
            savedLocationGroup.Title = locationGroup.Title;
            savedLocationGroup.Description = locationGroup.Description;
            savedLocationGroup.OwnerId = locationGroup.OwnerId;
            savedLocationGroup.LocationItems = locationGroup.LocationItems;
        }

        public async Task DeleteLocationGroupList(Guid locationGroupId)
        {
            await Task.Delay(Constant.Mocking.FakeDelay);
            var locationGroup = LocationGroupLists.FirstOrDefault(l => l.Id == locationGroupId);
            LocationGroupLists.Remove(locationGroup);
        }

        //public async Task SaveLocationGroup()
        //{
        //    await Task.Delay(Constant.Mocking.FakeDelay);
        //    var testLocationGroup = InitializeLocationGroupList();
        //}
    }
}
