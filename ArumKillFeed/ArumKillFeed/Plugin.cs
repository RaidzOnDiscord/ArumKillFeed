using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using SDG.Unturned;
using UnityEngine;
using Rocket.Core.Plugins;
using Rocket.API.Collections;
using Steamworks;
using ArumKillFeed.Types;
using Rocket.Core;
using Rocket.API.Serialisation;
using ArumColorPlugin.Types;

namespace ArumKillFeed
{
    public class Plugin : RocketPlugin<Config>
    {
        public override TranslationList DefaultTranslations => new TranslationList
        {
            { "ACID", "{0} был убит кислотой зомби" },
            { "ANIMAL", "{0} был убит животным" },
            { "ARENA", "{0} умер за зоной" },
            { "BLEEDING", "{0} был убит от кравотечения" },
            { "BONES", "{0} разбился о землю" },
            { "BOULDER", "{0} был убит камнем" },
            { "BREATH", "{0} задохнулся" },
            { "BURNER", "{0} был сожжён огнем зомби" },
            { "BURNING", "{0} сгорел заживо" },
            { "CHARGE", "{1} подорвал {0} взрывчаткой" },
            { "FOOD", "{0} умер от голода" },
            { "FREEZING", "{0} замёрз до смерти" },
            { "GRENADE", "{0} был убит гранатой игроком {1}" },
            { "GUN", "{0} был убит с [{1}] игроком: [{2}] [{3}m]" },
            { "INFECTION", "{0} умер из-за инфекции =(" },
            { "LANDMINE", "{0} наступил на мину" },
            { "MELEE", "{0} был убит холодным оружием игроком {1} [{2}m]" },
            { "MISSILE", "{1} запустил {0} полетать на ракете" },
            { "PUNCH", "{0} был затыкан руками игроком {1}" },
            { "ROADKILL", "{0} сбит машиной игроком {1}" },
            { "SENTRY", "{0} был застрелен турелью" },
            { "SHRED", "{0} напаролся на шипы" },
            { "SPARK", "{0} умер от тока высокого напряжения" },
            { "SPIT", "{0} расстворился в кислоте зомби" },
            { "SPLASH", "{1} убил {0} осколками снарядов от {1}  патронов" },
            { "SUICIDE", "{0} сделал суицид" },
            { "VEHICLE", "{0} взорвался вместе с транспортом" },
            { "WATER", "{0} умер от жажды" },
            { "ZOMBIE", "{0} был убит от зомби" },
        };

        public static Plugin Instance;
        DateTime LastCalled = DateTime.Now;
        List<KillFeedInfo> KFI = new List<KillFeedInfo>();
        protected override void Load()
        {
            Instance = this;
            if (Configuration.Instance.DownloadWorkshop) WorkshopDownloadConfig.getOrLoad().File_IDs.Add(2024941699);
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
            foreach (SteamPlayer sp in Provider.clients)
            {
                EffectManager.sendUIEffect(Configuration.Instance.EffectID, Configuration.Instance.EffectKey, UnturnedPlayer.FromSteamPlayer(sp).CSteamID, true);
            }
        }

        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Configuration.Instance.EffectID, Configuration.Instance.EffectKey, player.CSteamID, true);
            MoveNext();
        }

        private void UnturnedPlayerEvents_OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            try
            {
                UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(murderer);
                if (!IsEnabledCause(cause)) return;
                switch (cause)
                {
                    case EDeathCause.ACID:
                        KillFeedInfo kfi = new KillFeedInfo { Translate = Translate("ACID", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.ANIMAL:
                        kfi = new KillFeedInfo { Translate = Translate("ANIMAL", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.ARENA:
                        kfi = new KillFeedInfo { Translate = Translate("ARENA", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.BLEEDING:
                        kfi = new KillFeedInfo { Translate = Translate("BLEEDING", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.BONES:
                        kfi = new KillFeedInfo { Translate = Translate("BONES", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.BOULDER:
                        kfi = new KillFeedInfo { Translate = Translate("BOULDER", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.BREATH:
                        kfi = new KillFeedInfo { Translate = Translate("BREATH", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.BURNER:
                        kfi = new KillFeedInfo { Translate = Translate("BURNER", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.BURNING:
                        kfi = new KillFeedInfo { Translate = Translate("BURNING", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.CHARGE:
                        kfi = new KillFeedInfo { Translate = Translate("CHARGE", GetColor(player), GetColor(killer)) };
                        KFI.Insert(0, kfi);
                        MoveNext();
                        break;
                    case EDeathCause.FOOD:
                        kfi = new KillFeedInfo { Translate = Translate("FOOD", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.FREEZING:
                        kfi = new KillFeedInfo { Translate = Translate("FREEZING", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.GRENADE:
                        if (killer != null && player.CSteamID == killer.CSteamID) return;
                        kfi = new KillFeedInfo { Translate = Translate("GRENADE", GetColor(player), GetColor(killer)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.GUN:
                        if (killer != null && player.CSteamID == killer.CSteamID) return;
                        kfi = new KillFeedInfo { Translate = Translate("GUN", GetColor(player), killer.Player.equipment.asset.itemName, GetColor(killer), System.Math.Round(Vector3.Distance(player.Position, killer.Position), 2)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.INFECTION:
                        kfi = new KillFeedInfo { Translate = Translate("INFECTION", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.LANDMINE:
                        kfi = new KillFeedInfo { Translate = Translate("LANDMINE", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.MELEE:
                        if (killer != null && player.CSteamID == killer.CSteamID) return;
                        kfi = new KillFeedInfo { Translate = Translate("MELEE", GetColor(player), GetColor(killer), System.Math.Round(Vector3.Distance(player.Position, killer.Position), 2)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.MISSILE:
                        if (killer != null && player.CSteamID == killer.CSteamID) return;
                        kfi = new KillFeedInfo { Translate = Translate("MISSILE", GetColor(player), GetColor(killer)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.PUNCH:
                        kfi = new KillFeedInfo { Translate = Translate("PUNCH", GetColor(player), GetColor(killer)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.ROADKILL:
                        if (killer != null && player.CSteamID == killer.CSteamID) return;
                        kfi = new KillFeedInfo { Translate = Translate("ROADKILL", GetColor(player), GetColor(killer)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.SENTRY:
                        kfi = new KillFeedInfo { Translate = Translate("SENTRY", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.SHRED:
                        kfi = new KillFeedInfo { Translate = Translate("SHRED", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.SPARK:
                        kfi = new KillFeedInfo { Translate = Translate("SPARK", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.SPIT:
                        kfi = new KillFeedInfo { Translate = Translate("SPIT", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.SPLASH:
                        kfi = new KillFeedInfo { Translate = Translate("SPLASH", GetColor(player), GetColor(killer), killer.Player.equipment.asset.itemName) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.SUICIDE:
                        kfi = new KillFeedInfo { Translate = Translate("SUICIDE", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.VEHICLE:
                        kfi = new KillFeedInfo { Translate = Translate("VEHICLE", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.WATER:
                        kfi = new KillFeedInfo { Translate = Translate("WATER", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                    case EDeathCause.ZOMBIE:
                        kfi = new KillFeedInfo { Translate = Translate("ZOMBIE", GetColor(player)) };
                        AddKFI(kfi);
                        break;
                }
            } catch (Exception ex) { Rocket.Core.Logging.Logger.LogException(ex, "Exception in OnPlayerDeath"); }
        }

        private void AddKFI(KillFeedInfo kfi)
        {
            KFI.Insert(0, kfi);
            MoveNext();
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
        }

        public void FixedUpdate()
        {
            if ((DateTime.Now - LastCalled).TotalSeconds >= 0.1)
            {
                foreach (KillFeedInfo kfi in KFI.ToList())
                {
                    if ((DateTime.Now - kfi.TimeStart).TotalSeconds >= Configuration.Instance.DurationClose)
                    {
                        byte index = (byte)KFI.IndexOf(kfi);
                        SendUIEffectText($"arumkillfeed.{index}.text", "");
                        KFI.Remove(kfi);
                        MoveNext();
                    }
                }
                LastCalled = DateTime.Now;
            }
        }

        private void MoveNext()
        {
            if (KFI.Count == Configuration.Instance.Lines)
            {
                SendUIEffectText($"arumkillfeed.{KFI.Count - 1}.text", "");
                KFI.RemoveAt(KFI.Count - 1);
            }
            foreach (KillFeedInfo kfi in KFI.ToList())
            {
                byte index = (byte)KFI.IndexOf(kfi);
                SendUIEffectText($"arumkillfeed.{index}.text", string.Format("<size={0}>{1}</size>", Configuration.Instance.FontSize, kfi.Translate));
            }
        }

        private void SendUIEffectText(string childName, string text)
        {
            foreach (SteamPlayer sp in Provider.clients)
            {
                UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(sp);
                EffectManager.sendUIEffect(Configuration.Instance.EffectID, Configuration.Instance.EffectKey, player.CSteamID, false);
                EffectManager.sendUIEffectText(Configuration.Instance.EffectKey, player.CSteamID, true, childName, text);
            }
        }

        private bool IsEnabledCause(EDeathCause cause)
        {
            foreach (KillFeedCause causeFromConfig in Configuration.Instance.KillFeedCauses)
            {
                if (causeFromConfig.Cause.ToString().ToLower() == cause.ToString().ToLower() && causeFromConfig.Enabled) return true;
            }
            return false;
        }

        private string GetColor(UnturnedPlayer player)
        {
            ArumColorPlugin.PlayerComponent component = player.GetComponent<ArumColorPlugin.PlayerComponent>();
            return string.Format("<color={0}>{1}</color>", component.User.Color, player.DisplayName.Length > Configuration.Instance.MaxCharsName ? player.DisplayName.Remove(Configuration.Instance.MaxCharsName, player.DisplayName.Length - Configuration.Instance.MaxCharsName).Replace("<", "").Replace(">", "") + "..." : player.DisplayName.Replace("<", "").Replace(">", ""));
        }
    }
}
