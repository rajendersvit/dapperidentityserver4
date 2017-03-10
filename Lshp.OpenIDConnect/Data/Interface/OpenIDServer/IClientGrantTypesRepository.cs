using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IClientGrantTypesRepository
    {
        Task<IEnumerable<ClientGrantType>> FindClientGrantTypeByClientIdAsync(int clientId);
        Task<ClientGrantType> FindClientGrantTypeByIdAsync(int id, int  clientId);
        Task StoreClientGrantType(ClientGrantType clientGrantType); 
        Task RemoveClientGrantType(int id, int  clientId);
    }
}
