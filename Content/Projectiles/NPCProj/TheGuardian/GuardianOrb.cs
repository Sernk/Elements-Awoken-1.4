using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.TheGuardian
{
    public class GuardianOrb : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/NPCProj/TheGuardian/GuardianTargeter"; } }

        public override void SetDefaults()
        {
            Projectile.width = 62;
            Projectile.height = 62;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.scale = 1;
        }
        public override void AI()
        {
            Projectile.rotation += 0.05f;
            if (Projectile.ai[0] ==0 )
            {
                Projectile.scale = 0.01f;
                Projectile.ai[0]++;
            }
            float maxScale = 0.7f;
            if (Projectile.scale < maxScale) Projectile.scale += maxScale / 30;
            else Projectile.scale = maxScale;

            if (Main.rand.NextBool(3))
            {
                int innerCircle = 18;
                Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(innerCircle * 0.5f, innerCircle * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.noGravity = true;
                dust.velocity = Projectile.velocity * -0.2f;
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot, Projectile.position);
            Player P = Main.player[Main.myPlayer];
            float Speed = 20f;
            float rotation = (float)Math.Atan2(Projectile.Center.Y - P.Center.Y, Projectile.Center.X - P.Center.X);
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<GuardianShot>(), Projectile.damage, 0f, 0);
        }
    }
}