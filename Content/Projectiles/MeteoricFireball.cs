using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class MeteoricFireball : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.2f, 0.4f);

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Main.rand.NextBool(5))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].scale *= 1.5f;
                Main.dust[dust].noGravity = true;
            }
            Move(0.2f, Main.MouseWorld);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                float alpha = 1 - ((float)k / (float)Projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)Projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(252, 32, 3), (float)k / (float)Projectile.oldPos.Length) * alpha;
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        private void Move(float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - Projectile.Center;

            if (Projectile.velocity.X < desiredVelocity.X)
            {
                Projectile.velocity.X = Projectile.velocity.X + speed;
                if (Projectile.velocity.X < 0f && desiredVelocity.X > 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X + speed;
                }
            }
            else if (Projectile.velocity.X > desiredVelocity.X)
            {
                Projectile.velocity.X = Projectile.velocity.X - speed;
                if (Projectile.velocity.X > 0f && desiredVelocity.X < 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X - speed;
                }
            }
            if (Projectile.velocity.Y < desiredVelocity.Y)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + speed;
                if (Projectile.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + speed;
                    return;
                }
            }
            else if (Projectile.velocity.Y > desiredVelocity.Y)
            {
                Projectile.velocity.Y = Projectile.velocity.Y - speed;
                if (Projectile.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - speed;
                    return;
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 6 }, Projectile.damage,"magic");
        }
    }
}