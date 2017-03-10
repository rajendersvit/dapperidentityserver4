using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IClientPostLogoutRedirectUrisRepository
    {
        Task<ClientPostLogoutRedirectUri> FindClientPostLogoutRedirectUriByIdAsync(int id, int clientId);
        Task<IEnumerable<ClientPostLogoutRedirectUri>> FindClientPostLogoutRedirectUriByClientIdAsync(int clientId);
        Task RemoveClientPostLogoutRedirectUri(int id, int clientId);
        Task StoreClientPostLogoutRedirectUri(ClientPostLogoutRedirectUri clientPostLogoutRedirectUri);
    }
}
