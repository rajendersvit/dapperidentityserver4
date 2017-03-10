using Lshp.OpenIDConnect.Data.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.Logger
{
    public class SQLLoggerProvider : ILoggerProvider
    {
        private IUserActionEventLogRepository userActionEventLogRepository;
        readonly Func<string, LogLevel, bool> filter;
        public SQLLoggerProvider(IUserActionEventLogRepository userActionEventLogRepository, Func<string, LogLevel, bool> filter)
        {
            this.userActionEventLogRepository = userActionEventLogRepository;
            this.filter = filter;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new SQLLogger(userActionEventLogRepository, categoryName, filter);
        }

        public void Dispose()
        {
            
        }
    }
}
