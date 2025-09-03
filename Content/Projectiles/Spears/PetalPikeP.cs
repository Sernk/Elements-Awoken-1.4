using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Spears
{
    public class PetalPikeP : ModProjectile
    {
        protected virtual float HoldoutRangeMin => 50f;
        protected virtual float HoldoutRangeMax => 120f;
        readonly float shootSpeed = 10f;

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.CloneDefaults(ProjectileID.Spear);
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            int duration = player.itemAnimationMax;

            player.heldProj = Projectile.whoAmI;

            if (player.itemAnimation < player.itemAnimationMax / 3)
            {
                Projectile.ai[0] -= 1.1f;
                if (Projectile.localAI[0] == 0f)
                {
                    Projectile.localAI[0] = 1f;
                    float count = 25.0f;
                    int type = ModContent.ProjectileType<PetalPikeStem>();
                    for (int k = 0; (double)k < (double)count; k++)
                    {
                        Vector2 vector2 = (Vector2.UnitX * 0.0f + -Vector2.UnitY.RotatedBy((double)k * (6.22 / (double)count), new Vector2()) * new Vector2(2.0f, 8.0f)).RotatedBy((double)Projectile.velocity.ToRotation(), new Vector2());
                    }
                    if (player.itemAnimation < player.itemAnimationMax / 2)
                    {
                        float numberProjectiles = 3;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            float rotation = MathHelper.ToRadians(18);
                            Vector2 projPos = Projectile.position + Vector2.Normalize(new Vector2(Projectile.velocity.X, Projectile.velocity.Y)) * 2;
                            Vector2 perturbedSpeed = Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * shootSpeed;
                            Projectile.NewProjectile(EAU.Proj(Projectile), projPos.X, projPos.Y, perturbedSpeed.X, perturbedSpeed.Y, type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                        }
                    }
                }
            }
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