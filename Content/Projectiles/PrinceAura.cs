using ElementsAwoken.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PrinceAura : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 2;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.timeLeft = 60;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Projectile.ai[0]--;
            if (Projectile.ai[0] <= 0)
            {
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + Main.rand.Next(-Projectile.width / 2, Projectile.width / 2), Projectile.position.Y, 0f, 9f, ModContent.ProjectileType<PrinceRain>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
                Projectile.ai[0] = 8;
            }
            
            if (Projectile.localAI[0] == 0) Projectile.ai[1] += 3;
            else Projectile.ai[1] -= 3;
            if (Projectile.ai[1] >= Projectile.width) Projectile.localAI[0]++;
            if (Projectile.ai[1] <= 0) Projectile.localAI[0] = 0;

            int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
            for (int i = 0; i < dustLength; i++)
            {
                float Y = (float)Math.Sin(Projectile.ai[1] / 5) * 10;
                Vector2 dustPos = new Vector2(Projectile.ai[1], Y);

                Dust dust = Main.dust[Dust.NewDust(Projectile.position + dustPos, 2, 2, ModContent.DustType<PrinceDust>())];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / dustLength * (float)i;
                dust.noGravity = true;
                dust.alpha = Projectile.alpha;
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<PrinceAura>()] > 5)
            {
                Projectile.Kill();
            }
        }
    }
}