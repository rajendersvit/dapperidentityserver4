using Dapper;
using Lshp.OpenIDConnect.Data.Interface;
using Lshp.OpenIDConnect.Model.Entities;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Repository
{
    public class ApiResourcesRepository : BaseRepository, IApiResourcesRepository
    {
        public ApiResourcesRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            using (var con = GetConnection())
            {
                var apiResource = await con.QueryFirstOrDefaultAsync<ApiResource>("SELECT [Id], [Description], [DisplayName], [Enabled], [Name] FROM [ApiResources] WHERE [Name] = @name;", new { Name = name });
                if (apiResource == null) return null;
                var sql = @"
                        SELECT [Id], [ApiResourceId], [Description], [DisplayName], [Emphasize], [Name], [Required], [ShowInDiscoveryDocument] FROM [ApiScopes] WHERE [ApiResourceId] = @Id
	                   SELECT  [Type] FROM [ApiScopeClaims] AS Apisc Inner JOIN [ApiScopes] as Apis on Apisc.ApiScopeId=Apis.Id  WHERE Apis.[ApiResourceId] = @Id
                        SELECT [Id], [ApiResourceId], [Description], [Expiration], [Type], [Value] FROM [ApiSecrets] WHERE [ApiResourceId] = @Id
                        ";
                using (var multi = await con.QueryMultipleAsync(sql, new { Id = apiResource.Id }))
                {
                    apiResource.ApiScopes = multi.Read<ApiScope>().ToList();
                    apiResource.ApiScopeClaim = multi.Read<ApiScopeClaim>().ToList();
                    apiResource.ApiSecrets = multi.Read<ApiSecret>().ToList();
                }
                return apiResource;
            }
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            using (var con = GetConnection())
            {
                var apiResourceIds = await con.QueryAsync<int>(@"SELECT DISTINCT [ARS].[Id] FROM [APIScopes] As [APIS]
                            INNER JOIN [ApiResources] AS [ARS] ON [APIS].[ApiResourceId] = [ARS].[Id]
                            WHERE [APIS].[Name] IN @Ids", new { Ids = scopeNames });

                var sql = @"SELECT [Id], [Description], [DisplayName], [Enabled], [Name] FROM [ApiResources] WHERE ID in @Id;
                        SELECT [Id], [ApiResourceId], [Description], [DisplayName], [Emphasize], [Name], [Required], [ShowInDiscoveryDocument] FROM [ApiScopes] WHERE [ApiResourceId] in  @Id;
	                    SELECT  [Apisc].[Id],[Type] FROM [ApiScopeClaims] AS Apisc Inner JOIN [ApiScopes] as Apis on Apisc.ApiScopeId=Apis.Id  WHERE Apis.[ApiResourceId] in @id;
                        SELECT [Id], [ApiResourceId], [Description], [Expiration], [Type], [Value] FROM [ApiSecrets] WHERE [ApiResourceId] in @id;
                        ";
                List<ApiResource> apiResources = new List<ApiResource>();
                using (var multi = await con.QueryMultipleAsync(sql, new { Id = apiResourceIds }))
                {
                    var apiResource = multi.Read<ApiResource>();
                    var ApiScopes = multi.Read<ApiScope>();
                    var ApiScopesClaims = multi.Read<ApiScopeClaim>();
                    var ApiSecret = multi.Read<ApiSecret>();

                    foreach (var item in apiResource)
                    {
                        item.ApiScopeClaim = ApiScopesClaims.Where(i => item.ApiScopes.Select(x => x.Id).Contains(i.ApiScopeId)).ToList();
                        item.ApiSecrets = (from r in ApiSecret.Where(i => i.ApiResourceId == item.Id) select r).ToList();
                        item.ApiScopes = (from r in ApiScopes.Where(i => i.ApiResourceId == item.Id) select r).ToList();
                        item.ApiScopeClaim = item.ApiScopeClaim;
                        apiResources.Add(item);
                    }
                };
                return apiResources;
            }
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            List<IdentityResource> identityResource = new List<IdentityResource>();
            List<IdentityClaim> claims = new List<IdentityClaim>();
            using (var con = GetConnection())
            {
                using (var multi = await con.QueryMultipleAsync(@"select * from IdentityResources where Name in @names ;
                    select IdentityResourceId,Type from IdentityClaims  as IC
                    join IdentityResources as IR on IC.IdentityResourceId=IR.Id where IR.Name in @names ", new { names = scopeNames }))
                {
                    identityResource = multi.Read<IdentityResource>().ToList();
                    claims = multi.Read<IdentityClaim>().ToList();
                }
            }
            foreach (var item in identityResource)
            {
                item.IdentityClaims = (from r in claims.Where(i => i.IdentityResourceId == item.Id) select r).ToList();
                //identityResource.Add(item);
            }
            return identityResource;
        }

        public async Task<IEnumerable<ApiResource>> GetAllApiResources()
        {
            IEnumerable<string> apiScopeName = new List<string>();
            using (var con = GetConnection())
            {
                apiScopeName = await con.QueryAsync<string>(@"SELECT DISTINCT [APIS].Name FROM [APIScopes] As [APIS]", new { });
            }
            return await FindApiResourcesByScopeAsync(apiScopeName);
        }

        public async Task<IEnumerable<IdentityResource>> GetAllIdentityResource()
        {
            IEnumerable<string> identityScopeName = new List<string>();
            using (var con = GetConnection())
            {
                identityScopeName = await con.QueryAsync<string>(@"SELECT DISTINCT Name FROM [IdentityResources]", new { });
            }
            return await FindIdentityResourcesByScopeAsync(identityScopeName);
        }
    }
}