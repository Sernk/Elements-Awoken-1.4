using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Krecheus
{
    public class KrecheusPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 450;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AncientRed>(), 0f, 0f, 100, default(Color), 1f);
            Main.dust[dust].velocity *= 0f;
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale *= 0.5f;

            Player player = Main.player[Main.myPlayer];
            Projectile.localAI[1]--;
            if (Projectile.localAI[1] <= 0)
            {
                float Speed = 12f;
                float rotation = (float)Math.Atan2(Projectile.Center.Y - player.Center.Y, Projectile.Center.X - player.Center.X);
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<KrecheusSpike>(), Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                Projectile.localAI[1] = Main.rand.Next(8, 12);
            }
        }        
    }
}