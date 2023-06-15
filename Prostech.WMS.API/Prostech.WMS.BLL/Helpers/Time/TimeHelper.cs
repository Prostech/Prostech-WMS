using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prostech.WMS.BLL.Helpers.Time
{
    public static class TimeHelper
    {
        public static DateTime CurrentTime
        {
            get
            {
                DateTime utcTime = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, cstZone);
                return cstTime;
            }
        }
    }

}
