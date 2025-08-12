using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Infernace
{
    public class InfernalMonolith : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 80;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.hostile = true;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] < 15)
            {
                Projectile.position.Y -= 80 / 15;
            }
            if (Projectile.localAI[0] > 120)
            {
                Projectile.position.Y += 80 / 30;
            }
            if (Projectile.localAI[0] > 150)
            {
                Projectile.Kill();
            }        
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindProjectiles.Add(index);
           // drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Projectile.localAI[0] < 20)
            {
                target.velocity.Y -= 30;
                target.AddBuff(BuffID.OnFire, 300);
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust fire = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f)];
                fire.noGravity = true;
            }
            for (int k = 0; k < 20; k++)
            {
                Dust stone = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 232)];
            }
        }
    }
}