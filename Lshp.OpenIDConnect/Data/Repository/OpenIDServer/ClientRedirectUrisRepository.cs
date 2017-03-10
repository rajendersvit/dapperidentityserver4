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
    public class ClientRedirectUrisRepository : BaseRepository, IClientRedirectUrisRepository
    {
        public ClientRedirectUrisRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task<IEnumerable<ClientRedirectUri>> FindClientRedirectUriByClientIdAsync(int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QueryAsync<ClientRedirectUri>("select Id,ClientId,RedirectUri from ClientRedirectUris where clientId=@clientId", new { @clientid = clientId });
            }
        }

        public async Task<ClientRedirectUri> FindClientRedirectUriByIdAsync(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QuerySingleAsync<ClientRedirectUri>("select Id,ClientId,RedirectUri from ClientRedirectUris where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task RemoveClientRedirectUri(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("delete from ClientRedirectUris where clientId=@clientId and id=@id", new { @clientid = clientId,@id=id });
            }
        }

        public async Task StoreClientRedirectUri(ClientRedirectUri clientRedirectUri)
        {
            using (var db = GetConnection())
            {
                 await db.ExecuteAsync("insert into ClientRedirectUris(ClientId,RedirectUri) VALUES(@clientId,@RedirectUri)", new { @clientid = clientRedirectUri.ClientId, @RedirectUri = clientRedirectUri.RedirectUri });
            }
        }
    }
}
