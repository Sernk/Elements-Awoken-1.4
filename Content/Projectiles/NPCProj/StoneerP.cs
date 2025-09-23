using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj
{
    public class StoneerP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.hostile = true;
            Projectile.aiStyle = 1;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
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
    }
}