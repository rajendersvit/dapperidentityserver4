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
    public class ClientSecretsRepository : BaseRepository, IClientSecretsRepository
    {
        public ClientSecretsRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task<IEnumerable<ClientSecret>> FindClientSecretByClientIdAsync(int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QueryAsync<ClientSecret>("select Id,ClientId,Type,Value,Description,Expiration from ClientSecrets where clientId=@clientId", new { @clientid = clientId });
            }
        }

        public async Task<ClientSecret> FindClientSecretByIdAsync(int id, int clientId)
        {
            using (var db = GetConnection())
            {
                return await db.QuerySingleAsync<ClientSecret>("select Id,ClientId,Type,Value,Description,Expiration from ClientSecrets where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task RemoveClientSecret(int id, int clientId)
        { 
            using (var db = GetConnection())
            {
                 await db.ExecuteAsync("DELETE from ClientSecrets where clientId=@clientId and id=@id", new { @clientid = clientId, @id = id });
            }
        }

        public async Task StoreClientSecret(ClientSecret clientSecret)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("INSERT INTO  ClientSecrets (ClientId,Type,Value,Description,Expiration) VALUES (@ClientId,@Type,@Value,@Description,@Expiration) ", new { @ClientId = clientSecret.ClientId, @Type = clientSecret.Type, @Value = clientSecret.Value, @Description = clientSecret.Description, @Expiration = clientSecret.Expiration });
            }
        }
    }
}
