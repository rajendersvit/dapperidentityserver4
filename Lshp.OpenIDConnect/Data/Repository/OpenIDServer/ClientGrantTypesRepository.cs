using Lshp.OpenIDConnect.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Microsoft.Extensions.Options;
using Lshp.OpenIDConnect.Model.Entities;
using Dapper;

namespace Lshp.OpenIDConnect.Data.Repository
{
    public class ClientGrantTypesRepository : BaseRepository, IClientGrantTypesRepository
    {
        public ClientGrantTypesRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task<IEnumerable<ClientGrantType>> FindClientGrantTypeByClientIdAsync(int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QueryAsync<ClientGrantType>("select Id,ClientId,GrantType from ClientGrantTypes where clientId=@clientId", new { @clientid = clientId });
            }
        }

        public async Task<ClientGrantType> FindClientGrantTypeByIdAsync(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QuerySingleAsync<ClientGrantType>("select Id,ClientId,GrantType from ClientGrantTypes where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task RemoveClientGrantType(int Id, int clientId)
        {
            using (var db = GetConnection())
            {
                 await db.ExecuteAsync("DELETE FROM ClientGrantTypes where clientId=@clientId and id=@id", new { @clientid = clientId, @id = Id });
            }
        }

        public async Task StoreClientGrantType(ClientGrantType clientGrantType)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("INSERT INTO ClientGrantTypes(ClientId,GrantType) VALUES(@clientId,@GrantType) ", new { @clientid = clientGrantType.ClientId, @GrantType = clientGrantType.GrantType });
            }
        }
    }
}
