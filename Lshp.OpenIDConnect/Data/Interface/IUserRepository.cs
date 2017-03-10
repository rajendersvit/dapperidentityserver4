using Lshp.OpenIDConnect.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IUserRepository
    {
        Task UpdateAsync(User user);
        Task<User> SelectByIdAsync(long userId);
        Task<User> SelectByUserNameAsync(string userName);
        Task DeleteAsync(User user);
        Task CreateAsync(User user);
        Task<User> SelectByEmailAsync(string email);
        Task<List<User>> SelectUsers();
    }
}