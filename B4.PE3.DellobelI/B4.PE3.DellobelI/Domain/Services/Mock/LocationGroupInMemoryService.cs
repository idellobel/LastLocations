using B4.PE3.DellobelI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Mock
{
    public class LocationGroupInMemoryService
    { 
    private static List<LocationGroup> locationsList;
    private static List<LocationGroup> LocationsList
        {
        get
        {
            if (locationsList == null)
                    locationsList = InitializeLocationsList();
            return locationsList;
        }
    }

    private static List<LocationGroup> InitializeLocationsList()
    {
        var locationGroup = new List<LocationGroup>
            {
                new LocationGroup{
                    Id = Guid.NewGuid(),
                    OwnerId = Guid.Empty, //the first user
                    Title = "Siegfried's first bucket list",
                    Description = "A simple bucket list",
                    IsFavorite = true,

                }
            };

        //     public Guid LocationId { get; set; }
        //public double Latitude { get; set; }
        //public double Longitude { get; set; }
        //public DateTime CurrentLocation { get; set; }
        //public string LocationName { get; set; }
        //public int Position { get; set; }

        //items for first bucket
        locationGroup[0].LocationItems = new List<Location>
            {
                new Location{
                    LocationId = Guid.NewGuid(), LocationName="Thuis",
                    Position = 1, Longitude = 51.134717, Latitude = 2.761236,
                    CurrentLocation = DateTime.UtcNow,
                    LocationGroup = locationGroup[0],
                    LocationGroupId = locationGroup[0].Id
                },
                new Location{
                    LocationId = Guid.NewGuid(), LocationName="AlberMonument",
                    Position = 1, Longitude = 51.136070, Latitude = 2.755743,
                    CurrentLocation = DateTime.UtcNow,
                    LocationGroup = locationGroup[0],
                    LocationGroupId = locationGroup[0].Id
                },
               new Location{
                    LocationId = Guid.NewGuid(), LocationName="Vismijn",
                    Position = 1, Longitude = 51.133607,  Latitude = 2.748845,
                    CurrentLocation = DateTime.UtcNow,
                    LocationGroup = locationGroup[0],
                    LocationGroupId = locationGroup[0].Id
                }
            };
        return locationGroup;
    }

    public async Task<IEnumerable<LocationGroup>> GetLocationGroupListsForUser(Guid userid)
    {
        await Task.Delay(Constants.Mocking.FakeDelay);
        return LocationsList.Where(b => b.OwnerId == userid);
    }

    public async Task<LocationGroup> GetLocationGroupList(Guid bucketId)
    {
        await Task.Delay(Constants.Mocking.FakeDelay);
        return LocationsList.FirstOrDefault(b => b.Id == bucketId);
    }

    public async Task SaveBucketList(LocationGroup bucket)
    {
        await Task.Delay(Constants.Mocking.FakeDelay);
        var savedLocationGroup = LocationsList.FirstOrDefault(b => b.Id == bucket.Id);
        if (savedLocationGroup == null) //this is a new bucket
        {
            savedLocationGroup = bucket;
            savedLocationGroup.Id = Guid.NewGuid();
                LocationsList.Add(savedLocationGroup);
        }
        //savedLocationGroup.Title = bucket.Title;
        //savedLocationGroup.Description = bucket.Description;
        //savedLocationGroup.ImageUrl = bucket.ImageUrl;
        //savedLocationGroup.IsFavorite = bucket.IsFavorite;
        //savedLocationGroup.OwnerId = bucket.OwnerId;
        //savedLocationGroup.Items = bucket.Items;
    }

    public async Task DeleteLocationsList(Guid bucketId)
    {
        await Task.Delay(Constants.Mocking.FakeDelay);
        var bucket = LocationsList.FirstOrDefault(b => b.Id == bucketId);
            LocationsList.Remove(bucket);
    }
}
