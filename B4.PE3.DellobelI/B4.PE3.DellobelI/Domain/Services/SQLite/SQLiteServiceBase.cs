using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using SQLite.Net;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace B4.PE3.DellobelI.Domain.Services.SQLite
{
    /// <summary>
    /// Basisklasse voor alle "SQLite" implementaties van een service
    /// </summary>
    public abstract class SQLiteServiceBase
    {
        protected readonly SQLiteConnection connection;

        public SQLiteServiceBase()
        {
            //get the platform-specific SQLiteConnection
            var connectionFactory = DependencyService.Get<ISQLiteConnectionFactory>();
            connection = connectionFactory.CreateConnection("lastlocationsdata.db1");

            ////verwijdert bestaande tabellen
            //connection.DropTable<User>();
            //connection.DropTable<AppSettings>();
            //connection.DropTable<LocationGroup>();
            //connection.DropTable<Location>();

            //maakt tabellen aan (indien ze niet bestaan)
            connection.CreateTable<User>();
            connection.CreateTable<AppSettings>();
            connection.CreateTable<LocationGroup>();
            connection.CreateTable<Location>();

            //seed

            if (connection.Table<LocationGroup>().Count() == 0)
            {
                // only insert the data if it doesn't already exist

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
                connection.InsertOrReplaceAll(locations);


               var locationItems = new List<Location>
                    {
                        new Location{
                            LocationId = Guid.NewGuid(), LocationName="Thuis",
                            Position = 1, Latitude = 51.134717, Longitude = 2.761236,
                            TimeLocation = Convert.ToDateTime(" 02 December 2017 08:03:38 "),
                            LocationGroup = locations[0],
                            LocationGroupId = locations[0].Id
                        },
                        new Location{
                            LocationId = Guid.NewGuid(), LocationName="AlbertMonument",
                            Position = 2, Latitude = 51.136070, Longitude = 2.755743,
                            TimeLocation = Convert.ToDateTime(" 25 November 2017 12:05:14"),
                            LocationGroup = locations[0],
                            LocationGroupId = locations[0].Id
                        },
                       new Location{
                            LocationId = Guid.NewGuid(), LocationName="Vismijn",
                            Position = 3, Latitude = 51.133607,  Longitude = 2.748845,
                            TimeLocation = Convert.ToDateTime(" 14 November 2017 17:30:00 "),
                            LocationGroup = locations[0],
                            LocationGroupId = locations[0].Id
                        },
                        new Location{
                            LocationId = Guid.NewGuid(), LocationName="IVO/HOWEST",
                            Position = 1, Latitude = 51.192507, Longitude = 3.214015,
                            TimeLocation = Convert.ToDateTime(" 18 November 2017 18:03:40 "),
                            LocationGroup = locations[1],
                            LocationGroupId = locations[1].Id
                        },
                        new Location{
                            LocationId = Guid.NewGuid(), LocationName="Marktplein",
                            Position = 2, Latitude = 51.208988, Longitude = 3.224328,
                            TimeLocation = Convert.ToDateTime(" 28 November 2017 08:12:14"),
                            LocationGroup = locations[1],
                            LocationGroupId = locations[1].Id
                        },
                       new Location{
                            LocationId = Guid.NewGuid(), LocationName="Kinepolis",
                            Position = 3, Latitude = 51.179972,  Longitude = 3.202001,
                            TimeLocation = Convert.ToDateTime(" 12 December 2017 14:30:58 "),
                            LocationGroup = locations[1],
                            LocationGroupId = locations[1].Id
                        },
                        new Location{
                            LocationId = Guid.NewGuid(), LocationName="Grote Markt",
                            Position = 1, Latitude = 50.847052, Longitude = 4.352418,
                            TimeLocation = Convert.ToDateTime(" 06 December 2017 11:30:40 "),
                            LocationGroup = locations[2],
                            LocationGroupId = locations[2].Id
                        },
                        new Location{
                            LocationId = Guid.NewGuid(), LocationName="Luchthaven",
                            Position = 2, Latitude = 50.898498, Longitude = 4.483535,
                            TimeLocation = Convert.ToDateTime(" 16 November 2017 12:05:14"),
                            LocationGroup = locations[2],
                            LocationGroupId = locations[2].Id
                        },
                        new Location{
                            LocationId = Guid.NewGuid(), LocationName="Atomium",
                            Position = 3, Latitude = 50.895170,  Longitude = 4.341302,
                            TimeLocation = Convert.ToDateTime(" 26 November 2017 17:30:00 "),
                            LocationGroup = locations[2],
                            LocationGroupId = locations[2].Id
                        }
                    };
                connection.InsertOrReplaceAll(locationItems);

            }
        }
    }
}





           
      

