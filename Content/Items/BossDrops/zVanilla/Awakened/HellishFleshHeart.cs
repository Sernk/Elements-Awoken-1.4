using ElementsAwoken.Content.Projectiles.Minions;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla.Awakened
{
    public class HellishFleshHeart : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.accessory = true;
            Item.rare = ModContent.RarityType<EARarity.Awakened>();
            Item.GetGlobalItem<EARaritySettings>().awakened = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.hellHeart = true;

            if (player.ownedProjectileCounts[ModContent.ProjectileType<HungryMinion>()] < 3)
            {
                Projectile.NewProjectile(EAU.Play(player), player.Center,Main.rand.NextVector2Square(2,2), ModContent.ProjectileType<HungryMinion>(), 30, 5f, player.whoAmI, 0);
            }
        }
    }
}