using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Manaspike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.4f, 0.2f, 0.4f);
            for (int num121 = 0; num121 < 4; num121++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 234)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)num121;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            Projectile.velocity.Y += 0.1f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            int numberProjectiles = Main.rand.Next(2,5);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center,-Projectile.oldVelocity.RotatedByRandom(MathHelper.ToRadians(90)) * 0.5f, ModContent.ProjectileType<Manashatter>(), Projectile.damage / 2, 2f, Projectile.owner, 0f, 0f);
            }
        }
    }
}