using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class MiniEaterOfSouls : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.aiStyle = 39;
            Main.projFrames[Projectile.type] = 4;
            AIType = 190;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 12;
        }
        public override void AI()
        {
            if (Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center) >= 800)
            {
                Projectile.Kill();
            }
        }
    }
}