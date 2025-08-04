using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class SlimeBoosterTrail : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
            Projectile.light = 1f;
            Projectile.extraUpdates = 1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }
        public override void AI()
        {
            Dust dust = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 4, 0, 0, 150, new Color(0, 80, 255, 100), 2f)];
            dust.velocity.X *= 0.6f;
            dust.velocity.Y *= 0.3f;
            dust.scale *= 0.6f;
            dust.noGravity = true;
        }
    }
}