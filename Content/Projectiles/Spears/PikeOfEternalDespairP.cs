using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Spears
{
    public class PikeOfEternalDespairP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.CloneDefaults(ProjectileID.Spear);
        }
        protected virtual float HoldoutRangeMin => 84f;
        protected virtual float HoldoutRangeMax => 160;

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
                    for (int k = 0; (double)k < (double)count; k++)
                    {
                        Vector2 vector2 = (Vector2.UnitX * 0.0f + -Vector2.UnitY.RotatedBy((double)k * (6.22 / (double)count), new Vector2()) * new Vector2(2.0f, 8.0f)).RotatedBy((double)Projectile.velocity.ToRotation(), new Vector2());
                        int dust = Dust.NewDust(Projectile.Center, 0, 0, Const.PinkFlame, 0.0f, 0.0f, 0, new Color(), 1.0f);
                        Main.dust[dust].scale = 1.25f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Projectile.Center + vector2;
                        Main.dust[dust].velocity = Projectile.velocity * 0.0f + vector2.SafeNormalize(Vector2.UnitY) * 1.0f;
                    }
                    if (player.itemAnimation < player.itemAnimationMax / 2) Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X + Projectile.velocity.X, Projectile.Center.Y + Projectile.velocity.Y, Projectile.velocity.X * 14.0f, Projectile.velocity.Y * 15.0f, ModContent.ProjectileType<DespairBody>(), Projectile.damage, Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
                }
            }
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Const.PinkFlame, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 200, new Color(), 1.2f);
                Main.dust[dust].velocity += Projectile.velocity * 0.3f;
                Main.dust[dust].velocity *= 0.2f;
            }
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Const.PinkFlame, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 200, new Color(), 1.2f);
                Main.dust[dust].velocity += Projectile.velocity * 0.3f;
                Main.dust[dust].velocity *= 0.2f;
                Main.dust[dust].noGravity = true;
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 4;
        }
    }
}