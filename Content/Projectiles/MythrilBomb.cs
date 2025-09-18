using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class MythrilBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.2f;
            Projectile.velocity.Y += 0.2f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 5) Projectile.Kill();
            else
            {
                if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X;
                if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = -oldVelocity.Y;
                Projectile.velocity *= 0.5f;
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            int numProj = 5;
            for (int i = 0; i < numProj; i++)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.NextFloat(-5,5), Main.rand.NextFloat(-5, 5), ModContent.ProjectileType<MythrilSpike>(), (int)(Projectile.damage * 0.5f), 0f, Projectile.owner, 0f, 0f);
            }
        }
    }
}