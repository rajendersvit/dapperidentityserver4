using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IApiResourcesRepository
    {
        Task<ApiResource> FindApiResourceAsync(string name);
        Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames);
        Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames);
        Task<IEnumerable<ApiResource>> GetAllApiResources();
        Task<IEnumerable<IdentityResource>> GetAllIdentityResource();
    }
}
