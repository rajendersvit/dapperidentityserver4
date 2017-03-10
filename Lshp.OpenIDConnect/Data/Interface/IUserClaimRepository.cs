using Lshp.OpenIDConnect.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IUserClaimRepository
    {
        Task InsertAsync(UserClaim userClaim);
        Task<IList<UserClaim>> SelectClaimsByUserId(long userId);
        Task DeleteAsync(UserClaim userClaim);
        Task<IList<User>> SelectUsersByUserClaim(UserClaim claim);
    }
}
