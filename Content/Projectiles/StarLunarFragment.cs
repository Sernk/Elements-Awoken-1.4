using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class StarLunarFragment : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.light = 1.5f;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] <= 10)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            Projectile.ai[1]++;

            int dustID = 6;
            if (Projectile.ai[0] == 1) dustID = 242;
            else if (Projectile.ai[0] == 2) dustID = 197;
            else if (Projectile.ai[0] == 3) dustID = 229;

            for (int i = 0; i < 3; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustID, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 1.2f)];
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.noGravity = true;
                dust.velocity *= 0.5f;
            }

            Projectile.velocity.Y += 0.04f;
        }
    }
}