using Lshp.OpenIDConnect.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lshp.OpenIDConnect.Util.ConfigManager;
using Microsoft.Extensions.Options;
using Lshp.OpenIDConnect.Model.Entities;
using Dapper;

namespace Lshp.OpenIDConnect.Data.Repository
{
    public class UserActionEventLogRepository : BaseRepository, IUserActionEventLogRepository
    {
        public UserActionEventLogRepository(IOptions<ConfigEntry> option) : base(option)
        {
        }

        public async Task StoreUserActionEventLog(Model.Entities.UserActionEventLog userActionEventLog)
        {
            using (var db = GetConnection())
            {
                await db.ExecuteAsync("INSERT INTO UserActionEventLog(EventID,LogLevel,Message,DateCreated,DateCreatedUtc) VALUES(@EventID,@LogLevel,@Message,@DateCreated,@DateCreatedUtc) ", new { @EventID = userActionEventLog.EventID, @LogLevel = userActionEventLog.LogLevel, @Message = userActionEventLog.Message, @DateCreated = DateTime.Now, @DateCreatedUtc = DateTime.UtcNow });
            }
        }
    }
}
