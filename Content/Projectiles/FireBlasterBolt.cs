using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class FireBlasterBolt : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
            Projectile.light = 1f;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 3f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 2; k++)
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.position.X, Projectile.position.Y, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), ModContent.ProjectileType<GreekFire>(), (int)(Projectile.damage * 0.4f), Projectile.knockBack * 0.35f, Main.myPlayer, 0f, 0f)];
                proj.timeLeft = 120;
                proj.DamageType = DamageClass.Ranged;
            }
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }
    }
}