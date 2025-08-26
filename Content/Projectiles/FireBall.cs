using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class FireBall : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 1f;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Dust smoke = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31)];
            smoke.velocity = Projectile.velocity * 0.2f;
            smoke.scale *= 1.5f;
            smoke.noGravity = true;

            Dust fire = Main.dust[Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f)];
            fire.velocity *= 0.6f;
            fire.scale *= 0.6f;
            fire.noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 2; k++)
            {
                Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.position.X, Projectile.position.Y, Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f), ModContent.ProjectileType<GreekFire>(), (int)(Projectile.damage * 0.7f), Projectile.knockBack * 0.35f, Main.myPlayer, 0f, 0f)];
                proj.timeLeft = 90;
                proj.DamageType = DamageClass.Magic;
            }
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }
    }
}