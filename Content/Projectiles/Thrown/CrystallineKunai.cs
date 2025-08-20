using ElementsAwoken.Content.NPCs.Town;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class CrystallineKunai : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 90;
            Projectile.DamageType = DamageClass.Throwing;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == ModContent.NPCType<Storyteller>())
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.velocity.Y += 0.09f;
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 60, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f); // red : 12? 
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 61, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f); // green
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 59, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f); // blue
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 62, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f); // pink
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 60, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f); // red : 12? 
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 61, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f); // green
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 59, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f); // blue
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 62, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f); // pink
            }
        }
    }
}