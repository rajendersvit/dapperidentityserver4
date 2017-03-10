using Lshp.OpenIDConnect.Service.AdminService.ClientViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.AdminService
{
    public interface IManageOpenIDClient
    {
        Task<int> GetIdByClientID(string clientId);
        Task<IEnumerable<Client>> GetAllClients();

        Task<IEnumerable<Client>> SearchClients(int pageId, string clientName, string clientCode);

        Task<Client> FindClientByClientIdAsync(string clientId);

        Task StoreClient(Client client);

        Task<IEnumerable<ClientClaim>> FindClientClaimByClientIdAsync(int clientId);

        Task<IEnumerable<ClientSecret>> FindClientSecretByClientIdAsync(int clientId);

        Task<IEnumerable<ClientGrantType>> FindClientGrantTypeByClientIdAsync(int clientId);

        Task<IEnumerable<ClientRedirectUri>> FindClientRedirectUriByClientIdAsync(int clientId);

        Task<ClientScope> FindClientScopeByIdAsync(int id, int clientId);

        Task<IEnumerable<ClientScope>> FindClientScopeByClientIdAsync(int clientId);

        Task<IEnumerable<ClientPostLogoutRedirectUri>> FindClientPostLogoutRedirectUriByClientIdAsync(int clientId);

        Task<ClientClaim> FindClientClaimByIdAsync(int id, int clientId);

        Task<ClientSecret> FindClientSecretByIdAsync(int id, int clientId);

        Task<ClientGrantType> FindClientGrantTypeByIdAsync(int id, int clientId);

        Task StoreClientScope(ClientScope clientscope);

        Task<ClientRedirectUri> FindClientRedirectUriByIdAsync(int id, int clientId);

        Task<ClientPostLogoutRedirectUri> FindClientPostLogoutRedirectUriByIdAsync(int id, int clientId);

        Task<ClientCorsOrigin> FindClientCorsOriginByIdAsync(int clientId);

        Task StoreClientClaim(ClientClaim clientClaim);

        Task StoreClientSecret(ClientSecret clientSecret);

        Task StoreClientGrantType(ClientGrantType clientGrantType);

        Task StoreClientRedirectUri(ClientRedirectUri clientRedirectUri);

        Task StoreClientPostLogoutRedirectUri(ClientPostLogoutRedirectUri clientPostLogoutRedirectUri);

        Task StoreClientCorsOrigin(ClientCorsOrigin clientCorsOrigin);

        Task RemoveClient(int id, int clientId);

        Task RemoveClientClaim(int id, int clientId);

        Task RemoveClientSecret(int id, int clientId);

        Task RemoveClientGrantType(int id, int clientId);

        Task RemoveClientRedirectUri(int id, int clientId);

        Task RemoveClientPostLogoutRedirectUri(int id, int clientId);

        Task RemoveClientCorsOrigin(int id, int clientId);

        Task RemoveClientScope(int id, int clientId);
    }
}