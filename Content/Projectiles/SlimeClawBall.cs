using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SlimeClawBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.1f;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slime Ball");
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.05f;

            Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 4, Projectile.velocity.X * 0.3f, Projectile.velocity.Y * 0.3f, 150, new Color(0, 220, 40, 100), 2f)];
            dust.velocity *= 0.6f;
            dust.scale *= 0.6f;
            dust.noGravity = true;
        }
    }
}