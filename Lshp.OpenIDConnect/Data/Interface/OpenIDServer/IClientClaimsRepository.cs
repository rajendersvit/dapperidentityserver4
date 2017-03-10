using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IClientClaimsRepository
    {
        Task<IEnumerable<ClientClaim>> FindClientClaimByClientIdAsync(int clientId);
        Task<ClientClaim> FindClientClaimByIdAsync(int id, int clientId);
        Task StoreClientClaim(ClientClaim clientClaim);
        Task RemoveClientClaim(int id, int clientId);
    }
}
