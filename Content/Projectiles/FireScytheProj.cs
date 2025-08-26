using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class FireScytheProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 58;
            Projectile.light = 0.5f;
            Projectile.penetrate = -1;
            Projectile.scale = 0.9f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0]++;
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.alpha != 0) return false;
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            Projectile.rotation += 0.5f;
            Projectile.velocity *= 0.97f;
            Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch)];
            dust.noGravity = true;
            Projectile.ai[1]++;
            if (Projectile.ai[0] > 5 || Projectile.ai[1] > 120) Projectile.alpha += 255 / 30;
            if (Projectile.alpha >= 255) Projectile.Kill();

            int hitboxSize = Projectile.width / 2;
            if (Collision.SolidCollision(Projectile.Center - new Vector2(hitboxSize / 2, hitboxSize / 2), hitboxSize, hitboxSize))
            {
                Projectile.ai[0] = 10;
            }
        }
    }
}