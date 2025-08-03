using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class RadiantBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 900;
            Projectile.penetrate = -1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Starstruck>(), 300);
            target.immune[Projectile.owner] = 5;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            ProjectileUtils.PushOtherEntities(Projectile);
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Const.PinkFlame)];
                dust.velocity *= 0.1f;
                dust.scale *= 0.2f;
                dust.noGravity = true;
                dust.fadeIn = 1f;
            }
            Vector2 targetPos = Projectile.position;
            float targetDist = 700;
            bool target = false;
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (distance < targetDist)
                    {
                        targetDist = distance;
                        targetPos = npc.Center;
                        target = true;
                    }
                }
            }
            if (target)
            {
                Vector2 toTarget = targetPos - Projectile.Center;
                float dist = (float)Math.Sqrt(toTarget.X * toTarget.X + toTarget.Y * toTarget.Y);
                toTarget.Normalize();
                if (dist >= 150)
                {
                    Projectile.velocity = toTarget * 18;
                }
            }
            else
            {
                float speed = 16f;
                Vector2 toTarget = player.Center - Projectile.Center;
                float dist = (float)Math.Sqrt(toTarget.X * toTarget.X + toTarget.Y * toTarget.Y);
                if (dist < 100)
                {
                }
                else if (dist < 2000)
                {
                    dist = speed / dist;
                    toTarget.X *= dist;
                    toTarget.Y *= dist;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + toTarget.X) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + toTarget.Y) / 21f;
                }
                else
                {
                    Projectile.Center = player.Center;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Const.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 16; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Const.PinkFlame, Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 100, default(Color), 1.2f)];
                dust.noGravity = true;
                dust.velocity *= 0.5f;
                dust.fadeIn = 0.9f;
            }
        }
    }
}