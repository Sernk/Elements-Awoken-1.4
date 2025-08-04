using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    /// <summary>
    /// TODO: No buffs "Bleeding";
    /// </summary>
    public class SpinalEye : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.velocity.Y += 0.05f;
        }
        //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        //{
        //    target.AddBuff(ModContent.BuffType<Bleeding>(), 200);
        //}
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 5, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}