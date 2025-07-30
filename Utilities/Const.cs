using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace ElementsAwoken
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Const
    {
        public static SpriteBatch Sb => Main.spriteBatch;
        //public static void SetAnimationSoul<T>() where T : ModItem
        //{
        //    ItemID.Sets.AnimatesAsSoul[ModContent.ItemType<T>()] = true;
        //}
        public static void SetSoul(int type)
        {
            ItemID.Sets.AnimatesAsSoul[type] = true;
        }
    }
}