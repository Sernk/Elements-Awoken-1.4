using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class GelticArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            AIType = ProjectileID.WoodenArrowFriendly;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.Poisoned, 200);
            }
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.Slow, 200);
            }
        }
    }
}