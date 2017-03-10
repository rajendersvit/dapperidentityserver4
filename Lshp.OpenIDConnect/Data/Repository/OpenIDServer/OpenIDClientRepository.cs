using Dapper;
using Lshp.OpenIDConnect.Data.Interface;
using Lshp.OpenIDConnect.Model.Entities;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Repository
{
    public class OpenIDClientRepository : BaseRepository, IClientsRepository
    {
        public OpenIDClientRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            using (IDbConnection db = GetConnection())
            {
                var client = await db.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Clients WHERE ClientId=@Id and Enabled=1", new { Id = clientId });
                if(client== null) { return null; }
                var sql = @"
                SELECT Origin FROM ClientCorsOrigins WHERE ClientId = @id
                SELECT GrantType FROM ClientGrantTypes WHERE ClientId = @id
                SELECT Scope FROM ClientScopes WHERE ClientId = @id
                SELECT Type, Value FROM ClientClaims WHERE ClientId = @id
                SELECT Value, Type, Description, Expiration FROM ClientSecrets WHERE ClientId = @id
                SELECT Provider FROM ClientIdPRestrictions WHERE ClientId = @id
                SELECT PostLogoutRedirectUri FROM ClientPostLogoutRedirectUris WHERE ClientId = @id
                SELECT RedirectUri FROM ClientRedirectUris WHERE ClientId = @id";
                using (var multi = await db.QueryMultipleAsync(sql, new { Id = client.Id }))
                {
                    //var client = multi.Read<Client>().Single();
                    client.AllowedCorsOrigins = multi.Read<ClientCorsOrigin>().ToList();
                    client.AllowedGrantTypes = multi.Read<ClientGrantType>().ToList();
                    client.AllowedScopes = multi.Read<ClientScope>().ToList();
                    client.Claims = multi.Read<ClientClaim>().ToList();
                    client.ClientSecrets = multi.Read<ClientSecret>().ToList();
                    client.IdentityProviderRestrictions = multi.Read<ClientIdPRestriction>().ToList();
                    client.PostLogoutRedirectUris = multi.Read<ClientPostLogoutRedirectUri>().ToList();
                    client.RedirectUris = multi.Read<ClientRedirectUri>().ToList();
                }
                return client;
            }
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            using (IDbConnection db = GetConnection())
            {
                return await db.QueryAsync<Client>("SELECT * FROM Clients", new { });
            }
        }

        public async Task<int> GetIdByClientID(string clientId)
        {
            using (IDbConnection db = GetConnection())
            {
                return await db.QuerySingleAsync<int>("SELECT id FROM Clients where clientId=@clientId", new { @clientId=clientId });
            }
        }

        public async Task RemoveClient(int id,int clientId)
        {
            using (IDbConnection db = GetConnection())
            {
                await db.ExecuteAsync("update Clients set Enabled=0 where id=@id and clientId=@clientId", new { @clientId = clientId, @id = id });
            }
        }
         
        public Task<IEnumerable<Client>> SearchClients(int pageId, string clientName, string clientCode)
        {
            throw new NotImplementedException();
        }

        public async Task StoreClient(Client client)
        {
            using (IDbConnection db = GetConnection())
            {
                if (client.Id == 0)
                {
                    await db.ExecuteAsync(@"INSERT INTO Clients (AbsoluteRefreshTokenLifetime,AccessTokenLifetime,AccessTokenType,AllowAccessTokensViaBrowser,AllowOfflineAccess,AllowPlainTextPkce,AllowRememberConsent,AlwaysIncludeUserClaimsInIdToken,AlwaysSendClientClaims,AuthorizationCodeLifetime,ClientId,ClientName,ClientUri,EnableLocalLogin,Enabled,IdentityTokenLifetime,IncludeJwtId,LogoUri,LogoutSessionRequired,LogoutUri,PrefixClientClaims,ProtocolType,RefreshTokenExpiration,RefreshTokenUsage,RequireClientSecret,RequireConsent,RequirePkce,SlidingRefreshTokenLifetime,UpdateAccessTokenClaimsOnRefresh)
                                            VALUES(2592000, 3600, 0, 0, 0, 0, 1, 0, 0, 300, @clientId, @clientName, 'NULL', 1, 1, 300, 0, 'NULL', 1, 'NULL', 1, 'oidc', 1, 1, 1, 1, 0, 1296000, 0)", new { @clientId = client.ClientId, @clientName = client.ClientName });
                }
                else
                {
                    await db.ExecuteAsync(@"Update Clients SET AbsoluteRefreshTokenLifetime = @AbsoluteRefreshTokenLifetime,
                                            AccessTokenLifetime = @AccessTokenLifetime,
                                            AccessTokenType = @AccessTokenType,
                                            AllowAccessTokensViaBrowser = @AllowAccessTokensViaBrowser,
                                            AllowOfflineAccess = @AllowOfflineAccess,
                                            AllowPlainTextPkce = @AllowPlainTextPkce,
                                            AllowRememberConsent = @AllowRememberConsent,
                                            AlwaysIncludeUserClaimsInIdToken = @AlwaysIncludeUserClaimsInIdToken,
                                            AlwaysSendClientClaims = @AlwaysSendClientClaims,
                                            AuthorizationCodeLifetime = @AuthorizationCodeLifetime,                                             
                                            ClientName = @ClientName,
                                            ClientUri = @ClientUri,
                                            EnableLocalLogin = @EnableLocalLogin,
                                            Enabled = @Enabled,
                                            IdentityTokenLifetime = @IdentityTokenLifetime,
                                            IncludeJwtId = @IncludeJwtId,
                                            LogoUri = @LogoUri,
                                            LogoutSessionRequired = @LogoutSessionRequired,
                                            LogoutUri = @LogoutUri,
                                            PrefixClientClaims = @PrefixClientClaims,
                                            ProtocolType = @ProtocolType,
                                            RefreshTokenExpiration = @RefreshTokenExpiration,
                                            RefreshTokenUsage = @RefreshTokenUsage,
                                            RequireClientSecret = @RequireClientSecret,
                                            RequireConsent = @RequireConsent,
                                            RequirePkce = @RequirePkce,
                                            SlidingRefreshTokenLifetime = @SlidingRefreshTokenLifetime,
                                            UpdateAccessTokenClaimsOnRefresh = @UpdateAccessTokenClaimsOnRefresh
                                            WHERE id = @id and clientId=@clientId", new
                    {
                        id = client.Id,
                        clientid = client.ClientId,
                        client.AbsoluteRefreshTokenLifetime,
                        AccessTokenLifetime = client.AccessTokenLifetime,
                        AccessTokenType = client.AccessTokenType,
                        AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser,
                        AllowOfflineAccess = client.AllowOfflineAccess,
                        AllowPlainTextPkce = client.AllowPlainTextPkce,
                        AllowRememberConsent = client.AllowRememberConsent,
                        AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken,
                        AlwaysSendClientClaims = client.AlwaysSendClientClaims,
                        AuthorizationCodeLifetime = client.AuthorizationCodeLifetime,
                        ClientName = client.ClientName,
                        ClientUri = client.ClientUri,
                        EnableLocalLogin = client.EnableLocalLogin,
                        Enabled = client.Enabled,
                        IdentityTokenLifetime = client.IdentityTokenLifetime,
                        IncludeJwtId = client.IncludeJwtId,
                        LogoUri = client.LogoUri,
                        LogoutSessionRequired = client.LogoutSessionRequired,
                        LogoutUri = client.LogoutUri,
                        PrefixClientClaims = client.PrefixClientClaims,
                        ProtocolType = client.ProtocolType,
                        RefreshTokenExpiration = client.RefreshTokenExpiration,
                        RefreshTokenUsage = client.RefreshTokenUsage,
                        RequireClientSecret = client.RequireClientSecret,
                        RequireConsent = client.RequireConsent,
                        RequirePkce = client.RequirePkce,
                        SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime,
                        UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh
                    });

                }
            }
        }

    }
}