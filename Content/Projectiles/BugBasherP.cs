using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class BugBasherP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.scale = 1f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 3;
            Projectile.timeLeft = 200;
            Projectile.extraUpdates = 2;
            AIType = 301;
        }
        public override void AI()
        {
            Projectile.velocity.X *= 1.07f;
            Projectile.velocity.Y *= 1.07f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] += 0.1f;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            ReleaseToxin();
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ReleaseToxin();
        }
        private void ReleaseToxin()
        {
            float random = Main.rand.Next(30, 90); // so its not an even spread
            float spread = random * 0.0174f;
            double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            int i;
            for (i = 0; i < 4; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<BugToxin>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<BugToxin>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
            SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        }
    }
}