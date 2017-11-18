using B4.PE3.DellobelI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Mock
{
    public class UsersInMemoryService
    {
        private static List<User> users = new List<User>
        {
            new User{
                Id = Guid.Empty,
                UserName = "Ivan",
                Email="ivan.dellobel@gmail.com",
            }
        };

        public async Task<User> GetUserById(Guid id)
        {
            await Task.Delay(Constants.Mocking.FakeDelay);
            return users.FirstOrDefault(u => u.Id == id);
        }

        public async Task SaveUser(User user)
        {
            var oldUser = await GetUserById(user.Id);
            oldUser = user;
        }
    }
}
