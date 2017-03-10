using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public class ProfileService<TUser> 
        : IProfileService         where TUser : class
    {
        private readonly IUserClaimsPrincipalFactory<TUser> _claimsFactory;
        private readonly UserManager<TUser> _userManager;

        public ProfileService(UserManager<TUser> userManager,
            IUserClaimsPrincipalFactory<TUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();

            //claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();


            //claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));
            //claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));

            context.AddFilteredClaims(principal.Claims.Union(claims)); 

        }

        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
