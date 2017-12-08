using B4.PE3.DellobelI.Domain.Models;
using System;
using System.Threading.Tasks;

namespace B4.PE3.DellobelI.Domain.Services.Abstract
{
    public interface IUsersService 
    {
        Task<User> GetUserById(Guid id);
        Task SaveUser(User user);
    }
}
