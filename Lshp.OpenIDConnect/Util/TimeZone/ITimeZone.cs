using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lshp.OpenIDConnect.Util.TimeZone
{
    public interface ITimeZone
    {
        DateTime ConvertToLocalTime(DateTime utcDateTime, string timeZoneId);
        DateTime ConvertToUtc(DateTime localDateTime, string timeZoneId);//, ZoneLocalMappingResolver resolver = null);
        IReadOnlyCollection<string> GetTimeZoneList();
    }
    
}
