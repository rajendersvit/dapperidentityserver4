using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IClientSecretsRepository
    { 
        Task<IEnumerable<ClientSecret>> FindClientSecretByClientIdAsync(int clientId);
        Task<ClientSecret> FindClientSecretByIdAsync(int id, int clientId);
        Task RemoveClientSecret(int id, int clientId);
        Task StoreClientSecret(ClientSecret clientSecret);
    }
}
