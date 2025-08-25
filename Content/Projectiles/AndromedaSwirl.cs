using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class AndromedaSwirl : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public float shrink = 150;
        public float dustAI = 20;
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 100;
            Projectile.extraUpdates = 2;
        }
        public override void AI()
        {
            if (shrink > 0f)
            {
                shrink -= 4f;
            }
            Vector2 offset = new Vector2(shrink, 0);
            Projectile parent = Main.projectile[(int)Projectile.ai[1]];
            Projectile.ai[0] += 0.05f;
            Projectile.Center = parent.Center + offset.RotatedBy(Projectile.ai[0] + Projectile.ai[1] * (Math.PI * 2 / 8));

            if (shrink <= 0)
            {
                Projectile.Kill();
            }
            dustAI--;
            if (dustAI > 15)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 4.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            if (dustAI <= 15 && dustAI > 10)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 197, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 2.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            if (dustAI <= 10 && dustAI > 5)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 242, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
            if (dustAI <= 5 && dustAI > 0)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 229, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 2.75f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
            }
        }
    }
}