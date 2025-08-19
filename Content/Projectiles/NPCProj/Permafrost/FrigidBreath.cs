using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Permafrost
{
    public class FrigidBreath : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.scale *= 1.3f;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }
        public override void OnHitPlayer(Player player, Player.HurtInfo info)
        {
            player.AddBuff(BuffID.Frostburn, 180, false);
            player.AddBuff(BuffID.Chilled, 180, false);
        }
        public override void AI()
        {
            Projectile.velocity *= 0.99f;
            if (Projectile.ai[0] == 0)
            {
                Projectile.scale = 0.3f;
                Projectile.ai[0]++;
            }
            float maxScale = 1.3f;
            if (Projectile.scale < maxScale) Projectile.scale += maxScale / 20f;
            else Projectile.scale = maxScale;
            if (Projectile.ai[1] == 1)
            {
                Projectile.alpha += 255 / 10;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
            if (Main.rand.NextBool(5))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 135);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].velocity *= 1f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter % 9 == 0)
            {
                Projectile.frame++;
                if (Projectile.frame > 6)
                {
                    Projectile.frame = 6;
                    Projectile.ai[1] = 1;
                }
            }
            return true;
        }
    }
}