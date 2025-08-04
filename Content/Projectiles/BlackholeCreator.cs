using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BlackholeCreator : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, EAU.PinkFlame, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.75f);
            Main.dust[dust].velocity *= 0.6f;
            Main.dust[dust].scale *= 0.6f;
            Main.dust[dust].noGravity = true;

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.position.X, Projectile.position.Y, 0f, 0f, ModContent.ProjectileType<Blackhole>(), Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
            SoundEngine.PlaySound(SoundID.Item117, Projectile.position);
        }
    }
}