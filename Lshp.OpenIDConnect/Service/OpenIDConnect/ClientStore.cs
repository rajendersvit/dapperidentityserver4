using IdentityServer4.Stores;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Lshp.OpenIDConnect.Data.Interface;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public class ClientStore : IClientStore
    {
        IClientsRepository clientsRepository;
        public ClientStore(IClientsRepository clientsRepository)
        {
            this.clientsRepository = clientsRepository;
        }
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var clientData = await clientsRepository.FindClientByIdAsync(clientId);
            return await Task.FromResult(MapClient(clientData));
        }
        public Client MapClient(Model.Entities.Client model)
        {
            if (model == null) return null;
            Client client = new Client();
            client.AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime;
            client.AccessTokenLifetime = model.AccessTokenLifetime;
            client.AccessTokenType = GetEnumTypes.GetClientAccessTokenType(model.AccessTokenType);
            client.AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser;
            client.AllowedCorsOrigins = model.AllowedCorsOrigins.Select(i => i.Origin).ToList();
            client.AllowedGrantTypes = model.AllowedGrantTypes.Select(i => i.GrantType).ToList();
            client.AllowedScopes = model.AllowedScopes.Select(i => i.Scope).ToList();
            client.AllowOfflineAccess = model.AllowOfflineAccess;
            client.AllowPlainTextPkce = model.AllowPlainTextPkce;
            client.AllowRememberConsent = model.AllowRememberConsent;
            client.AlwaysIncludeUserClaimsInIdToken = model.AlwaysIncludeUserClaimsInIdToken;
            client.AlwaysSendClientClaims = model.AlwaysSendClientClaims;
            client.AuthorizationCodeLifetime = model.AuthorizationCodeLifetime;
            model.Claims.ForEach(i => client.Claims.Add(new System.Security.Claims.Claim(i.Type, i.Value)));
            client.ClientId = model.ClientId;
            client.ClientName = model.ClientName;
            client.ClientSecrets = (from r in model.ClientSecrets select new Secret() { Description = r.Description, Expiration = r.Expiration, Type = r.Type, Value = r.Value }).ToList();
            client.ClientUri = model.ClientUri;
            client.Enabled = model.Enabled;
            client.EnableLocalLogin = model.EnableLocalLogin;
            client.IdentityProviderRestrictions = model.IdentityProviderRestrictions.Select(i => i.Provider).ToList();
            client.IdentityTokenLifetime = model.IdentityTokenLifetime;
            client.IncludeJwtId = model.IncludeJwtId;
            client.LogoUri = model.LogoUri;
            client.LogoutSessionRequired = model.LogoutSessionRequired;
            client.LogoutUri = model.LogoutUri;
            client.PostLogoutRedirectUris = model.PostLogoutRedirectUris.Select(i => i.PostLogoutRedirectUri).ToList();
            client.PrefixClientClaims = model.PrefixClientClaims;
            client.ProtocolType = model.ProtocolType;
            client.RedirectUris = model.RedirectUris.Select(i => i.RedirectUri).ToList();
            client.RefreshTokenExpiration = GetEnumTypes.GetTokenExpiration(model.RefreshTokenExpiration);
            client.RefreshTokenUsage = GetEnumTypes.GetTokenUsage(model.RefreshTokenUsage);
            client.RequireClientSecret = model.RequireClientSecret;
            client.RequireConsent = model.RequireConsent;
            client.RequirePkce = model.RequirePkce;
            client.SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime;
            client.UpdateAccessTokenClaimsOnRefresh = model.UpdateAccessTokenClaimsOnRefresh;
            return client;
        }

    }
}
