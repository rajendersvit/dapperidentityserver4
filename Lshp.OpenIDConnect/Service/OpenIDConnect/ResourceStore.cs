using Lshp.OpenIDConnect.Data.Interface;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public class ResourceStore : IResourceStore
    {
        private IApiResourcesRepository resourcesRepository;

        public ResourceStore(IApiResourcesRepository resourcesRepository)
        {
            this.resourcesRepository = resourcesRepository;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var resource = await resourcesRepository.FindApiResourceAsync(name); 
            return await Task.FromResult(MapApiResource(resource));
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var resources = await resourcesRepository.FindApiResourcesByScopeAsync(scopeNames);
            List<ApiResource> apiResources = new List<ApiResource>();
            foreach (var resource in resources)
            {
                apiResources.Add(MapApiResource(resource));
            }
            return await Task.FromResult(apiResources);
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var resources = await resourcesRepository.FindIdentityResourcesByScopeAsync(scopeNames);
            if (resources == null) return null;
            List<IdentityResource> identityResources = new List<IdentityResource>();

            foreach (var item in resources)
            {
                identityResources.Add(MapIdentityResource(item));
            }
            return await Task.FromResult(identityResources);
        }

        public async Task<Resources> GetAllResources()
        {

            List<IdentityResource> identityResources = new List<IdentityResource>();
            List<ApiResource> apiResources = new List<ApiResource>();
            var apiResource = await resourcesRepository.GetAllApiResources();
            foreach(var item in apiResource)
            {
                apiResources.Add(MapApiResource(item));
            }

            var identityResource = await resourcesRepository.GetAllIdentityResource();
            foreach(var item in identityResource)
            {
                identityResources.Add(MapIdentityResource(item));
            }
            var resource = new Resources(identityResources, apiResources);
            return await Task.FromResult(resource);
        }

        public ApiResource MapApiResource(Model.Entities.ApiResource resource)
        {
            if (resource == null) return null;
            ApiResource apiResource = new ApiResource();
            apiResource.Name = resource.Name;
            apiResource.Scopes = (from r in resource.ApiScopes
                                  select new Scope()
                                  {
                                      Description = r.Description,
                                      Emphasize = r.Emphasize,
                                      DisplayName = r.DisplayName
,
                                      Name = r.Name,
                                      Required = r.Required,
                                      ShowInDiscoveryDocument = r.ShowInDiscoveryDocument
                                  }).ToList();
            apiResource.UserClaims = (from r in resource.ApiScopeClaim select r.Type).ToList();
            apiResource.Enabled = resource.Enabled;
            apiResource.DisplayName = resource.DisplayName;
            apiResource.ApiSecrets = (from r in resource.ApiSecrets
                                      select new Secret()
                                      {
                                          Description = r.Description,
                                          Type = r.Type,
                                          Expiration = r.Expiration,
                                          Value = r.Value
                                      }).ToList();
            return apiResource;
        }
        public IdentityResource MapIdentityResource(Model.Entities.IdentityResource model)
        {
            if (model == null) return null;
            IdentityResource identityResource = new IdentityResource();
            identityResource.Description = model.Description;
            identityResource.DisplayName = model.DisplayName;
            identityResource.Emphasize = model.Emphasize;
            identityResource.Enabled = model.Enabled;
            identityResource.Name = model.Name;
            identityResource.Required = model.Required;
            identityResource.ShowInDiscoveryDocument = model.ShowInDiscoveryDocument;
            identityResource.UserClaims = model.IdentityClaims.Select(i => i.Type).ToList();
            return identityResource;
        }
    }
}