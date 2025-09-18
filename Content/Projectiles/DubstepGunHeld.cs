using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DubstepGunHeld : ModProjectile
    {
        public float shootTimer1 = 0;
        public float shootTimer2 = 0;
        public float shootTimer3 = 0;

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
            SoundEngine.PlaySound(new SoundStyle(EAU.SoundPath("Dubstep")), Projectile.position);
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            Projectile.ai[0] += 1f;
            Projectile.ai[1] += 1f;

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
            shootTimer1--;
            shootTimer2--;
            shootTimer3--;
            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 vector16 = Vector2.Normalize(Projectile.velocity) * 15f;
                if (float.IsNaN(vector16.X) || float.IsNaN(vector16.Y))
                {
                    vector16 = -Vector2.UnitY;
                }
                if (shootTimer1 <= 0)
                {
                    Vector2 speed = vector16.RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DubstepWave>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);                  
                    shootTimer1 = 6;
                }
                if (shootTimer2 <= 0)
                {
                    Vector2 speed = vector16.RotatedByRandom(MathHelper.ToRadians(50));
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DubstepBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    shootTimer2 = 14;
                    PlayerEnergy modPlayer = player.GetModPlayer<PlayerEnergy>();
                    modPlayer.energy -= 3;
                }
                if (shootTimer3 <= 0)
                {
                    Vector2 speed = vector16;
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DubstepPulse>(), (int)(Projectile.damage * 1.75), 20f, Projectile.owner, 0f, 0f);
                    shootTimer3 = 60;
                }
                if (!player.channel)
                {
                    Projectile.Kill();
                }
            }
            Vector2 thing = Projectile.velocity;
            thing.Normalize(); // makes the value = 1
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