using B4.PE3.DellobelI.Domain.Models;
using B4.PE3.DellobelI.Domain.Services.Abstract;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.SQLite
{
    class UsersSQLiteService : SQLiteServiceBase, IUsersService
    {
        private async Task EnsureDefaultUser()
        {
            await Task.Run(async () =>
            {
                try
                {
                    //voor alle zekerheid steeds i gebruiker met Guid.Empty (= lokale gebruiker) 
                    await SaveUser(
                        new User
                        {
                            Id = Guid.Empty,
                            UserName = "Default User",
                            Email = "",
                        });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    await EnsureDefaultUser();
                    return connection.Find<User>(id);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw;
                }
            });
        }

        public async Task SaveUser(User user)
        {
            await Task.Run(() =>
            {
                try
                {
                    connection.InsertOrReplace(user);
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

