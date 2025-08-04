using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class PincerArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.scale = 1f;
            Projectile.penetrate = 2;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 200);
            target.AddBuff(BuffID.Venom, 200);
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0 && Projectile.penetrate == 1)
            {
                Projectile.damage = (int)(Projectile.damage * 0.4f);
                Projectile.ai[1]++;
            }
        }
    }
}