using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Krecheus
{
    public class KrecheusSide : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;

            int num49 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AncientRed>(), 0f, 0f, 100, default(Color), 1f);
            Main.dust[num49].velocity *= 0.3f;
            Main.dust[num49].fadeIn = 0.9f;
            Main.dust[num49].noGravity = true;
        }
    }
}