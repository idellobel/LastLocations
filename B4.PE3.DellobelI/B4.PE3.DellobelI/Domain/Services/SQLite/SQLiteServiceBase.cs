using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using SQLite.Net;
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

            //verwijdert bestaande tebellen
            connection.DropTable<User>();
            connection.DropTable<AppSettings>();
            connection.DropTable<LocationGroup>();
            connection.DropTable<Location>();

            //maakt tabellen aan (indien ze niet bestaan)
            connection.CreateTable<User>();
            connection.CreateTable<AppSettings>();
            connection.CreateTable<LocationGroup>();
            connection.CreateTable<Location>();

           
        }
    }
}
