using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Lshp.OpenIDConnect.Data.Interface;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        IPersistedGrantsRepository persistedGrantsRepository;
        public PersistedGrantStore(IPersistedGrantsRepository persistedGrantsRepository)
        {
            this.persistedGrantsRepository  = persistedGrantsRepository;
        }
        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var persistedGrant = new List<PersistedGrant>();
            var data =  await persistedGrantsRepository.GetAllAsync(subjectId);
            foreach(var item in data )
            {
                persistedGrant.Add(MapPersisted(item));
            }
            return persistedGrant;
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            var data = await persistedGrantsRepository.GetAsync(key);
            return MapPersisted(data);
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
           return Task.FromResult(persistedGrantsRepository.RemoveAllAsync(subjectId,clientId));           
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            return Task.FromResult(persistedGrantsRepository.RemoveAllAsync(subjectId, clientId,type ));
        }

        public Task RemoveAsync(string key)
        { 
            return Task.FromResult(persistedGrantsRepository.RemoveAsync(key));
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            return Task.FromResult(persistedGrantsRepository.StoreAsync(new Model.Entities.PersistedGrant() { ClientId = grant.ClientId, CreationTime = grant.CreationTime, Data = grant.Data, Expiration = grant.Expiration, Key = grant.Key, SubjectId = grant.SubjectId, Type = grant.Type }));
        }
        private PersistedGrant MapPersisted(Model.Entities.PersistedGrant persistedGrant)
        {
            if (persistedGrant != null)
            {
                PersistedGrant identityPersisted = new PersistedGrant();
                identityPersisted.ClientId = persistedGrant.ClientId;
                identityPersisted.CreationTime = persistedGrant.CreationTime;
                identityPersisted.Data = persistedGrant.Data;
                identityPersisted.Expiration = persistedGrant.Expiration;
                identityPersisted.Key = persistedGrant.Key;
                identityPersisted.SubjectId = persistedGrant.SubjectId;
                identityPersisted.Type = persistedGrant.Type; 
                return identityPersisted;
            }
            return null;
        }
    }
}
