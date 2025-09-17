using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class EscapedChlorobyte : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chlorobyte");
        }
        public override void AI()
        {
            Player target = Main.player[(int)Projectile.ai[0]];

            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 45)
            {
                double angle = Math.Atan2(target.Center.Y - Projectile.Center.Y, target.Center.X - Projectile.Center.X);
                Projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 2f;

                if (Projectile.Hitbox.Intersects(new Rectangle((int)target.Center.X - 4, (int)target.Center.Y - 4, 8, 8)))
                {
                    Projectile.Kill();
                    target.AddBuff(BuffID.Poisoned, 120);
                }
            }
            else
            {
                Projectile.velocity *= 0.98f;
            }
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 75)];
                dust.velocity *= 0.6f;
                dust.position -= Projectile.velocity / 8f * (float)i;
                dust.noGravity = true;
                dust.scale = 0.8f;
            }
            if (Vector2.Distance(target.Center, Projectile.Center) > 200)
            {
                Projectile.Kill();
            }
        }
    }
}
