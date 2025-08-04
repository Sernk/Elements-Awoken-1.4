using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.Content.Projectiles.Explosions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DesolationCharge4 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.penetrate = 6;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Main.NewText(Projectile.penetrate);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Lighting.AddLight(Projectile.Center, 0.2f, 0.6f, 0.3f);

            for (int i = 0; i < 5; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AncientGreen>())];
                dust.velocity = Vector2.Zero;
                dust.position -= Projectile.velocity / 6f * (float)i;
                dust.noGravity = true;
                dust.scale = 1f;
            }
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] % 5 == 0)
            {
                Explode();
            }
        }
        public override void OnKill(int timeLeft)
        {
            Explode();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<AncientsCurse>(), 450);
        }
        private void Explode()
        {
            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X - Projectile.velocity.X * 1.5f, Projectile.Center.Y- Projectile.velocity.Y * 1.5f, 0f, 0f, ModContent.ProjectileType<DesolationExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);           
        }
    }
}