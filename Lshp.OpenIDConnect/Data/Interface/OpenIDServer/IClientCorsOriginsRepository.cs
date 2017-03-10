using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IClientCorsOriginsRepository
    {
        Task<bool> IsOriginAllowedAsync(string origin);
        Task<IEnumerable<ClientCorsOrigin>> FindClientCorsOriginsByClientIdAsync(int clientId);
        Task<ClientCorsOrigin> FindClientCorsOriginsByIdAsync(int Id, int clientId);
        Task StoreClientCorsOrigin(ClientCorsOrigin clientCorsOrigin);
        Task RemoveClientCorsOriginsByIdAsync(int Id, int clientId);
    }

}
