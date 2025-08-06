using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla.Awakened
{
    public class BleedingHeart : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ModContent.RarityType<EARarity.Awakened>();
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.rand.NextBool(4))
            {
                Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 5, 0, 0, 0, default(Color))];
                dust.velocity.X *= 0.2f;
                dust.velocity.Y = 0;
            }

            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.bleedingHeart = true;
        }
    }
}