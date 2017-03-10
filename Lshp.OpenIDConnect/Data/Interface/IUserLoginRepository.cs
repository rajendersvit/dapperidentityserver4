using Lshp.OpenIDConnect.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IUserLoginRepository
    {
        Task InsertAsync(UserLogin userLogin);
        Task<User> UserByLoginInfoAsync(UserLogin userLogin);
        Task<IList<UserLogin>> LoginInfoByUserId(User user);
        Task DeleteAsync(UserLogin userLogin);
    }
}
