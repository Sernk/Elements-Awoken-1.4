using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ElementsAwoken.Content.Projectiles
{
    public class CelestiaArc : ModProjectile
    {
        float timeTillStop = 200;
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 4;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            Projectile.scale *= 0.5f;
        }
        public override void AI()
        {
            //arc code
            int num3 = Projectile.frameCounter;
            Projectile.frameCounter = num3 + 1;
            Lighting.AddLight(Projectile.Center, 0.3f, 0.45f, 0.5f);
            if (Projectile.velocity == Vector2.Zero)
            {
                if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
                {
                    Projectile.frameCounter = 0;
                    bool flag36 = true;
                    for (int num855 = 1; num855 < Projectile.oldPos.Length; num855 = num3 + 1)
                    {
                        if (Projectile.oldPos[num855] != Projectile.oldPos[0])
                        {
                            flag36 = false;
                        }
                        num3 = num855;
                    }
                    if (flag36)
                    {
                        Projectile.Kill();
                        return;
                    }
                }
                if (Main.rand.Next(Projectile.extraUpdates) == 0)
                {
                    for (int num856 = 0; num856 < 2; num856 = num3 + 1)
                    {
                        float num857 = Projectile.rotation + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                        float num858 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector96 = new Vector2((float)Math.Cos((double)num857) * num858, (float)Math.Sin((double)num857) * num858);
                        int num859 = Dust.NewDust(Projectile.Center, 0, 0, 226, vector96.X, vector96.Y, 0, default(Color), 1f);
                        Main.dust[num859].noGravity = true;
                        Main.dust[num859].scale = 1.2f;
                        num3 = num856;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Vector2 value39 = Projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)Projectile.width;
                        int num860 = Dust.NewDust(Projectile.Center + value39 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                        Dust dust = Main.dust[num860];
                        dust.velocity *= 0.5f;
                        Main.dust[num860].velocity.Y = -Math.Abs(Main.dust[num860].velocity.Y);
                        return;
                    }
                }
            }
            else if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
            {
                Projectile.frameCounter = 0;
                float num861 = Projectile.velocity.Length();
                UnifiedRandom unifiedRandom = new UnifiedRandom((int)Projectile.ai[1]);
                int num862 = 0;
                Vector2 vector97 = -Vector2.UnitY;
                Vector2 vector98;
                do
                {
                    int num863 = unifiedRandom.Next();
                    Projectile.ai[1] = (float)num863;
                    num863 %= 100;
                    float f = (float)num863 / 100f * 6.28318548f;
                    vector98 = f.ToRotationVector2();
                    if (vector98.Y > 0f)
                    {
                        vector98.Y *= -1f;
                    }
                    bool flag37 = false;
                    if (vector98.Y > -0.02f)
                    {
                        flag37 = true;
                    }
                    if (vector98.X * (float)(Projectile.extraUpdates + 1) * 2f * num861 + Projectile.localAI[0] > 40f)
                    {
                        flag37 = true;
                    }
                    if (vector98.X * (float)(Projectile.extraUpdates + 1) * 2f * num861 + Projectile.localAI[0] < -40f)
                    {
                        flag37 = true;
                    }
                    if (!flag37)
                    {
                        goto IL_25086;
                    }
                    num3 = num862;
                    num862 = num3 + 1;
                }
                while (num3 < 100);
                Projectile.velocity = Vector2.Zero;
                Projectile.localAI[1] = 1f;
                goto IL_25092;
                IL_25086:
                vector97 = vector98;
                IL_25092:
                if (Projectile.velocity != Vector2.Zero)
                {
                    Projectile.localAI[0] += vector97.X * (float)(Projectile.extraUpdates + 1) * 2f * num861;
                    Projectile.velocity = vector97.RotatedBy((double)(Projectile.ai[0] + 1.57079637f), default(Vector2)) * num861;
                    Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
                    return;
                }
            }
            timeTillStop--;
            if (timeTillStop <= 0)
            {
                Projectile.localAI[1] += 2f;
                Projectile.position += Projectile.velocity;
                Projectile.velocity = Vector2.Zero;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.localAI[1] < 1f)
            {
                Projectile.localAI[1] += 2f;
                Projectile.position += Projectile.velocity;
                Projectile.velocity = Vector2.Zero;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)       
        {
            if (Projectile.localAI[1] < 1f)
            {
                Projectile.localAI[1] += 2f;
                Projectile.position += Projectile.velocity;
                Projectile.velocity = Vector2.Zero;
            }
            Projectile.damage = 0;
            Projectile.velocity *= 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color color = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
            Vector2 end = Projectile.position + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Texture2D drawTex = TextureAssets.Extra[33].Value; // the lightning texture
            Projectile.GetAlpha(color);
            Vector2 scale16 = new Vector2(Projectile.scale) / 2f;
            for (int num289 = 0; num289 < 3; num289++)
            {
                float num298 = (Projectile.localAI[1] == -1f || Projectile.localAI[1] == 1f) ? -0.2f : 0f;
                if (num289 == 0)
                {
                    scale16 = new Vector2(Projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = new Color(115, 204, 219, 0) * 0.5f;
                }
                else if (num289 == 1)
                {
                    scale16 = new Vector2(Projectile.scale) * 0.4f;
                    DelegateMethods.c_1 = new Color(113, 251, 255, 0) * 0.5f;
                }
                else
                {
                    scale16 = new Vector2(Projectile.scale) * 0.2f;
                    DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
                }
                DelegateMethods.f_1 = 1f;
                for (int num290 = Projectile.oldPos.Length - 1; num290 > 0; num290--)
                {
                    if (!(Projectile.oldPos[num290] == Vector2.Zero))
                    {
                        Vector2 start = Projectile.oldPos[num290] + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Vector2 end2 = Projectile.oldPos[num290 - 1] + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, drawTex, start, end2, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                    }
                }
                if (Projectile.oldPos[0] != Vector2.Zero)
                {
                    Vector2 start2 = Projectile.oldPos[0] + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, drawTex, start2, end, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                }
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                if (Projectile.oldPos[i].X == 0f && Projectile.oldPos[i].Y == 0f)
                {
                    break;
                }
                projHitbox.X = (int)Projectile.oldPos[i].X;
                projHitbox.Y = (int)Projectile.oldPos[i].Y;
                if (projHitbox.Intersects(targetHitbox))
                {
                    return true;
                }
            }
            return false;
        }       
    }
}