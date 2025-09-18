using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CrimsonShadeP : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 2;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crimson Shade");
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.01f) / 255f, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f);
            if (Projectile.ai[0] <= 4f)
            {
                Projectile.ai[1] = Main.rand.Next(4,60);
                Projectile.ai[0] += 1f;
                return;
            }
            Projectile.velocity.Y = Projectile.velocity.Y + 0.075f;
            int num = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
            for (int i = 0; i < num; i++)
            {
                int num155 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 127, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num155].noGravity = true;
                Main.dust[num155].velocity *= 0.1f;
                Main.dust[num155].velocity += Projectile.velocity * 0.5f;
            }
            Projectile.ai[1]--;
            if (Projectile.ai[1] <= 0)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 8f, ModContent.ProjectileType<CrimsonShadeHoming>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.ai[1] = Main.rand.Next(4, 60);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
    }
}