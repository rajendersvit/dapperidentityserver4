using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.Middleware
{
    public class CustomHttpHeadersMiddleware
    {
        /// <summary>
        /// The delegate for the next part of the pipeline. This field is read-only.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The name of the current hosting environment. This field is read-only.
        /// </summary>
        private readonly string _environmentName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHttpHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The delegate for the next part of the pipeline.</param> 
        public CustomHttpHeadersMiddleware(
            RequestDelegate next, IHostingEnvironment environment)
        {
            _next = next;
            _environmentName = environment.EnvironmentName;
        }

        /// <summary>
        /// Invokes the middleware asynchronously.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the actions performed by the middleware.
        /// </returns>
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("X-Powered-By");
                if (_environmentName != null)
                {
                    context.Response.Headers.Add("X-Environment", _environmentName);
                }

                context.Response.Headers.Add("X-Instance", Environment.MachineName);
                context.Response.Headers.Add("X-Request-Id", context.TraceIdentifier);

                stopwatch.Stop();

                string duration = stopwatch.Elapsed.TotalMilliseconds.ToString("0.00ms", CultureInfo.InvariantCulture);

                context.Response.Headers.Add("X-Request-Duration", duration);

                return Task.CompletedTask;
            });

            await _next(context);
        } 
    }
}
