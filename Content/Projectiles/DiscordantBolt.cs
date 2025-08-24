using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class DiscordantBolt : ModProjectile
    {
        int toxinTimer = 10;
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 24;
            Projectile.scale = 1.0f;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 0;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Discord>(), 120);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > Main.projFrames[Projectile.type] - 1)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 75);
            Main.dust[dust].velocity *= 0.1f;
            Main.dust[dust].scale *= 1.5f;
            Main.dust[dust].noGravity = true;
            dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 135);

            toxinTimer--;
            if (toxinTimer <=0)
            {
                int numberProjectiles = 2;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 value15 = new Vector2((float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12));
                    value15.X *= 0.25f;
                    value15.Y *= 0.25f;
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, value15.X, value15.Y, ModContent.ProjectileType<DiscordantToxin>(), Projectile.damage, 2f, Projectile.owner, 0f, 0f);
                }
                toxinTimer = 10 + Main.rand.Next(0,10);
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 75, 135 }, damageType: "magic");
        }
    }
}