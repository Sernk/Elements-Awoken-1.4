using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Azana
{
    public class AzanaInfectionPillar : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.ai[0] == 0f)
            {
                Projectile.alpha -= (int)Projectile.localAI[1];
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    Projectile.ai[0] = 1f;
                    if (Projectile.ai[1] == 0f)
                    {
                        Projectile.ai[1] += 1f;
                        Projectile.position += Projectile.velocity * 1f;
                    }
                    if (Main.myPlayer == Projectile.owner)
                    {
                        int num47 = Projectile.type;
                        float mult = 1;
                        if (Projectile.ai[1] >= 80f + Main.rand.Next(0,6))
                        {
                            num47 = ModContent.ProjectileType<AzanaInfectionPillarTip>();
                            mult = 1.4f;
                        }
                        int num48 = Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + Projectile.velocity.X * mult, Projectile.Center.Y + Projectile.velocity.Y * mult, Projectile.velocity.X, Projectile.velocity.Y, num47, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, Projectile.ai[1] + 1f);
                        NetMessage.SendData(27, -1, -1, null, num48, 0f, 0f, 0f, 0, 0, 0);
                        Main.projectile[num48].localAI[1] = Projectile.localAI[1];
                        return;
                    }
                }
            }
            else
            {
                if (Projectile.alpha < 170 && Projectile.alpha + 5 >= 170)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127, Projectile.velocity.X * 0.025f, Projectile.velocity.Y * 0.025f)];
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
                Projectile.ai[0]++;
                if (Projectile.ai[0] > 40)
                {
                    Projectile.alpha += 15;
                    if (Projectile.alpha >= 255)
                    {
                        Projectile.Kill();
                        return;
                    }
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 127, Projectile.oldVelocity.X * 0.025f, Projectile.oldVelocity.Y * 0.025f);
            }
        }
    }
}