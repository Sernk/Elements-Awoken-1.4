using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class WindfallFire : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 125;
            Projectile.extraUpdates = 3;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.15f) / 255f, ((255 - Projectile.alpha) * 0.45f) / 255f, ((255 - Projectile.alpha) * 0.05f) / 255f);
            if (Projectile.timeLeft > 125)
            {
                Projectile.timeLeft = 125;
            }
            if (Projectile.ai[0] > 12f)
            {
                if (Main.rand.Next(3) == 0)
                {
                    int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 59, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, (Color.Cyan), 3.75f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 2.5f;
                    int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 59, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, (Color.Cyan), 1.5f);
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            return;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numberProjectiles = 1;
            int num1 = Main.rand.Next(-30, 30);
            int num2 = Main.rand.Next(300, 500);
            for (int num131 = 0; num131 < numberProjectiles; num131++)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + num1, Projectile.Center.Y - num2, 0, 20, ModContent.ProjectileType<Feather2>(), Projectile.damage / 2, 0, Projectile.owner);
                }
            }
        }
    }
}