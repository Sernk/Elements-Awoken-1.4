using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.ScourgeFighter
{
    public class Napalm : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 22;
            Projectile.aiStyle = 14;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 360;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.4f, 0.8f);

            if (Projectile.velocity.X != Projectile.velocity.X)
            {
                Projectile.velocity.X = Projectile.velocity.X * -0.1f;
            }
            if (Projectile.velocity.X != Projectile.velocity.X)
            {
                Projectile.velocity.X = Projectile.velocity.X * -0.25f;
            }
            if (Projectile.velocity.Y != Projectile.velocity.Y && Projectile.velocity.Y > 1f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y * -0.25f;
            }

            if (Main.rand.NextBool(10))
            {
                int num202 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame, 0f, 0f, 100, default(Color), 1f);
                Dust var_2_88D1_cp_0_cp_0 = Main.dust[num202];
                var_2_88D1_cp_0_cp_0.position.X = var_2_88D1_cp_0_cp_0.position.X - 2f;
                Dust var_2_88EE_cp_0_cp_0 = Main.dust[num202];
                var_2_88EE_cp_0_cp_0.position.Y = var_2_88EE_cp_0_cp_0.position.Y + 2f;
                Dust dust = Main.dust[num202];
                dust.scale += (float)Main.rand.Next(50) * 0.01f;
                Main.dust[num202].noGravity = true;
                Dust var_2_8945_cp_0_cp_0 = Main.dust[num202];
                var_2_8945_cp_0_cp_0.velocity.Y = var_2_8945_cp_0_cp_0.velocity.Y - 2f;
                if (Main.rand.Next(2) == 0)
                {
                    int num203 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame, 0f, 0f, 100, default(Color), 1f);
                    Dust var_2_89A6_cp_0_cp_0 = Main.dust[num203];
                    var_2_89A6_cp_0_cp_0.position.X = var_2_89A6_cp_0_cp_0.position.X - 2f;
                    Dust var_2_89C3_cp_0_cp_0 = Main.dust[num203];
                    var_2_89C3_cp_0_cp_0.position.Y = var_2_89C3_cp_0_cp_0.position.Y + 2f;
                    dust = Main.dust[num203];
                    dust.scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
                    Main.dust[num203].noGravity = true;
                    dust = Main.dust[num203];
                    dust.velocity *= 0.1f;
                }
                if ((double)Projectile.velocity.Y < 0.25 && (double)Projectile.velocity.Y > 0.15)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.8f;
                }
            }
            Projectile.rotation = -Projectile.velocity.X * 0.05f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 5)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}