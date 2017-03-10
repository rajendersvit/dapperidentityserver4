using Dapper;
using Lshp.OpenIDConnect.Data.Interface;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using Lshp.OpenIDConnect.Model.Entities;
using System.Collections.Generic;

namespace Lshp.OpenIDConnect.Data.Repository
{
    public class ClientCorsOriginsRepository : BaseRepository, IClientCorsOriginsRepository
    {
        public ClientCorsOriginsRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async  Task<IEnumerable<ClientCorsOrigin>> FindClientCorsOriginsByClientIdAsync(int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QueryAsync<ClientCorsOrigin>("select id,ClientId,Origin from ClientCorsOrigins where clientId=@clientId", new { @clientid = clientId });
            }
        }

        public async Task<ClientCorsOrigin> FindClientCorsOriginsByIdAsync(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QuerySingleAsync<ClientCorsOrigin>("select id,ClientId,Origin from ClientCorsOrigins where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            using (var con = GetConnection())
            {
                var urls = await con.QueryAsync<string>("SELECT Origin FROM ClientCorsOrigins");
                var origins = urls.ToArray().Where(x => x != null).Distinct();
                var result = origins.Contains(origin, StringComparer.OrdinalIgnoreCase);
                return result;
            }
        }

        public async Task RemoveClientCorsOriginsByIdAsync(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("delete from ClientCorsOrigins where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task StoreClientCorsOrigin(ClientCorsOrigin clientCorsOrigin)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("insert into ClientCorsOrigins (ClientId,Origin) values(@clientId,@Origin)", new { @clientid = clientCorsOrigin.ClientId, @Origin = clientCorsOrigin.Origin });
            }
        }
    }
}