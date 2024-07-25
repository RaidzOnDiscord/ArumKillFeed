using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArumKillFeed.Types
{
    public class KillFeedInfo
    {
        public string Translate { get; set; }
        public DateTime TimeStart { get; private set; } = DateTime.Now;
    }
}
