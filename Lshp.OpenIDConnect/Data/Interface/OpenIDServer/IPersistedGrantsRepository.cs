using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
    public interface IPersistedGrantsRepository
    {
        Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId);
        Task<PersistedGrant> GetAsync(string key);
        Task RemoveAllAsync(string subjectId, string clientId);
        Task RemoveAllAsync(string subjectId, string clientId, string type);
        Task RemoveAsync(string key);
        Task StoreAsync(PersistedGrant grant);

    }
}
