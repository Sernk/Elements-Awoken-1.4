using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ChargeRifleHeld : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            float chargeLevel2 = 20f;
            float chargeLevel3 = 60f;


            Projectile.ai[0] += 1f;
            int num16 = 0;
            if (Projectile.ai[0] >= chargeLevel2)
            {
                num16++;
            }

            if (Projectile.soundDelay <= 0)
            {
                Projectile.soundDelay = 10 - num16;
                Projectile.soundDelay *= 2;
                if (Projectile.ai[0] != 1f)
                {
                    SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
                }
            }
            if (Projectile.ai[0] > 10f)
            {
                Vector2 vector13 = Vector2.UnitX * 18f;
                vector13 = vector13.RotatedBy((double)(Projectile.rotation - 1.57079637f), default(Vector2));
                Vector2 value5 = Projectile.Center + vector13;
                for (int k = 0; k < num16 + 1; k++)
                {
                    float num19 = 0.4f;
                    if (k % 2 == 1)
                    {
                        num19 = 0.65f;
                    }
                    Vector2 vector14 = value5 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (12f - (float)(num16 * 2));
                    int num20 = Dust.NewDust(vector14 - Vector2.One * 8f, 16, 16, 220, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 0, default(Color), 1f);
                    Main.dust[num20].velocity = Vector2.Normalize(value5 - vector14) * 1.5f * (10f - (float)num16 * 2f) / 10f;
                    Main.dust[num20].noGravity = true;
                    Main.dust[num20].scale = num19;
                    Main.dust[num20].customData = player;
                }
            }
            if (Main.myPlayer == Projectile.owner)
            {
                if (player.channel && !player.noItems && !player.CCed)
                {
                    if (Projectile.ai[0] >= chargeLevel3)
                    {
                        Vector2 vector16 = Vector2.Normalize(Projectile.velocity) * 10f;
                        if (float.IsNaN(vector16.X) || float.IsNaN(vector16.Y))
                        {
                            vector16 = -Vector2.UnitY;
                        }
                        int damage = (int)(Projectile.damage * 5f);
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, vector16.X, vector16.Y, ModContent.ProjectileType<ChargeRifleFull>(), damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item92, Projectile.position);
                        Projectile.Kill();
                    }
                }
                else
                {
                    if (Projectile.ai[0] < chargeLevel3)
                    {
                        Vector2 vector16 = Vector2.Normalize(Projectile.velocity) * 10f;
                        if (float.IsNaN(vector16.X) || float.IsNaN(vector16.Y))
                        {
                            vector16 = -Vector2.UnitY;
                        }
                        int projType = ModContent.ProjectileType<ChargeRifleHalf>();
                        SoundStyle sound = SoundID.Item11;
                        if (Projectile.ai[0] < chargeLevel2)
                        {
                            projType = (int)Projectile.ai[1];
                            sound = SoundID.Item11;
                        }
                        else
                        {
                            projType = ModContent.ProjectileType<ChargeRifleHalf>();
                            sound = SoundID.Item91;
                        }
                        int damage = (int)((Projectile.damage - 38) + Projectile.ai[0] * 2f);
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, vector16.X, vector16.Y, projType, damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                        SoundEngine.PlaySound(sound, Projectile.position);
                    }
                    Projectile.Kill();
                }
                Vector2 thing = Projectile.velocity;
                thing.Normalize();
                thing *= 20f;
                Vector2 yAdd = new Vector2(0, 0);
                if (player.direction == 1)
                {
                    yAdd.Y = 10;
                }
                Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f + thing.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2)) - yAdd;
                Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
                Projectile.spriteDirection = Projectile.direction;
                Projectile.timeLeft = 2;
                player.ChangeDir(Projectile.direction);
                player.heldProj = Projectile.whoAmI;
                player.itemTime = 2;
                player.itemAnimation = 2;
                player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));

                float scaleFactor = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                Vector2 vector3 = vector;
                Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector3;
                if (player.gravDir == -1f)
                {
                    value2.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector3.Y;
                }
                Vector2 vector4 = Vector2.Normalize(value2);
                if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
                {
                    vector4 = -Vector2.UnitY;
                }
                vector4 *= scaleFactor;
                if (vector4.X != Projectile.velocity.X || vector4.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity = vector4;
            }            
        }
    }
}