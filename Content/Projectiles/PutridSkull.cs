using ElementsAwoken.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles
{
    public class PutridSkull : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Putrid Skull");
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);
            Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 46,0,0,150)];
            dust.velocity *= 0.1f;
            dust.scale *= 1.5f;
            dust.noGravity = true;

            float rotateIntensity = 0.55f;
            float waveTime = 60f;
            Projectile.ai[0]++;
            if (Projectile.ai[1] == 0)
            {
                if (Projectile.ai[0] > waveTime * 0.5f)
                {
                    Projectile.ai[0] = 0;
                    Projectile.ai[1] = 1;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
            }
            else
            {
                if (Projectile.ai[0] <= waveTime)
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
                if (Projectile.ai[0] >= waveTime * 2)
                {
                    Projectile.ai[0] = 0;
                }
            }   
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.Item103.WithPitchOffset(-0.5f), Projectile.position);
            float rotation = MathHelper.ToRadians(360);
            float numDust = 30;
            for (int i = 0; i < numDust; i++)
            {
                Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numDust - 1))) * 4;
                Dust.NewDustPerfect(target.Center, DustType<PutridDust>(), perturbedSpeed).customData = target.Center;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 16; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 46, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 150, default(Color), 1.8f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }
        }
    }
}