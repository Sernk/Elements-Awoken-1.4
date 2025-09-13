using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PutridTrail : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Putrid Trail");
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.05f;
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.width, 46, 0f, 0f, 150, default(Color), 0.75f)];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            Projectile.ai[1]++;
            if (Projectile.ai[1] == 6) Projectile.tileCollide = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}