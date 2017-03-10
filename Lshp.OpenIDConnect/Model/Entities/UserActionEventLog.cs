using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Model.Entities
{
    public class UserActionEventLog
    {
        public int ID;
        public int EventID;
        public string LogLevel;
        public string Message;
        public DateTime DateCreated;
        public DateTime DateCreatedUtc;
    }
}
