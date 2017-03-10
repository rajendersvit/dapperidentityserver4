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
    public class PersistedGrantsRepository : BaseRepository, IPersistedGrantsRepository
    {
        public PersistedGrantsRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            using (var con = GetConnection())
            {
                return await con.QueryAsync<PersistedGrant>("SELECT [Key],[Type],[ClientId],[CreationTime],[Data],[Expiration],[SubjectId] FROM PersistedGrants WHERE SubjectId=@SubjectId",
                          new { SubjectId = subjectId });

            }
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            using (var con =GetConnection())
            {
                return await con.QueryFirstOrDefaultAsync<PersistedGrant>("SELECT [Key],[Type],[ClientId],[CreationTime],[Data],[Expiration],[SubjectId] FROM PersistedGrants WHERE [key]=@key",
                         new { key = key });
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            using (var con =GetConnection())
            {
                await con.QueryAsync("DELETE FROM PersistedGrants WHERE SubjectId=@subjectId and ClientId=@ClientId",
                        new { subjectId = subjectId, ClientId = clientId });
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {

            using (var con =GetConnection())
            {
                await con.QueryAsync("DELETE  FROM PersistedGrants WHERE SubjectId=@subjectId and ClientId=@ClientId and Type=type",
                        new { subjectId = subjectId, ClientId = clientId, type = type });
            }
        }

        public async Task RemoveAsync(string key)
        {
            using (var con =GetConnection())
            {
                await con.ExecuteAsync("DELETE FROM PersistedGrants WHERE [Key]=@key ",
                        new { key = key });
            }
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            using (var con =GetConnection())
            {
                await con.ExecuteAsync("INSERT INTO PersistedGrants ([Key], SubjectId, ClientId, CreationTime, Expiration, Type,Data) VALUES (@Key, @SubjectId, @ClientId, @CreationTime, @Expiration, @Type, @Data )", grant);
            }
        }
    }
}
