using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service
{
    public class IdentityServerBaseUrlMiddleware
    {
        private readonly RequestDelegate _next;

        public IdentityServerBaseUrlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var origin = string.Empty;
            if (context.Request.IsHttps || // Handles https straight to the server
                   context.Request.Headers["X-Forwarded-Proto"] == "https" || // Handles an IIS or Azure passthrough
                   context.Request.Headers["X-Forwarded-Proto"].Contains("https"))
            {
                origin = "https://" + request.Host.Value;
            }
            else
            {
                origin = request.Scheme + "://" + request.Host.Value;
            }

            context.SetIdentityServerOrigin(origin);

            string url = request.PathBase.Value;
            if (url != null && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            context.SetIdentityServerBasePath(url);

            await _next(context);
        }
    }
}