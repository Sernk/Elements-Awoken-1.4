using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class ToadTongueTip : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Projectile.ai[0] == 0f)
            {
                Projectile.alpha -= 200;
                if (Projectile.alpha <= 0)
                {
                    Projectile.alpha = 0;
                    Projectile.ai[0] = 1f;
                    if (Projectile.ai[1] == 0f)
                    {
                        Projectile.ai[1] += 1f;
                        Projectile.position += Projectile.velocity * 1f;
                    }
                }
            }
            else
            {
                Projectile.ai[0]++;
                if (Projectile.ai[0] > Projectile.localAI[0])
                {
                    Projectile.alpha += 60;
                    if (Projectile.alpha >= 255)
                    {
                        Projectile.Kill();
                        return;
                    }
                }
            }
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Poisoned, 600);
            target.AddBuff(BuffID.Venom, 300);
        }
    }
}