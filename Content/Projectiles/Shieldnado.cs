using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Shieldnado : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 3;
            AIType = 52;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shield of The Desert");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width + 2, Projectile.height + 2, 32, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
            Main.dust[dust].noGravity = true;
            Projectile.rotation = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2)
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