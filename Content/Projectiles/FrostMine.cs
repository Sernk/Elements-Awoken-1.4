using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class FrostMine : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 220;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 135, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 135, damageType: "magic");
        }
    }
}