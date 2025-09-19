using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
	public class StrangePlantBall3 : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = 0;
            Projectile.alpha = 255;
			Projectile.timeLeft = 150;
			Projectile.friendly = true;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Magic;          
			Projectile.penetrate = 2;
		}
        public override void AI()
        {
            for (int num121 = 0; num121 < 3; num121++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 56);
                Main.dust[dust].velocity *= 0.1f;
                if (Projectile.velocity == Vector2.Zero)
                {
                    Main.dust[dust].velocity.Y -= 1f;
                    Main.dust[dust].scale = 1.2f;
                }
                else
                {
                    Main.dust[dust].velocity += Projectile.velocity * 0.2f;
                }
                Main.dust[dust].position.X = Projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
                Main.dust[dust].position.Y = Projectile.Center.Y + (float)Main.rand.Next(-2, 3);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.Bubble, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ProjectileID.Bubble, Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
            }
            Projectile.Kill();
            return false;
        }
    }
}