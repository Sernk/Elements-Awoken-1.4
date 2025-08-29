using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Spears
{
    public class SkyScraperP : ModProjectile
    {
        protected virtual float HoldoutRangeMin => 86;
        protected virtual float HoldoutRangeMax => 170;

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
    }
}