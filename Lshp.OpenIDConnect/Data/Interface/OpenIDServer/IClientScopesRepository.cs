using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IClientScopesRepository
    {
        Task<IEnumerable<ClientScope>> FindClientScopeByClientIdAsync(int clientId);
        Task<ClientScope> FindClientScopeByIdAsync(int id,int clientId);
        Task StoreClientScope(ClientScope clientscope);
        Task RemoveClientScope(int id, int clientId);
    }
}
