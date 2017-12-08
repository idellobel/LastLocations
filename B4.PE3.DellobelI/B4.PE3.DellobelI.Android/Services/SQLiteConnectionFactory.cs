using System;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using SQLite.Net;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(B4.PE3.DellobelI.Droid.Services.SQLiteConnectionFactory))]
namespace B4.PE3.DellobelI.Droid.Services
{
    internal class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        public SQLiteConnection CreateConnection(string databaseFileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Path.Combine(path, databaseFileName);

            return new SQLiteConnection(
                new SQLitePlatformAndroid(),
                path,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite,
                storeDateTimeAsTicks: false
                );
        }
    }
}