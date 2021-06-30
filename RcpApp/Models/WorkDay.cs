using System;

namespace RcpApp.Models
{
    public class WorkDay
    {
        public string KodPracownika { get; set; }

        public DateTime? Data { get; set; }

        public TimeSpan? GodzinaWejscia { get; set; }

        public TimeSpan? GodzinaWyjscia { get; set; }
    }
}