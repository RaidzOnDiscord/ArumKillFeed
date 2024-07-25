using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArumKillFeed.Types;
using Rocket.API;

namespace ArumKillFeed
{
    public class Config : IRocketPluginConfiguration
    {
        public bool DownloadWorkshop;
        public ushort EffectID;
        public short EffectKey;
        public byte Lines;
        public float DurationClose;
        public byte FontSize;
        public byte MaxCharsName;
        public List<KillFeedCause> KillFeedCauses;
        public void LoadDefaults()
        {
            DownloadWorkshop = true;
            EffectID = 21394;
            EffectKey = 21394;
            Lines = 5;
            DurationClose = 3.75f;
            FontSize = 14;
            MaxCharsName = 16;
            KillFeedCauses = new List<KillFeedCause>
            {
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.ACID, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.ANIMAL, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.ARENA, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.BLEEDING, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.BONES, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.BOULDER, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.BREATH, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.BURNER, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.BURNING, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.CHARGE, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.FOOD, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.FREEZING, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.GRENADE, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.GUN, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.INFECTION, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.LANDMINE, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.MELEE, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.MISSILE, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.PUNCH, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.ROADKILL, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.SENTRY, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.SHRED, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.SPARK, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.SPIT, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.SPLASH, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.SUICIDE, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.VEHICLE, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.WATER, Enabled = true },
                new KillFeedCause{ Cause = SDG.Unturned.EDeathCause.ZOMBIE, Enabled = true },
            };
        }
    }
}
