using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class AncientIchorFlagonP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 600;
            AIType = 48;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                float speed = 4f;
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.GoldenShowerFriendly, (int)(Projectile.damage * 0.75f), 0f, 0);
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }
    }
}