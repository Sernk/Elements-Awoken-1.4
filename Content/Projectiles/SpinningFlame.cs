using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SpinningFlame : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public float shrink = 150;
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 1000;
        }
        public override void AI()
        {
            if (shrink > 0f)
            {
                shrink -= 0.1f;
            }
            Vector2 offset = new Vector2(shrink, 0);
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0] += 0.1f;
            Projectile.Center = player.Center + offset.RotatedBy(Projectile.ai[0] + Projectile.ai[1] * (Math.PI * 2 / 8));
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;
        }
    }
}