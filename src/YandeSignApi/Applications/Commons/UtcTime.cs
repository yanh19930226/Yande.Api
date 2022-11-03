using System;

namespace YandeSignApi.Applications.Commons
{
    public class UtcTime
    {
        

        private UtcTime()
        {
        }

        public static long CurrentTimeMillis() => (DateTime.UtcNow.Ticks - UtcTime.UtcStartTicks) / 10000L;
    }
}
