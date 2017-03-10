using Lshp.OpenIDConnect.Data.Repository;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Lshp.OpenIDConnect.Service.Logger
{
    public static class SQLLoggerFactoryExtensions
    {
        public static ILoggerFactory AddSQLLogger(this ILoggerFactory factory, IServiceProvider serviceProvider, Func<string, LogLevel, bool> filter = null)
         {
            if (factory == null) throw new ArgumentNullException("factory");

            factory.AddProvider(new SQLLoggerProvider(new UserActionEventLogRepository(serviceProvider.GetService<IOptions<ConfigEntry>>()), filter));

            return factory;
        }
    }
}
