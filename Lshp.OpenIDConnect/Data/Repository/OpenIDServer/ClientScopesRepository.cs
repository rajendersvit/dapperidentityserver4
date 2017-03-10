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
    public class ClientScopesRepository : BaseRepository, IClientScopesRepository
    {
        public ClientScopesRepository(IOptions<ConfigEntry> option) : base(option)
        {

        }

        public async Task<IEnumerable<ClientScope>> FindClientScopeByClientIdAsync(int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QueryAsync<ClientScope>("select Id,ClientId,scope from ClientScopes where clientId=@clientId", new { @clientid = clientId });
            }
        }

        public async Task<ClientScope> FindClientScopeByIdAsync(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QuerySingleAsync<ClientScope>("select Id,ClientId,scope from ClientScopes where clientId=@clientId and id=@id", new { @clientid = clientId,@id=id });
            }
        }

        public async Task RemoveClientScope(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("delete from ClientScopes where clientId=@clientId and id=@id", new { @clientid = clientId ,@id=id});
            }
        }

        public async Task StoreClientScope(ClientScope clientscope)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("insert into ClientScopes(ClientId,scope) values(@clientId,@scope)", new { @clientid = clientscope.ClientId, @scope= clientscope.Scope});
            }
        }
    }
}
