using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.Extensions.Options;
using Lshp.OpenIDConnect.Data.Interface;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Lshp.OpenIDConnect.Model.Entities;

namespace Lshp.OpenIDConnect.Data.Repository
{
    public class UserLoginRepository : BaseRepository, IUserLoginRepository
    {
        public UserLoginRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task DeleteAsync(UserLogin userLogin)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"DELETE FROM [dbo].[UserLogins] WHERE [LoginProvider] = @LoginProvider AND [ProviderKey] = @ProviderKey AND [UserId] = @UserId", userLogin);
            }
        }

        public async Task InsertAsync(UserLogin userLogin)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"INSERT INTO [dbo].[UserLogins]([LoginProvider],[ProviderKey],[UserId])
                VALUES(@LoginProvider,@ProviderKey,@UserId)", userLogin);
            }
        }

        public async Task<IList<UserLogin>> LoginInfoByUserId(User user)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var userLogin = await db.QueryAsync<UserLogin>("SELECT [LoginProvider], [ProviderKey] FROM [dbo].[UserLogins] WHERE [UserId] = @Id",
                               new { user.Id });
                return userLogin.ToList();
            }
        }

        public async Task<User> UserByLoginInfoAsync(UserLogin userLogin)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var user = await db.QueryAsync<User>(@"SELECT * 
                            FROM [dbo].[Users] 
                            WHERE [Id] = (SELECT [UserId] 
                                FROM [dbo].[UserLogins] 
                            WHERE [LoginProvider] = @LoginProvider 
                                AND [ProviderKey] = @ProviderKey)",
                                    new { userLogin });
                return user.FirstOrDefault();
            }
        }
    }
}
