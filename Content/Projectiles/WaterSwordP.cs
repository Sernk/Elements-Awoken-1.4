using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class WaterSwordP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Arkhalis);
            AIType = ProjectileID.Arkhalis;
            Projectile.width = 68;
            Projectile.height = 62;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 28;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);

            float num = 0f;
            if (Projectile.spriteDirection == -1)
            {
                num = 3.14159274f;
            }
            int num29 = Projectile.frame + 1;
            Projectile.frame = num29;
            if (num29 >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
            Projectile.soundDelay--;
            if (Projectile.soundDelay <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
                Projectile.soundDelay = 12;
            }
            if (Main.myPlayer == Projectile.owner)
            {
                if (player.channel && !player.noItems && !player.CCed)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                    }
                    Vector2 vector20 = Main.MouseWorld - vector;
                    vector20.Normalize();
                    if (vector20.HasNaNs())
                    {
                        vector20 = Vector2.UnitX * (float)player.direction;
                    }
                    vector20 *= scaleFactor6;
                    if (vector20.X != Projectile.velocity.X || vector20.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector20;
                }
                else
                {
                    Projectile.Kill();
                }
            }
            Vector2 vector21 = Projectile.Center + Projectile.velocity * 3f;
            Lighting.AddLight(vector21, 0.8f, 0.8f, 0.8f);
            if (Main.rand.Next(3) == 0)
            {
                int num31 = Dust.NewDust(vector21 - Projectile.Size / 2f, Projectile.width, Projectile.height, 59, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 2f);
                Main.dust[num31].noGravity = true;
                Main.dust[num31].position -= Projectile.velocity;
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 2;
        }
    }
}