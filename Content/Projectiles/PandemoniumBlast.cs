using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class PandemoniumBlast : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Main.projFrames[Projectile.type] = 3;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 200;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ChaosBurn>(), 180);
        }
        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 2f), Projectile.width + 2, Projectile.height + 2, 127, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
            Main.dust[dust].noGravity = true;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
        public override void OnKill(int timeLeft)
        {
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                value15.X *= 0.25f;
                value15.Y *= 0.25f;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, value15.X, value15.Y, ModContent.ProjectileType<PandemoniumFlame>(), Projectile.damage / 2, 2f, Projectile.owner, 0f, 0f);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 2)
                    Projectile.frame = 0;
            }
            return true;
        }
    }
}