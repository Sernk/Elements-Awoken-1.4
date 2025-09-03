using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class HarvesterScythe : ModProjectile
    {
        int healCD = 0;
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 180;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 3;
            if (healCD <= 0)
            {
                Main.player[Projectile.owner].statLife += 2;
                Main.player[Projectile.owner].HealEffect(2);
                healCD = 8;
            }
        }
        public override void AI()
        {
            for (int l = 0; l < 2; l++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 74, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 1f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= Main.rand.NextFloat(0.5f, 0.9f);
                Main.dust[dust].noGravity = true;
            }

            Lighting.AddLight(Projectile.Center, 0.3f, 0.9f, 0.6f);
            Projectile.rotation += 0.6f;
            Projectile.velocity *= 0.99f;
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 45)
            {
                Projectile.alpha += 5;
            }
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
    }
}