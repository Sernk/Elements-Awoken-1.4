using ElementsAwoken.Content.Mounts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.Obsidious
{
    public class SacredCrystal : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.value = Item.buyPrice(0, 20, 0, 0);
            Item.rare = 6;
            Item.expert = true;
            Item.UseSound = SoundID.Item79;
            Item.noMelee = true;
            Item.mountType = ModContent.MountType<SacredCrystalMount>();
            Item.color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 200);
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Item.color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 200);
        }
    }
}