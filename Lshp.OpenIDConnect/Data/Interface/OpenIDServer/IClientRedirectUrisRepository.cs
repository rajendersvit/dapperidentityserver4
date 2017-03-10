using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IClientRedirectUrisRepository
    {
        Task<IEnumerable<ClientRedirectUri>> FindClientRedirectUriByClientIdAsync(int clientId);
        Task<ClientRedirectUri> FindClientRedirectUriByIdAsync(int id, int clientId);
        Task RemoveClientRedirectUri(int id, int clientId); 
        Task StoreClientRedirectUri(ClientRedirectUri clientRedirectUri);
    }
}
