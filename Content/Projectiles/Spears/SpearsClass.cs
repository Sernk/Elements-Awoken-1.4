using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Spears
{
    public abstract class SpearsClass : ModProjectile
    {
        /// <summary>
        /// <para/> If sprite = 62  x  64, HoldoutRangeMin = 33f
        /// <para/> If sprite = 110 x 110, HoldoutRangeMin = 84f
        /// <para/> If sprite = 120 x 120, HoldoutRangeMin = 86f
        /// </summary>
        public abstract float HoldoutRangeMin { get; }

        /// <summary>
        /// <para/> If sprite = 62  x  64, HoldoutRangeMax =  80f
        /// <para/> If sprite = 110 x 110, HoldoutRangeMax = 160f
        /// <para/> If sprite = 120 x 120, HoldoutRangeMax = 170f
        /// </summary>
        public abstract float HoldoutRangeMax { get; }

        /// <summary>
        /// Buffs = base 0.
        /// <para/> What debuff should be applied when striking an enemy
        /// </summary>
        public virtual int Buffs => 0;

        /// <summary>
        /// DebuffDuration = base 180 (3 seconds).
        /// </summary>
        public virtual int DebuffDuration => 180;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spear);
            Projectile.DamageType = DamageClass.Melee;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            int duration = player.itemAnimationMax;

            player.heldProj = Projectile.whoAmI;

            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity);

            float halfDuration = duration * 0.5f;
            float progress;

            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                Projectile.rotation += MathHelper.ToRadians(135f);
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Buffs, DebuffDuration, false);
        }
    }
}
