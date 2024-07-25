using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArumKillFeed.Types
{
    public class KillFeedCause
    {
        [XmlAttribute]
        public EDeathCause Cause { get; set; }
        [XmlAttribute]
        public bool Enabled { get; set; }
    }
}
