using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousHandTrail : ModProjectile
    {	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 60;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Projectile.localAI[0] += 1f;

            if (Projectile.localAI[0] > 3f)
            {
                if (Projectile.ai[0] == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[dust].velocity *= 0f;
                        Main.dust[dust].noGravity = true;
                    }
                    if (Main.rand.Next(100) == 0)
                    {
                        Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<GreekFireHostile>(), Projectile.damage, 1, Main.myPlayer);
                    }
                }
                if (Projectile.ai[0] == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[dust].velocity *= 0f;
                        Main.dust[dust].noGravity = true;
                    }
                }
                if (Projectile.ai[0] == 2)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 135, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[dust].velocity *= 0f;
                        Main.dust[dust].noGravity = true;
                    }
                }
                if (Projectile.ai[0] == 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust1].noGravity = true;
                        Main.dust[dust1].velocity *= 0f;
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, EAU.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                        Main.dust[dust2].noGravity = true;
                        Main.dust[dust2].velocity *= 0f;
                    }
                }
            }
        }
    }
}