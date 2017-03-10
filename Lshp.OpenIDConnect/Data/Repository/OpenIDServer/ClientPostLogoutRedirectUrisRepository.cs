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
    public class ClientPostLogoutRedirectUrisRepository : BaseRepository, IClientPostLogoutRedirectUrisRepository
    {
        public ClientPostLogoutRedirectUrisRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task<IEnumerable<ClientPostLogoutRedirectUri>> FindClientPostLogoutRedirectUriByClientIdAsync(int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QueryAsync<ClientPostLogoutRedirectUri>("select Id,ClientId,PostLogoutRedirectUri from ClientPostLogoutRedirectUris where clientId=@clientId", new { @clientid = clientId });
            }
        }

        public async Task<ClientPostLogoutRedirectUri> FindClientPostLogoutRedirectUriByIdAsync(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QuerySingleAsync<ClientPostLogoutRedirectUri>("select Id,ClientId,PostLogoutRedirectUri from ClientPostLogoutRedirectUris where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task RemoveClientPostLogoutRedirectUri(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                 await db.ExecuteAsync("DELETE FROM  ClientPostLogoutRedirectUris where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task StoreClientPostLogoutRedirectUri(ClientPostLogoutRedirectUri clientPostLogoutRedirectUri)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync(" INSERT INTO ClientPostLogoutRedirectUris(ClientId,PostLogoutRedirectUri) VALUES(@clientId,@PostLogoutRedirectUri)", new { @clientid = clientPostLogoutRedirectUri.ClientId, PostLogoutRedirectUri = clientPostLogoutRedirectUri.PostLogoutRedirectUri });
            } 
        }
    }
}
