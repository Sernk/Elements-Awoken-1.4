using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.UI.Tooltips;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.zVanilla.Awakened
{
    public class FadedCloth : ModItem
    {
        public int shootTimer = 120;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ModContent.RarityType<EARarity.Awakened>();
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<EARaritySettings>().awakened = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.fadedCloth = true;
            shootTimer--;
            float maxDistance = 500f;
            if (shootTimer <= 0)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int l = 0; l < Main.npc.Length; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.CanBeChasedBy(player) && Vector2.Distance(player.Center, nPC.Center) <= maxDistance && Collision.CanHit(player.Center, 2, 2, nPC.Center, 2, 2))
                        {
                            float Speed = 12f;
                            float rotation = (float)Math.Atan2(player.Center.Y - nPC.Center.Y, player.Center.X - nPC.Center.X);
                            Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                            Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<SkeleSkull>(), 40, 0f, Main.myPlayer, 0f, 0f);
                            shootTimer = Main.rand.Next(60,180);
                            return;
                        }
                    }
                }
            }
        }
    }
}