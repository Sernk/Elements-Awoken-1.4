using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class EquinoxSwirl : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
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
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(12, 0);
            Projectile parent = Main.projectile[(int)Projectile.ai[1]];
            Projectile.ai[0] += 0.1f;
            Projectile.position = parent.position + offset.RotatedBy(Projectile.ai[0] + Projectile.ai[1] * (Math.PI * 2 / 8));

            if (parent.active == false)
            {
                Projectile.Kill();
            }
            if (Main.rand.Next(2) == 0)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 242, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
                Main.dust[dust].velocity *= 0.6f;
                Main.dust[dust].scale *= 0.6f;
                Main.dust[dust].noGravity = true;
                int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, default(Color), 3.75f);
                Main.dust[dust2].velocity *= 0.6f;
                Main.dust[dust2].scale *= 0.6f;
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 229, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, default(Color), 1.75f);
                Main.dust[dust3].velocity *= 0.6f;
                Main.dust[dust3].scale *= 0.6f;
                Main.dust[dust3].noGravity = true;
                int dust4 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 197, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 0, default(Color), 1.75f);
                Main.dust[dust4].velocity *= 0.6f;
                Main.dust[dust4].scale *= 0.6f;
                Main.dust[dust4].noGravity = true;
            }
        }
    }
}