using Microsoft.Xna.Framework.Graphics;
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
    }
}