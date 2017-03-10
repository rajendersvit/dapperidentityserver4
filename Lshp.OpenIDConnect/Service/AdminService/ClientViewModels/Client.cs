using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lshp.OpenIDConnect.Service.AdminService.ClientViewModels
{
    public class Client
    { 
        public int Id { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AccessTokenType { get; set; }
        public Boolean AllowAccessTokensViaBrowser { get; set; }
        public Boolean AllowOfflineAccess { get; set; }
        public Boolean AllowPlainTextPkce { get; set; }
        public Boolean AllowRememberConsent { get; set; }
        public Boolean AlwaysIncludeUserClaimsInIdToken { get; set; }
        public Boolean AlwaysSendClientClaims { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public string ClientId { get; set; }
        [Required]
        [Display(Name ="Client Name")]
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public Boolean EnableLocalLogin { get; set; }
        public Boolean Enabled { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public Boolean IncludeJwtId { get; set; }
        public string LogoUri { get; set; }
        public Boolean LogoutSessionRequired { get; set; }
        public string LogoutUri { get; set; }
        public Boolean PrefixClientClaims { get; set; }
        public string ProtocolType { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int RefreshTokenUsage { get; set; }
        public Boolean RequireClientSecret { get; set; }
        public Boolean RequireConsent { get; set; }
        public Boolean RequirePkce { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public Boolean UpdateAccessTokenClaimsOnRefresh { get; set; } 
        public List<ClientSecret> ClientSecrets { get; set; }
        public List<ClientGrantType> AllowedGrantTypes { get; set; }
        public List<ClientRedirectUri> RedirectUris { get; set; }
        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }
        //public List<ClientScope> AllowedScopes { get; set; }
        //public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }
        public List<ClientClaim> Claims { get; set; }
        public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }

    }
}
