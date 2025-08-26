using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class FireKnife : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 75;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = ProjectileID.VampireKnife;
            AIType = ProjectileID.VampireKnife;
        }
        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 240f)
            {
                Projectile.alpha += 3;
                Projectile.damage = (int)((double)Projectile.damage * 0.95);
                Projectile.knockBack = (float)((int)((double)Projectile.knockBack * 0.95));
            }
            if (Projectile.ai[0] < 240f)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, 6, damageType: "thrown");
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                value15.X *= 0.25f;
                value15.Y *= 0.25f;
                Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, value15.X, value15.Y, ModContent.ProjectileType<FireKnifeBolt>(), Projectile.damage / 2, 2f, Projectile.owner, 0f, 0f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 80);   //this make so when the projectile/flame hit a npc, gives it the buff  onfire , 80 = 3 seconds
        }
    }
}