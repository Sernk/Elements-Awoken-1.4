using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousHomingBall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 150;
        }
        public override void AI()
        {
            Projectile.localAI[1] += 1f;

            if (Projectile.localAI[1] <= 45f)
            {
                if (Projectile.localAI[0] >= 1 && Projectile.localAI[0] < 200)
                {
                    double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - Projectile.position.Y, Main.player[Main.myPlayer].position.X - Projectile.position.X);
                    Projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 7f;
                }
            }
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 4f)
            {
                for (int i = 0; i < 5; i++)
                {
                    int dust1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 62, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].velocity *= 0f;
                }
                for (int i = 0; i < 2; i++)
                {
                    int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, EAU.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].velocity *= 0f;
                }
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            return false;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.HostileExplosion(Projectile, 62, Projectile.damage, soundID: 103);
        }
    }
}