using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class Skysand : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 1;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0]++;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] != 0) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            int numDusts = Projectile.ai[0] != 0 ? 1 : 2;
        	for (int i = 0; i < numDusts; i++)
			{
				Dust dust4 = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 138, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f)];
				dust4.velocity = Vector2.Zero;
				dust4.position -= Projectile.velocity / numDusts * (float)i;
				dust4.noGravity = true;
				dust4.scale = 0.8f * MathHelper.Lerp(0.9f,0.3f,i/numDusts);
			}
            if (Projectile.ai[0] != 0) Projectile.ai[1]++;
            if (Projectile.ai[1] > 10) Projectile.Kill();
        }
    }
}