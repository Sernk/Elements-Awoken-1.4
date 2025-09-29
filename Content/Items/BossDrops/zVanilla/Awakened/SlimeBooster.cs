using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla.Awakened
{
    public class SlimeBooster : ModItem
    {
        private int timer = 0;
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
            timer++;
               MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.slimeBooster = true;
            player.jumpSpeedBoost += 1.75f;
            if (player.velocity.X != 0 && player.velocity.Y == 0)
            {
                int mod = (int)MathHelper.Lerp(5, 1, MathHelper.Clamp(Math.Abs(player.velocity.X) / 20f, 0, 1));
                int timeleft = (int)MathHelper.Lerp(60, 20, MathHelper.Clamp(Math.Abs(player.velocity.X) / 20f, 0, 1));
                if (timer  % mod == 0 &&  Main.myPlayer == player.whoAmI)
                {
                    Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.Play(player), player.Bottom.X, player.Bottom.Y - 2, 0f, 0f, ModContent.ProjectileType<SlimeBoosterTrail>(), 10, 0f, Main.myPlayer)];
                    proj.timeLeft = timeleft;
                    if (player.gravDir == -1) proj.position.Y -= player.height;
                }
            }
        }
    }
}