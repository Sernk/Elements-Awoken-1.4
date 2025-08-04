using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CelestialInfernoSpin : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000;
        }
        public override void AI()
        {
            Vector2 offset = new Vector2(100, 0);
            int dustType = 6;
            switch ((int)Projectile.ai[1])
            {
                case 0:
                    dustType = 6;
                    offset = new Vector2(100, 0);
                    break;
                case 1:
                    dustType = 197;
                    offset = new Vector2(125, 0);
                    break;
                case 2:
                    dustType = 229;
                    offset = new Vector2(150, 0);
                    break;
                case 3:
                    dustType = 242;
                    offset = new Vector2(175, 0);
                    break;
                default: break;
            }
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0] += 0.1f;
            Projectile.Center = player.Center + offset.RotatedBy(Projectile.ai[0] * (Math.PI * 2 / 8));
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }
}