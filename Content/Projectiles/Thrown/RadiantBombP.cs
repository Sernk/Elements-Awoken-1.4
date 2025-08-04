using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class RadiantBombP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
        }
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.05f;
            Projectile.velocity.Y += 0.056f;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item29, Projectile.position);
            float rotation = MathHelper.TwoPi;
            float numProj = 8;
            for (int i = 0; i < numProj; i++)
            {
                Vector2 perturbedSpeed = (rotation / numProj * i).ToRotationVector2() * 6.5f;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<RadiantStarSpiral>(), (int)(Projectile.damage * 0.6f), Projectile.knockBack, Projectile.owner);
            }
        }
    }
}