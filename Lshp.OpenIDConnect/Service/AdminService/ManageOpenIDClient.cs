using Lshp.OpenIDConnect.Data.Interface;
using Lshp.OpenIDConnect.Service.AdminService.ClientViewModels;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.AdminService
{
    public class ManageOpenIDClient : IManageOpenIDClient
    {
        public IClientsRepository clientRepository;
        public IClientGrantTypesRepository clientGrantTypesRepository;
        public IClientScopesRepository clientScopesRepository;
        public IClientClaimsRepository clientClaimsRepository;
        public IClientCorsOriginsRepository clientCorsOriginsRepository;
        public IClientRedirectUrisRepository clientRedirectUrisRepository;
        public IClientSecretsRepository clientSecretsRepository;
        public IClientPostLogoutRedirectUrisRepository clientPostLogoutRedirectUrisRepository;

        public ManageOpenIDClient(IClientsRepository clientRepository, IClientGrantTypesRepository clientGrantTypesRepository, IClientScopesRepository clientScopesRepository
            , IClientClaimsRepository clientClaimsRepository, IClientCorsOriginsRepository clientCorsOriginsRepository, IClientRedirectUrisRepository clientRedirectUrisRepository
            , IClientSecretsRepository clientSecretsRepository, IClientPostLogoutRedirectUrisRepository clientPostLogoutRedirectUrisRepository)
        {
            this.clientRepository = clientRepository;
            this.clientGrantTypesRepository = clientGrantTypesRepository;
            this.clientScopesRepository = clientScopesRepository;
            this.clientClaimsRepository = clientClaimsRepository;
            this.clientCorsOriginsRepository = clientCorsOriginsRepository;
            this.clientRedirectUrisRepository = clientRedirectUrisRepository;
            this.clientSecretsRepository = clientSecretsRepository;
            this.clientPostLogoutRedirectUrisRepository = clientPostLogoutRedirectUrisRepository;
        }

        public async Task<Client> FindClientByClientIdAsync(string clientId)
        {
            var client = await clientRepository.FindClientByIdAsync(clientId);
            return TypeAdapter.Adapt<Client>(client);
        }

        public async Task<IEnumerable<ClientClaim>> FindClientClaimByClientIdAsync(int clientId)
        {
            var claims = await clientClaimsRepository.FindClientClaimByClientIdAsync(clientId);
            return TypeAdapter.Adapt<IEnumerable<ClientClaim>>(claims);
        }

        public async Task<ClientClaim> FindClientClaimByIdAsync(int id, int clientId)
        {
            var claims = await clientClaimsRepository.FindClientClaimByIdAsync(id, clientId);
            return TypeAdapter.Adapt<ClientClaim>(claims);
        }

        public async Task<ClientCorsOrigin> FindClientCorsOriginByIdAsync(int clientId)
        {
            var data = await clientCorsOriginsRepository.FindClientCorsOriginsByClientIdAsync(clientId);
            return TypeAdapter.Adapt<ClientCorsOrigin>(data);
        }

        public async Task<IEnumerable<ClientGrantType>> FindClientGrantTypeByClientIdAsync(int clientId)
        {
            var data = await clientGrantTypesRepository.FindClientGrantTypeByClientIdAsync(clientId);
            return TypeAdapter.Adapt<IEnumerable<ClientGrantType>>(data);
        }

        public async Task<ClientGrantType> FindClientGrantTypeByIdAsync(int id, int clientId)
        {
            var data = await clientGrantTypesRepository.FindClientGrantTypeByIdAsync(id, clientId);
            return TypeAdapter.Adapt<ClientGrantType>(data);
        }

        public async Task<IEnumerable<ClientPostLogoutRedirectUri>> FindClientPostLogoutRedirectUriByClientIdAsync(int clientId)
        {
            var data = await clientPostLogoutRedirectUrisRepository.FindClientPostLogoutRedirectUriByClientIdAsync(clientId);
            return TypeAdapter.Adapt<IEnumerable<ClientPostLogoutRedirectUri>>(data);
        }

        public async Task<ClientPostLogoutRedirectUri> FindClientPostLogoutRedirectUriByIdAsync(int id, int clientId)
        {
            var data = await clientPostLogoutRedirectUrisRepository.FindClientPostLogoutRedirectUriByIdAsync(id, clientId);
            return TypeAdapter.Adapt<ClientPostLogoutRedirectUri>(data);
        }

        public async Task<IEnumerable<ClientRedirectUri>> FindClientRedirectUriByClientIdAsync(int clientId)
        {
            var data = await clientRedirectUrisRepository.FindClientRedirectUriByClientIdAsync(clientId);
            return TypeAdapter.Adapt<IEnumerable<ClientRedirectUri>>(data);
        }

        public async Task<ClientRedirectUri> FindClientRedirectUriByIdAsync(int id, int clientId)
        {
            var data = await clientRedirectUrisRepository.FindClientRedirectUriByIdAsync(id, clientId);
            return TypeAdapter.Adapt<ClientRedirectUri>(data);
        }

        public async Task<IEnumerable<ClientSecret>> FindClientSecretByClientIdAsync(int clientId)
        {
            var data = await clientSecretsRepository.FindClientSecretByClientIdAsync(clientId);
            return TypeAdapter.Adapt<IEnumerable<ClientSecret>>(data);
        }

        public async Task<ClientSecret> FindClientSecretByIdAsync(int id, int clientId)
        {
            var data = await clientSecretsRepository.FindClientSecretByIdAsync(id, clientId);
            return TypeAdapter.Adapt<ClientSecret>(data);
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            var client = await clientRepository.GetAllClients();
            return TypeAdapter.Adapt<IEnumerable<Client>>(client);
        }

        public async Task RemoveClient(int id, int clientId)
        {
            await clientRepository.RemoveClient(id, clientId);
        }

        public async Task RemoveClientClaim(int id, int clientId)
        {
            await clientClaimsRepository.RemoveClientClaim(id, clientId);
        }

        public async Task RemoveClientCorsOrigin(int id, int clientId)
        {
            await clientCorsOriginsRepository.RemoveClientCorsOriginsByIdAsync(id, clientId);
        }

        public async Task RemoveClientGrantType(int id, int clientId)
        {
            await clientGrantTypesRepository.RemoveClientGrantType(id, clientId);
        }

        public async Task RemoveClientPostLogoutRedirectUri(int id, int clientId)
        {
            await clientPostLogoutRedirectUrisRepository.RemoveClientPostLogoutRedirectUri(id, clientId);
        }

        public async Task RemoveClientRedirectUri(int id, int clientId)
        {
            await clientRedirectUrisRepository.RemoveClientRedirectUri(id, clientId);
        }

        public async Task RemoveClientSecret(int id, int clientId)
        {
            await clientSecretsRepository.RemoveClientSecret(id, clientId);
        }

        public Task<IEnumerable<Client>> SearchClients(int pageId, string clientName, string clientCode)
        {
            throw new NotImplementedException();
        }

        public async Task StoreClient(Client client)
        {
            await clientRepository.StoreClient(TypeAdapter.Adapt<Model.Entities.Client>(client));
        }

        public async Task StoreClientClaim(ClientClaim clientClaim)
        {
            await clientClaimsRepository.StoreClientClaim(TypeAdapter.Adapt<Model.Entities.ClientClaim>(clientClaim));
        }

        public async Task StoreClientCorsOrigin(ClientCorsOrigin clientCorsOrigin)
        {
            await clientCorsOriginsRepository.StoreClientCorsOrigin(TypeAdapter.Adapt<Model.Entities.ClientCorsOrigin>(clientCorsOrigin)); 
        }

        public async Task StoreClientGrantType(ClientGrantType clientGrantType)
        {
            await clientGrantTypesRepository.StoreClientGrantType(TypeAdapter.Adapt<Model.Entities.ClientGrantType>(clientGrantType));
        }

        public async Task StoreClientPostLogoutRedirectUri(ClientPostLogoutRedirectUri clientPostLogoutRedirectUri)
        {
            await clientPostLogoutRedirectUrisRepository.StoreClientPostLogoutRedirectUri(TypeAdapter.Adapt<Model.Entities.ClientPostLogoutRedirectUri>(clientPostLogoutRedirectUri));
        }

        public async Task StoreClientRedirectUri(ClientRedirectUri clientRedirectUri)
        {
            await clientRedirectUrisRepository.StoreClientRedirectUri(TypeAdapter.Adapt<Model.Entities.ClientRedirectUri>(clientRedirectUri)); 
        }

        public async Task StoreClientSecret(ClientSecret clientSecret)
        {
            await clientSecretsRepository.StoreClientSecret(TypeAdapter.Adapt<Model.Entities.ClientSecret>(clientSecret));
        }

        public async Task StoreClientScope(ClientScope clientScope)
        {
            await clientScopesRepository.StoreClientScope(TypeAdapter.Adapt<Model.Entities.ClientScope>(clientScope));
        }

        public async Task<ClientScope> FindClientScopeByIdAsync(int id, int clientId)
        {
            var data = await clientScopesRepository.FindClientScopeByIdAsync(id, clientId);
            return TypeAdapter.Adapt<ClientScope>(data);
        } 

        public async Task RemoveClientScope(int id, int clientId)
        {
            await clientScopesRepository.RemoveClientScope(id, clientId);
        }

        public async Task<IEnumerable<ClientScope>> FindClientScopeByClientIdAsync(int clientId)
        {
            var data = await clientScopesRepository.FindClientScopeByClientIdAsync(clientId);
            return TypeAdapter.Adapt<IEnumerable<ClientScope>>(data);
        }

        public async Task<int> GetIdByClientID(string clientId)
        {
            return await clientRepository.GetIdByClientID(clientId);
        }
    }
}