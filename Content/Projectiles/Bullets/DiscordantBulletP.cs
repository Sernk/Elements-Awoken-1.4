using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Bullets
{
    public class DiscordantBulletP : ModProjectile
    {
        public bool teleported = false;
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.6f, 0.1f, 0.3f);

            float max = 200f;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC nPC = Main.npc[i];
                if (!teleported && nPC.active && !nPC.friendly && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                {
                    TeleDust();

                    Projectile.tileCollide = false;

                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * max, (float)Math.Cos(angle) * max);
                    Projectile.Center = nPC.Center + offset;

                    Vector2 toTarget = new Vector2(nPC.Center.X - Projectile.Center.X, nPC.Center.Y - Projectile.Center.Y);
                    toTarget.Normalize();
                    Projectile.velocity = toTarget * 20f;

                    TeleDust();
                    teleported = true;
                }
            }
        }
        private void TeleDust()
        {
            float num1 = 16f;
            int num2 = 0;
            while ((float)num2 < num1)
            {
                Vector2 vector = Vector2.UnitX * 0f;
                vector += -Vector2.UnitY.RotatedBy((double)((float)num2 * (6.28318548f / num1)), default(Vector2)) * new Vector2(1f, 4f);
                vector = vector.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                int dust = Dust.NewDust(Projectile.Center, 0, 0, 127, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + vector;
                Main.dust[dust].velocity = Projectile.velocity * 0f + vector.SafeNormalize(Vector2.UnitY) * 1f;
                num2++;
            }
        }
    }
}