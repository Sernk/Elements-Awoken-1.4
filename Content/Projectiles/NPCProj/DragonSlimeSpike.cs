using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class DragonSlimeSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.hostile = true;
            Projectile.aiStyle = 1;
            Projectile.penetrate = -1;
            Projectile.scale *= 0.8f;
        }
        public override void AI()
        {
            if (Projectile.ai[0] % 3 == 0)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position + new Vector2(0, 12).RotatedBy(Projectile.rotation), Projectile.width, Projectile.height, 6, 0f, 0f, 50, new Color(255, 136, 78, 150), 1.2f)];
                dust.velocity *= 0.3f;
                dust.velocity -= Projectile.velocity * 0.3f;
                dust.noGravity = true;
                dust.fadeIn = 0.8f;
            }
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item17, Projectile.position);
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 5f)
            {
                Projectile.ai[0] = 5f;
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, 80, false);
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 6, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}