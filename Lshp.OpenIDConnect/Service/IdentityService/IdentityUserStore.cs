using Lshp.OpenIDConnect.Data.Interface;
using Lshp.OpenIDConnect.Model.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.IdentityService
{
    public class IdentityUserStore : IUserLoginStore<User>,
                                    IUserClaimStore<User>,
                                    IUserRoleStore<User>,
                                    IUserPasswordStore<User>,
                                    IUserSecurityStampStore<User>,
                                    IUserEmailStore<User>,
                                    IUserPhoneNumberStore<User>,
                                    IUserTwoFactorStore<User>,
                                    IQueryableUserStore<User>,
                                    IUserLockoutStore<User>,
                                    IUserStore<User>
    {
        private readonly IUserLoginRepository userLoginRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserClaimRepository userClaimRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRoleRepository userRoleRepository;

        public IQueryable<User> Users
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IdentityUserStore(IUserLoginRepository userLoginRepository,
            IUserRepository userRepository,
            IUserClaimRepository userClaimRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository)
        {
            this.userLoginRepository = userLoginRepository;
            this.userRepository = userRepository;
            this.userClaimRepository = userClaimRepository;
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            await userRepository.CreateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var match = await userRepository.SelectByIdAsync(user.Id);
            if (match != null)
            {
                match.UserName = user.UserName;
                match.Email = user.Email;
                match.PhoneNumber = user.PhoneNumber;
                match.TwoFactorEnabled = user.TwoFactorEnabled;
                match.PasswordHash = user.PasswordHash;
                await userRepository.UpdateAsync(match);
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public virtual async Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await userRepository.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var match = userRepository.SelectByIdAsync(user.Id).Result;
            if (match != null)
            {
                await userRepository.DeleteAsync(match);
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var userIdLong = Convert.ToInt64(userId);
            var user = await userRepository.SelectByIdAsync(userIdLong);

            return user;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await userRepository.SelectByUserNameAsync(normalizedUserName);

            return user;
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(true);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.FromResult(true);
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            await UpdateAsync(user);
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public async Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            await UpdateAsync(user);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            await UpdateAsync(user);
        }

        public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            await UpdateAsync(user);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            IList<UserLoginInfo> logins = new List<UserLoginInfo>();
            var userLogins = await userLoginRepository.LoginInfoByUserId(user);
            foreach (var userLogin in userLogins)
            {
                var userLoginInfo = new UserLoginInfo(userLogin.LoginProvider, userLogin.ProviderKey, user.UserName);
                logins.Add(userLoginInfo);
            }
            return logins;
        }

        public async Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            var record = new UserLogin
            {
                LoginProvider = loginProvider,
                ProviderKey = providerKey
            };
            return await userLoginRepository.UserByLoginInfoAsync(record);
        }

        public async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (login == null)
                throw new ArgumentNullException(nameof(login));

            var record = new UserLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                UserId = user.Id
            };
            await userLoginRepository.InsertAsync(record);
        }

        public async Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var record = new UserLogin
            {
                LoginProvider = loginProvider,
                ProviderKey = providerKey,
                UserId = user.Id
            };
            await userLoginRepository.DeleteAsync(record);
        }

        public void Dispose()
        {
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return
                Task.FromResult(user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset?(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset?());
        }

        public async Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEndDateUtc = !lockoutEnd.HasValue ? (DateTime?)null : lockoutEnd.Value.Date;
            await UpdateAsync(user);
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount++;
            await UpdateAsync(user);
            return await Task.FromResult(user.AccessFailedCount);
        }

        public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount = 0;
            await UpdateAsync(user);
        }

        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEnabled = enabled;
            await UpdateAsync(user);
        }

        public async Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.SecurityStamp = stamp;

            await UpdateAsync(user);
        }

        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            IList<System.Security.Claims.Claim> result = new List<System.Security.Claims.Claim>();
            var records = await userClaimRepository.SelectClaimsByUserId(user.Id);
            foreach (var record in records)
            {
                var claim = new System.Security.Claims.Claim(record.ClaimType, record.ClaimValue);
                result.Add(claim);
            }
            return result;
        }

        public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                var userClaim = new UserClaim();
                userClaim.ClaimType = claim.Type;
                userClaim.ClaimValue = claim.Value;
                userClaim.UserId = user.Id;
                await userClaimRepository.InsertAsync(userClaim);
            }
        }

        public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            var userClaim = new UserClaim();
            userClaim.ClaimType = claim.Type;
            userClaim.ClaimValue = claim.Value;
            userClaim.UserId = user.Id;
            await userClaimRepository.DeleteAsync(userClaim);
            userClaim.ClaimType = newClaim.Type;
            userClaim.ClaimValue = newClaim.Value;
            await userClaimRepository.InsertAsync(userClaim);
        }

        public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                var userClaim = new UserClaim();
                userClaim.ClaimType = claim.Type;
                userClaim.ClaimValue = claim.Value;
                userClaim.UserId = user.Id;
                await userClaimRepository.DeleteAsync(userClaim);
            }
        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            var userClaim = new UserClaim();
            userClaim.ClaimType = claim.Type;
            userClaim.ClaimValue = claim.Value;
            var result = await userClaimRepository.SelectUsersByUserClaim(userClaim);
            return result;
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new Exception("Role must not null or empty");
            }
            var role = await roleRepository.FindByName(roleName);

            if (role == null)
            {
                throw new Exception("Role name not found");
            }

            var record = new UserRole();
            record.RoleId = role.Id;
            record.UserId = user.Id;
            await userRoleRepository.InsertAsync(record);
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new Exception("Role must not null or empty");
            }
            var role = await roleRepository.FindByName(roleName);
            if (role != null)
            {
                var record = new UserRole();
                record.RoleId = role.Id;
                record.UserId = user.Id;
                await userRoleRepository.DeleteAsync(record);
            }
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await roleRepository.SelectRolesByUserIdAsync(user.Id);
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new Exception("Role must not null or empty");
            }

            var role = roleRepository.FindByName(roleName);
            if (role != null)
            {
                return await userRoleRepository.IsInRoleAsync(user, roleName);
            }
            return false;
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return await userRoleRepository.GetUsersInRoleAsync(roleName);
        }

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            await UpdateAsync(user);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.EmailConfirmed = confirmed;
            await UpdateAsync(user);
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await userRepository.SelectByEmailAsync(normalizedEmail);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.Email = normalizedEmail;
            return Task.FromResult(true);
        }
    }
}