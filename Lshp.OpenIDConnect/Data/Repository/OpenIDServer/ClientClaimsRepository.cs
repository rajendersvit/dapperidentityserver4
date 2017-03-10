using Lshp.OpenIDConnect.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lshp.OpenIDConnect.Model.Entities;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Microsoft.Extensions.Options;
using Dapper;

namespace Lshp.OpenIDConnect.Data.Repository
{
    public class ClientClaimsRepository : BaseRepository, IClientClaimsRepository
    {
        public ClientClaimsRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task<IEnumerable<ClientClaim>> FindClientClaimByClientIdAsync(int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QueryAsync<ClientClaim>("select Id,ClientId,Type,Value from ClientClaims where clientId=@clientId", new { @clientid = clientId });
            }
        }

        public  async Task<ClientClaim> FindClientClaimByIdAsync(int id, int clientId)
        {
            using (var db = GetConnection())
            {
              return  await db.QuerySingleAsync<ClientClaim>("select Id,ClientId,Type,Value from ClientClaims where clientId=@clientId and id=@id", new { @clientid = clientId,@id=id  }); 
            }
        }

        public async Task RemoveClientClaim(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("DELETE from ClientClaims where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task StoreClientClaim(ClientClaim clientClaim)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("Insert into (ClientId,[Type],Value) values(@ClientId,@Type,@Value) ", new { @ClientId = clientClaim.ClientId, @Type=clientClaim.Type, @Value =clientClaim.Value});
            }
        }
    }
}
