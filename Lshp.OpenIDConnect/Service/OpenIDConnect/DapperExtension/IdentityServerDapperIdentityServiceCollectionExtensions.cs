using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public static class IdentityServerDapperIdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityServerUserClaimsPrincipalFactory<TUser, TRole>(this IServiceCollection services)
            where TUser : class
            where TRole : class
        {
            return services.AddTransient<IUserClaimsPrincipalFactory<TUser>, UserClaimsFactory<TUser, TRole>>();
        }
    }
}
