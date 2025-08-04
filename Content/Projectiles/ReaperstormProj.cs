using ElementsAwoken.Content.Projectiles.Explosions;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ReaperstormProj : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 1f;
                SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
            }
            for (int i = 0; i < 4; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, EAU.PinkFlame)];
                dust.velocity *= 0.1f;
                dust.position -= Projectile.velocity / 5f * (float)i;
                dust.noGravity = true;
                dust.scale = 1.5f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<VoidExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        }
    }
}