using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Abstract
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteConnection CreateConnection(string databaseFileName);
    }
}
