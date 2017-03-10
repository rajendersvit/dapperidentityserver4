using Lshp.OpenIDConnect.Model.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.IdentityService
{
    public class IdentityUserPrincipalFactory : IUserClaimsPrincipalFactory<User>
    {
        public Task<ClaimsPrincipal> CreateAsync(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity("Identity.Application");
            //identity.AddClaim(new Claim( ClaimTypes.NameIdentifier, user.Id.ToString(),ClaimValueTypes.Integer64));
            identity.AddClaim(new Claim(JwtClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(JwtClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(JwtClaimTypes.Subject, user.Id.ToString()));
            identity.AddClaim(new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed.ToString(),ClaimValueTypes.Boolean));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            return Task.FromResult(principal);
        }
    }
}
