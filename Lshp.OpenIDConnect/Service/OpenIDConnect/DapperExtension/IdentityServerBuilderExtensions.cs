using Lshp.OpenIDConnect.Data.Interface;
using Lshp.OpenIDConnect.Data.Repository;
using Lshp.OpenIDConnect.Util.ConfigManager;
using IdentityModel;
using IdentityServer4.Configuration;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddDapperIdentity<TUser>(this IIdentityServerBuilder builder) where TUser : class
        {
            return builder.AddDapperIdentity<TUser>("Identity.Application");
        }

        public static IIdentityServerBuilder AddDapperIdentity<TUser>(this IIdentityServerBuilder builder, string authenticationScheme)
            where TUser : class
        {
            builder.Services.Configure<IdentityServerOptions>(options =>
            {
                options.Authentication.AuthenticationScheme = authenticationScheme;
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Cookies.ApplicationCookie.AuthenticationScheme = authenticationScheme;
                options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = System.TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";
                options.Cookies.ApplicationCookie.CookieName = "_da";
                // User settings
                options.User.RequireUniqueEmail = true;
                

                if (options.OnSecurityStampRefreshingPrincipal == null)
                {
                    options.OnSecurityStampRefreshingPrincipal = SecurityStampValidatorCallback.UpdatePrincipal;
                }
            });

            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator<TUser>>();
            builder.AddProfileService<ProfileService<TUser>>();
            builder.Services.AddTransient<ISecurityStampValidator, SecurityStampValidator<TUser>>();

            return builder;
        }

        public static IIdentityServerBuilder AddConfigurationStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IApiResourcesRepository>(provider => new ApiResourcesRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IClientsRepository>(provider => new OpenIDClientRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IPersistedGrantsRepository>(provider => new PersistedGrantsRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IClientCorsOriginsRepository>(provider => new ClientCorsOriginsRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IClientClaimsRepository>(provider => new ClientClaimsRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IClientGrantTypesRepository>(provider => new ClientGrantTypesRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IClientPostLogoutRedirectUrisRepository>(provider => new ClientPostLogoutRedirectUrisRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IClientRedirectUrisRepository>(provider => new ClientRedirectUrisRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IClientSecretsRepository>(provider => new ClientSecretsRepository(provider.GetService<IOptions<ConfigEntry>>()));
            builder.Services.AddSingleton<IClientScopesRepository>(provider => new ClientScopesRepository(provider.GetService<IOptions<ConfigEntry>>()));
            //builder.Services.AddSingleton<IClientIdPRestrictionsRepository>(provider => new ClientIdPRestrictionsRepository(provider.GetService<IOptions<ConfigEntry>>()));

            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();
            builder.Services.AddTransient<ICorsPolicyService, CorsPolicyService>();
            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();  

            return builder;
        }
        public static IIdentityServerBuilder  AddSigningCredentialStore(this IIdentityServerBuilder builder, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var config = Util.ConfigurationManager.GetInstance().GetConfig().X502Certificates;
 
            if (config != null && !string.IsNullOrWhiteSpace(config.Name))
            {
                var cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(System.IO.Path.Combine($"{env.ContentRootPath}",$"{config.Path}/{config.Name}"), config.Password);
                builder.AddSigningCredential(cert);
            }
            else
            {
                builder.AddTemporarySigningCredential();
            }
            return builder;
        }
        public static IIdentityServerBuilder AddConfigurationStoreCache(
            this IIdentityServerBuilder builder)
        {
            builder.AddInMemoryCaching();

            // these need to be registered as concrete classes in DI for
            // the caching decorators to work
            builder.Services.AddTransient<ClientStore>();
            builder.Services.AddTransient<ResourceStore>(); 
            // add the caching decorators
            builder.AddClientStoreCache<ClientStore>();
            builder.AddResourceStoreCache<ResourceStore>();
            

            return builder;
        }
    }
}