using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CursedFlame : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
			if (Projectile.velocity.X != Projectile.velocity.X) Projectile.velocity.X = Projectile.velocity.X * -0.1f;
			if (Projectile.velocity.X != Projectile.velocity.X) Projectile.velocity.X = Projectile.velocity.X * -0.5f;
			if (Projectile.velocity.Y != Projectile.velocity.Y && Projectile.velocity.Y > 1f) Projectile.velocity.Y = Projectile.velocity.Y * -0.5f;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 5f)
			{
				Projectile.ai[0] = 5f;
				if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
				{
					Projectile.velocity.X = Projectile.velocity.X * 0.97f;
					if ((double)Projectile.velocity.X > -0.01 && (double)Projectile.velocity.X < 0.01)
					{
						Projectile.velocity.X = 0f;
						Projectile.netUpdate = true;
					}
				}
				Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
			}
			Projectile.rotation += Projectile.velocity.X * 0.1f;
			if (Projectile.ai[1] == 0f && Projectile.type >= 326 && Projectile.type <= 328)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item13, Projectile.position);
			}
			int num1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
			Dust expr_8976_cp_0 = Main.dust[num1];
			expr_8976_cp_0.position.X = expr_8976_cp_0.position.X - 2f;
			Dust expr_8994_cp_0 = Main.dust[num1];
			expr_8994_cp_0.position.Y = expr_8994_cp_0.position.Y + 2f;
			Main.dust[num1].scale += (float)Main.rand.Next(50) * 0.01f;
			Main.dust[num1].noGravity = true;
			Dust expr_89E7_cp_0 = Main.dust[num1];
			expr_89E7_cp_0.velocity.Y = expr_89E7_cp_0.velocity.Y - 2f;
			if (Main.rand.Next(2) == 0)
			{
				int num2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
                Dust expr_8A4E_cp_0 = Main.dust[num2];
                expr_8A4E_cp_0.position.X = expr_8A4E_cp_0.position.X - 2f;
                Dust expr_8A6C_cp_0 = Main.dust[num2];
                expr_8A6C_cp_0.position.Y = expr_8A6C_cp_0.position.Y + 2f;
                Main.dust[num2].scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 0.1f;
            }
			if ((double)Projectile.velocity.Y < 0.25 && (double)Projectile.velocity.Y > 0.15)
			{
				Projectile.velocity.X = Projectile.velocity.X * 0.8f;
			}
			Projectile.rotation = -Projectile.velocity.X * 0.05f;
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
				return;
			}
        }       
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}