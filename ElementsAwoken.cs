using ElementsAwoken.Content.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace ElementsAwoken
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.

	public class ElementsAwoken : Mod
	{
        public static List<int> instakillImmune = new List<int>();
        public const int bossPromptDelay = 108000;
        public static bool calamityEnabled;
        public static int encounter = 0;
        public static bool encounterSetup = false;
        public static int encounterTimer = 0;
        public static int encounterShakeTimer = 0;
        public static ElementsAwoken instance;
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ElementsAwokenMessageType msgType = (ElementsAwokenMessageType)reader.ReadByte();
            switch (msgType)
            {
                case ElementsAwokenMessageType.StarHeartSync:
                    byte playernumber = reader.ReadByte();
                    MyPlayer starHeartPlayer = Main.player[playernumber].GetModPlayer<MyPlayer>();
                    int voidHeartsUsed = reader.ReadInt32();
                    int chaosHeartsUsed = reader.ReadInt32();
                    int lunarStarsUsed = reader.ReadInt32();
                    starHeartPlayer.voidHeartsUsed = voidHeartsUsed;
                    starHeartPlayer.chaosHeartsUsed = chaosHeartsUsed;
                    starHeartPlayer.lunarStarsUsed = lunarStarsUsed;
                    break;
                /*case ElementsAwokenMessageType.AwakenedSync:
                    playernumber = reader.ReadByte();
                    AwakenedPlayer awakenedPlayer = Main.player[playernumber].GetModPlayer<AwakenedPlayer>();
                    int sanity = reader.ReadInt32();
                    awakenedPlayer.sanity = sanity;
                    break;
                case ElementsAwokenMessageType.EnergySync:
                    playernumber = reader.ReadByte();
                    PlayerEnergy energyPlayer = Main.player[playernumber].GetModPlayer<PlayerEnergy>();
                    int energy = reader.ReadInt32();
                    energyPlayer.energy = energy;
                    break;
                case ElementsAwokenMessageType.Storyteller:
                    if (Main.npc[reader.ReadInt32()].ModNPC is Storyteller townNPC && townNPC.npc.active)
                    {
                        townNPC.HandlePacket(reader);
                    }
                    break;*/
                default:
                    Logger.WarnFormat("Elements Awoken: Unknown Message type: {0}", msgType);
                    break;
            }
        }
        public override void Load()
        {
            instance = this;
            if (!Main.dedServ)
            {
                Filters.Scene["ElementsAwoken:VoidLeviathanHead"] = new Filter(new VoidLeviathanScreenShaderData("FilterMiniTower").UseColor(1.0f, 0.2f, 0.55f).UseOpacity(0.4f), EffectPriority.VeryHigh);
            }
        }

        public static void DebugModeText(object text, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (ModContent.GetInstance<Config>().debugMode)
            {
                Main.NewText(text, color);
            }
        }
        public enum ElementsAwokenMessageType : byte
        {
            StarHeartSync,
            AwakenedSync,
            EnergySync,
            Storyteller,
        }

    }
}
