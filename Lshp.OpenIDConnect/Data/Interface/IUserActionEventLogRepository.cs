using Lshp.OpenIDConnect.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Data.Interface
{
   public interface IUserActionEventLogRepository
    {
        Task StoreUserActionEventLog(UserActionEventLog userActionEventLog);
    }
}
