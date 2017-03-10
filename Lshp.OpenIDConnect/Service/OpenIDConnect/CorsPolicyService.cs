using Lshp.OpenIDConnect.Data.Interface;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.OpenIDConnect
{
    public class CorsPolicyService : ICorsPolicyService
    {
        IClientCorsOriginsRepository clientCorsOriginsRepository;
        public CorsPolicyService(IClientCorsOriginsRepository clientCorsOriginsRepository)
        {
            this.clientCorsOriginsRepository = clientCorsOriginsRepository;
        }
        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            return await clientCorsOriginsRepository.IsOriginAllowedAsync(origin);
        }
    }
}
