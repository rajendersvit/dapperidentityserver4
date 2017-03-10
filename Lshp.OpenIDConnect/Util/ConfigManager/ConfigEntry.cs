using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Util.ConfigManager
{
    public class ConfigEntry
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public Dictionary<string,string> AppSettings { get; set; }
        public SmsSetting SmsSettings { get; set; }
        public X502Certificate X502Certificates { get; set; }
    }
}
