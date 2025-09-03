using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class DeterioratorKnife : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.timeLeft = Main.rand.Next(60, 90);
                Projectile.localAI[0]++;
            }
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 40f)
            {
                Projectile.damage = (int)((double)Projectile.damage * 0.95);
                Projectile.knockBack = (float)((int)((double)Projectile.knockBack * 0.95));
            }
            if (Projectile.ai[0] < 240f)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            }
            if (Main.rand.Next(6) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 74);
                Main.dust[dust].velocity *= 0.07f;
                Main.dust[dust].scale = Main.rand.NextFloat(0.5f, 1.2f);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnKill(int timeLeft)
        {
            if (Main.rand.Next(4) == 0)
            {
                SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/AcidHiss"), Projectile.Center);
            }
            for (int k = 0; k < 3; k++)
            {
                int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 74, 0f, 0f, 100, default(Color));
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("Corroding").Type, 300, false);
        }
    }
}