using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public static class IdentityServerIdentityBuilderExtensions
    {
        public static IdentityBuilder AddIdentityServerUserClaimsPrincipalFactory(this IdentityBuilder builder)
        {
            var interfaceType = typeof(IUserClaimsPrincipalFactory<>);
            interfaceType = interfaceType.MakeGenericType(builder.UserType);

            var classType = typeof(UserClaimsFactory<,>);
            classType = classType.MakeGenericType(builder.UserType, builder.RoleType);

            builder.Services.AddScoped(interfaceType, classType);

            return builder;
        }
    }
}
