using Lshp.OpenIDConnect.Data.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Service.Logger
{
    public class SQLLogger : ILogger
    {
        private string category;
        private Func<string, LogLevel, bool> filter;
        private bool selfException = false;

        private IUserActionEventLogRepository userActionEventLogRepository; 
        public SQLLogger(IUserActionEventLogRepository userActionEventLogRepository, string category, Func<string, LogLevel, bool> filter) 
        {
            this.filter = filter;
            this.userActionEventLogRepository = userActionEventLogRepository;
            this.category = category;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (filter == null || filter(category, logLevel));
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (selfException)
            {
                selfException = false;
                return;
            }
            selfException = true;
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }
          await userActionEventLogRepository.StoreUserActionEventLog(new Model.Entities.UserActionEventLog()
            {
                DateCreated = DateTime.Now,
                DateCreatedUtc = DateTime.UtcNow,
                EventID = eventId.Id,
                LogLevel = logLevel.ToString(),
                Message = message
            });
        }
    }
}
