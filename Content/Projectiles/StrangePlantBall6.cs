using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class StrangePlantBall6 : ModProjectile
    {
        public override string Texture { get { return EAU.ProjTexture; } }
        int spawnProj = 10;
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
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 63, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
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
            //projectile.velocity.Y += 0.05f;
            spawnProj--;
            if (spawnProj <= 0)
            {
                int type = 0;
                switch (Main.rand.Next(3))
                {
                    case 0: type = ModContent.ProjectileType<StrangePlantBall2>(); break;
                    case 1: type = ModContent.ProjectileType<StrangePlantBall3>(); break;
                    case 2: type = ModContent.ProjectileType<StrangePlantBall5>(); break;
                }
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 6f, type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                spawnProj = 18 + Main.rand.Next(6);
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 62, damageType: "magic");
            int numberProjectiles = 1;
            for (int num252 = 0; num252 < numberProjectiles; num252++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                while (value15.X == 0f && value15.Y == 0f)
                {
                    value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                }
                value15.Normalize();
                value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
                int num1 = Projectile.damage / 2;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.oldPosition.X + (float)(Projectile.width / 2), Projectile.oldPosition.Y + (float)(Projectile.height / 2), value15.X, value15.Y, ModContent.ProjectileType<StrangePlantBall1>(), num1, 0f, Projectile.owner, 0f, 0f);
            }
        }
    }
}