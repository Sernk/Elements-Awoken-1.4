using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ArmageddonBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.penetrate = 1;
            Projectile.light = 0.5f;
            Projectile.alpha = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void AI()
        {        
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, GetDustID());
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frame = (int)Projectile.ai[0];
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 0) target.AddBuff(BuffID.CursedInferno, 200);
            else if (Projectile.ai[0] == 1) target.AddBuff(BuffID.Frostburn, 200);
            else target.AddBuff(BuffID.OnFire, 200); ;
        }
        private int GetDustID()
        {
            if (Projectile.ai[0] == 0) return 75;
            else if (Projectile.ai[0] == 1) return 135;
            else return 6;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { GetDustID() }, Projectile.damage);
        }
    }
}