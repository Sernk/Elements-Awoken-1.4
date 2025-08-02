using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Const
    {
        public static SpriteBatch Sb => Main.spriteBatch;
        public static int PinkFlame => DustID.Firework_Pink;
        public static void SetSoul(int type)
        {
            ItemID.Sets.AnimatesAsSoul[type] = true;
        }
        public static void Longer(int type)
        {
            BuffID.Sets.LongerExpertDebuff[type] = true;
        }
        public static void CanBeCleared(int type)
        {
            BuffID.Sets.NurseCannotRemoveDebuff[type] = true;
        }
        public static void DSCursor(int type)
        {
            TileID.Sets.DisableSmartCursor[type] = true;
        }
        public static IEntitySource Proj(Projectile projectile)
        {
            return projectile.GetSource_FromThis();
        }
        public static IEntitySource NPCs(NPC npc)
        {
            return npc.GetSource_FromThis();
        }
        public static IEntitySource Players(Player player)
        {
            return player.GetSource_FromThis();
        }
        public static int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0: return ModContent.DustType<AncientRed>();   
                case 1: return ModContent.DustType<AncientGreen>();   
                case 2: return ModContent.DustType<AncientBlue>();  
                case 3: return ModContent.DustType<AncientPink>();
                default: return ModContent.DustType<AncientRed>();
            }
        }
        public static List<int> VanillaOreIDs()
        {
            List<int> idList = new List<int>()
            {
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
            };
            return idList;
        }
    }
}