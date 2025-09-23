using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
	public class SuperStinkPotion : ModProjectile
	{
		public override void SetDefaults()
		{
            Projectile.CloneDefaults(ProjectileID.ToxicFlask);
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
		}
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Super Stink Potion");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			else
			{
				Projectile.ai[0] += 0.1f;
				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
				Projectile.velocity *= 0.5f;
				SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            }

            return false;
		}
		public override void OnKill(int timeLeft)
		{
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2(Main.rand.Next(-12, 12), Main.rand.Next(-12, 12));
                value15.X *= 0.1f;
                value15.Y *= 0.1f;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, value15.X, value15.Y, 228, Projectile.damage / 2, 2f, Projectile.owner, 0f, 0f);
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Projectile.ai[0] += 0.2f;
			Projectile.velocity *= 0.6f;
		}
	}
}