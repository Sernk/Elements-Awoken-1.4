using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DesolationIIIHeld : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 52;

            //projectile.aiStyle = 75;

            Projectile.penetrate = -1;

            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desolation");
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            var s = Projectile.GetSource_FromThis();

            float chargeLevel1 = 20f;
            float chargeLevel2 = 45f;
            float chargeLevel3 = 60f;


            Projectile.ai[0] += 1f;
            Projectile.ai[1] += 1f;
            int num16 = 0;

            if (Projectile.ai[1] >= 10)
            {
                Projectile.ai[1] = 0f;

                float scaleFactor3 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                Vector2 vector11 = vector;
                Vector2 value4 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector11;
                if (player.gravDir == -1f)
                {
                    value4.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector11.Y;
                }
                Vector2 vector12 = Vector2.Normalize(value4);
                if (float.IsNaN(vector12.X) || float.IsNaN(vector12.Y))
                {
                    vector12 = -Vector2.UnitY;
                }
                vector12 *= scaleFactor3;
                if (vector12.X != Projectile.velocity.X || vector12.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity = vector12;

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
                    int num20 = Dust.NewDust(vector14 - Vector2.One * 8f, 16, 16, ModContent.DustType<AncientGreen>(), Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 0, default(Color), 1f);
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
                        Vector2 vector16 = Vector2.Normalize(Projectile.velocity) * 20f;
                        if (float.IsNaN(vector16.X) || float.IsNaN(vector16.Y))
                        {
                            vector16 = -Vector2.UnitY;
                        }
                        int damage = (int)(Projectile.damage * 5f);
                        Projectile.NewProjectile(s, Projectile.Center.X, Projectile.Center.Y, vector16.X, vector16.Y, ModContent.ProjectileType<DesolationCharge4>(), damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                        SoundEngine.PlaySound(SoundID.Item113, Projectile.position);
                        Projectile.Kill();
                    }
                }
                else
                {
                    int projType = ModContent.ProjectileType<DesolationCharge1>();
                    if (Projectile.ai[0] < chargeLevel1)
                    {
                        projType = ModContent.ProjectileType<DesolationCharge1>();
                    }
                    else if (Projectile.ai[0] < chargeLevel2)
                    {
                        projType = ModContent.ProjectileType<DesolationCharge2>();
                    }
                    else if (Projectile.ai[0] < chargeLevel3)
                    {
                        projType = ModContent.ProjectileType<DesolationCharge3>();
                    }
                    Vector2 vector16 = Vector2.Normalize(Projectile.velocity) * 15f;
                    if (float.IsNaN(vector16.X) || float.IsNaN(vector16.Y))
                    {
                        vector16 = -Vector2.UnitY;
                    }
                    int damage = (int)((Projectile.damage - 38) + Projectile.ai[0] * 2f);
                    Projectile.NewProjectile(s, Projectile.Center.X, Projectile.Center.Y, vector16.X, vector16.Y, projType, damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
                    Projectile.Kill();
                }
            }

            Vector2 thing = Projectile.velocity;
            thing.Normalize(); // makes the value = 1
            thing *= 20f;
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f + thing.RotatedBy((double)(MathHelper.Pi / 10), default(Vector2));
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