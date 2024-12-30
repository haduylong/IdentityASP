using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public long ExpiryTime { get; set; }
        public long RefreshTime { get; set; }
    }
}
