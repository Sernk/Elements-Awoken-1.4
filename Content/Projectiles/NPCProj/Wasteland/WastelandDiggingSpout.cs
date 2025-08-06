using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Wasteland
{
    public class WastelandDiggingSpout : ModProjectile
    { 	
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 400;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Projectile.velocity *= 0f;
            for (int k = 0; k < 3; k++)
            {
                int dust2 = Dust.NewDust(Projectile.position, Projectile.width, 32, 32, 0f, -16f, 100, default(Color), 1.5f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 1f;
            }
        }
        public override void OnKill(int timeLeft) { }
    }
}