using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BabySharkP : ModProjectile
    {
        public int[] canHit = new int[Main.maxNPCs];

        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.aiStyle = 39;
            AIType = 190;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Ranged;
        }
        public override void AI()
        {
            for (int i = 0; i < canHit.Length; i++)
            {
                 canHit[i]--;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (canHit[target.whoAmI] > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            canHit[target.whoAmI] = 30;
        }
    }
}