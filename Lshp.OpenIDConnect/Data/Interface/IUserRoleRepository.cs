using Lshp.OpenIDConnect.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IUserRoleRepository
    {
        Task InsertAsync(UserRole userRole);
        Task DeleteAsync(UserRole userRole);
        Task<bool> IsInRoleAsync(User user, string roleName);
        Task<IList<User>> GetUsersInRoleAsync(string roleName);
    }
}
