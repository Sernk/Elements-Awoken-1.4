using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class TheSilencerP : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 220;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 2f)
			{
                int dustLength = 10;
                for (int i = 0; i < dustLength; i++)
                {
                    int dustType = 135;
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            dustType = 135;
                            break;
                        case 1:
                            dustType = 242;
                            break;
                        default: break;
                    }
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType)];
                    dust.velocity = Vector2.Zero;
                    dust.position -= Projectile.velocity / dustLength * (float)i;
                    dust.noGravity = true;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 135, 242 }, damageType: "melee");
        }
    }
}