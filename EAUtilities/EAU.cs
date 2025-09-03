using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Tiles.Crafting;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken
{
    /// <summary>
    /// Elements Awoken Utilities
    /// TODO
    /// </summary>
    public class EAU
    {
        public static int FireEssence => ModContent.ItemType<FireEssence>();
        public static int VoidEssence => ModContent.ItemType<VoidEssence>();
        public static int ElementalForge => ModContent.TileType<ElementalForge>();
        public static int PinkFlame => DustID.Firework_Pink;
        public static int HandsOfDespair => ModContent.BuffType<HandsOfDespair>();
        public static int Dragonfire => ModContent.BuffType<Dragonfire>();
        public static int BaffsTime(int seconds = 0, int minutes = 0, int hours = 0) { int result = 0; result += seconds * 60; result += minutes * 60 * 60; result += hours * 60 * 60 * 60; return result; }
        public static SpriteBatch Sb => Main.spriteBatch;
        public static void SetSoul(int type) => ItemID.Sets.AnimatesAsSoul[type] = true;
        public static void Longer(int type) => BuffID.Sets.LongerExpertDebuff[type] = true;
        public static void CanBeCleared(int type) => BuffID.Sets.NurseCannotRemoveDebuff[type] = true;
        public static void DSCursor(int type) => TileID.Sets.DisableSmartCursor[type] = true;
        public static IEntitySource Proj(Projectile projectile) => projectile.GetSource_FromThis();
        public static IEntitySource NPCs(NPC npc) => npc.GetSource_FromThis();
        public static IEntitySource Play(Player player) => player.GetSource_FromThis();
        public static int GetDustID()
        {
            return Main.rand.Next(4) switch
            {
                0 => ModContent.DustType<AncientRed>(), 1 => ModContent.DustType<AncientGreen>(),
                2 => ModContent.DustType<AncientBlue>(), 3 => ModContent.DustType<AncientPink>(),
                _ => ModContent.DustType<AncientRed>(),
            };
        }
        public static int GetDustIDForAI(Projectile projectile)
        {
            int index = (int)projectile.ai[0];

            if (index >= 0 && index < EAList.DustIDs.Count) return EAList.DustIDs[index];

            return EAList.DustIDs[0];
        }
        public static List<int> VanillaOreIDs()
        {
            List<int> idList =
            [
                ItemID.CopperOre,
                ItemID.TinOre,
                ItemID.IronOre,
                ItemID.LeadOre,
                ItemID.SilverOre,
                ItemID.TungstenOre,
                ItemID.GoldOre,
                ItemID.PlatinumOre,
                ItemID.Meteorite,
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre,
                ItemID.Hellstone,
                ItemID.CobaltOre,
                ItemID.PalladiumOre,
                ItemID.MythrilOre,
                ItemID.OrichalcumOre,
                ItemID.AdamantiteOre,
                ItemID.TitaniumOre,
                ItemID.ChlorophyteOre,
                ItemID.LunarOre,
            ];
            return idList;
        }
    }
}