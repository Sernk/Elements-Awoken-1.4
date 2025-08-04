using ElementsAwoken.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Items.BossDrops.TheCelestial
{
    public class CelestialFlame : ModItem
    {
        public int shootTimer = 20;
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 7, 50, 0);
            Item.rare = 6;
            Item.expert = true;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Melee) += 10;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.GetCritChance(DamageClass.Throwing) += 10;
            player.GetCritChance(DamageClass.Magic) += 10;

            shootTimer--;
            float maxDistance = 500f;
            if (player.whoAmI == Main.myPlayer)
            {
                if (shootTimer <= 0)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPC = Main.npc[l];
                        if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Collision.CanHit(player.position, player.width, player.height, nPC.position, nPC.width, nPC.height) &&Vector2.Distance(player.Center, nPC.Center) <= maxDistance)
                        {
                            float projSpeed = 15f; //modify the speed the projectile are shot.  Lower number = slower projectile.
                            float speedX = nPC.Center.X - player.Center.X;
                            float speedY = nPC.Center.Y - player.Center.Y;
                            float num406 = (float)Math.Sqrt((double)(speedX * speedX + speedY * speedY));
                            num406 = projSpeed / num406;
                            speedX *= num406;
                            speedY *= num406;

                            SoundEngine.PlaySound(SoundID.Item12, player.position);
                            Projectile.NewProjectile(EAU.Play(player), player.Center.X, player.Center.Y, speedX, speedY, ModContent.ProjectileType<CelestialBoltFriendly>(), 35, 0f, Main.myPlayer, 0f, Main.rand.Next(4));
                            shootTimer = Main.rand.Next(20,40);
                            return;
                        }
                    }
                }
            }
        }
    }
}