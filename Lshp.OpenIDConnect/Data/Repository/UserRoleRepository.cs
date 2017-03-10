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
    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {
        public UserRoleRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task DeleteAsync(UserRole userRole)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"DELETE FROM [dbo].[UserRoles] WHERE [UserId] = @UserId AND [RoleId] = @RoleId", userRole);
            }
        }

        public async Task InsertAsync(UserRole userRole)
        {
            using (IDbConnection db = base.GetConnection())
            {
                await db.ExecuteAsync(@"INSERT INTO [dbo].[UserRoles]([UserId],[RoleId]) VALUES(@UserId,@RoleId)", userRole);
            }
        }
        
        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var result = await db.QueryAsync<UserRole>(@"SELECT [Name]
                        FROM [dbo].[UserRoles]
                        WHERE [Id] IN (SELECT [RoleId]
				                        FROM [dbo].[UserRoles]
				                        WHERE [UserId] = @Id)
                        AND UPPER([Name]) = @roleName",
                            new { user.Id, roleName = roleName.ToUpper() });
                return result.Any();
            }
        }
        public async Task<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            using (IDbConnection db = base.GetConnection())
            {
                var result = await db.QueryAsync<User>(@"SELECT b.*
                        FROM [dbo].[UserRoles] a
                        LEFT JOIN [dbo].[User] ON a.UserId = b.Id
                        WHERE a.Name = @roleName",
                            new { roleName = roleName.ToUpper() });
                return result.AsList();
            }
        }
    }
}
