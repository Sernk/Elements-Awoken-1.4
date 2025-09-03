using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CarapaceBall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Carapace Ball");
        }
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.08f;
            Projectile.velocity.Y += 0.16f;

            if (Projectile.timeLeft < 120)
            {
                Projectile.velocity.X *= 0.98f;
                Projectile.alpha += 255 / 120;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
            else
            {
                Projectile.velocity.X *= 0.995f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.Item70, Projectile.Center);
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 4; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 37, Projectile.oldVelocity.X * 0.25f, Projectile.oldVelocity.Y * 0.25f);
            }
        }
    }
}