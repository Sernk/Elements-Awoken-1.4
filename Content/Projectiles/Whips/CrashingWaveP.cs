using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Whips
{
    public class CrashingWaveP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.GetGlobalProjectile<EAProjectileType>().whip = true;
            Projectile.GetGlobalProjectile<EAProjectileType>().whipAliveTime = 15;
        }
        public override void AI()
        {       
            Player player = Main.player[Projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.localAI[1] > 0f)
            {
                Projectile.localAI[1] -= 1f;
            }
            Projectile.alpha -= 42;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = Projectile.velocity.ToRotation();
            }
            float num32 = (float)((Projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : -1);
            if (Projectile.ai[1] <= 0f)
            {
                num32 *= -1f;
            }
            int timeAlive = Projectile.GetGlobalProjectile<EAProjectileType>().whipAliveTime;
            Vector2 vector17 = (num32 * (Projectile.ai[0] / timeAlive * 6.28318548f - 1.57079637f)).ToRotationVector2();
            vector17.Y *= (float)Math.Sin((double)Projectile.ai[1]);
            if (Projectile.ai[1] <= 0f)
            {
                vector17.Y *= -1f;
            }
            vector17 = vector17.RotatedBy((double)Projectile.localAI[0], default(Vector2));
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] < timeAlive)
            {
                float distance = 20f;
                Projectile.velocity += distance * vector17;
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
            Vector2 vector24 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector24.X = (float)player.bodyFrame.Width - vector24.X;
            }
            if (player.gravDir != 1f)
            {
                vector24.Y = (float)player.bodyFrame.Height - vector24.Y;
            }
            vector24 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
            Projectile.Center = player.RotatedRelativePoint(player.position + vector24, true) - Projectile.velocity;
            if (Projectile.alpha == 0)
            {
                if (Main.rand.Next(2) == 0)
                {
                    Dust dust = Main.dust[Dust.NewDust(Projectile.position + Projectile.velocity * 2f, Projectile.width, Projectile.height, 172, 0f, 0f, 100, default(Color), 2f)];
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                    dust.velocity += Projectile.localAI[0].ToRotationVector2();
                    dust.fadeIn = 1.5f;
                }
                float num52 = 18f;
                int num53 = 0;
                while ((float)num53 < num52)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        Vector2 position = Projectile.position + Projectile.velocity + Projectile.velocity * ((float)num53 / num52);
                        Dust dust2 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, 172, 0f, 0f, 100, default(Color), 1f)];
                        dust2.noGravity = true;
                        dust2.fadeIn = 0.5f;
                        dust2.velocity += Projectile.localAI[0].ToRotationVector2();
                    }
                    num53++;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
            if (Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type])
            {
                color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
            }
            Vector2 projPos = Projectile.position;
            projPos = new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition; //fuck it
            Texture2D texture2D22 = TextureAssets.Projectile[Projectile.type].Value;
            Color alpha3 = Projectile.GetAlpha(color25);
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            float num230 = Projectile.velocity.Length() + 16f;
            bool flag24 = num230 < 100f;
            Vector2 value28 = Vector2.Normalize(Projectile.velocity);
            Rectangle rectangle8 = new Rectangle(0, 0, texture2D22.Width, 26);
            Vector2 value29 = new Vector2(0f, Main.player[Projectile.owner].gfxOffY);
            float rotation24 = Projectile.rotation + 3.14159274f;
            EAU.Sb.Draw(texture2D22, Projectile.Center.Floor() - Main.screenPosition + value29, new Rectangle?(rectangle8), alpha3, rotation24, rectangle8.Size() / 2f - Vector2.UnitY * 4f, Projectile.scale, SpriteEffects.None, 0f);
            num230 -= 40f * Projectile.scale;
            Vector2 vector31 = Projectile.Center.Floor();
            vector31 += value28 * Projectile.scale * 16f;
            rectangle8 = new Rectangle(0, 42, texture2D22.Width, 10); 
            if (num230 > 0f)
            {
                float num231 = 0f;
                while (num231 + 1f < num230)
                {
                    if (num230 - num231 < (float)rectangle8.Height)
                    {
                        rectangle8.Height = (int)(num230 - num231);
                    }
                    EAU.Sb.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Rectangle?(rectangle8), alpha3, rotation24, new Vector2((float)(rectangle8.Width / 2), 0f), Projectile.scale, SpriteEffects.None, 0f);
                    num231 += (float)rectangle8.Height * Projectile.scale;
                    vector31 += value28 * (float)rectangle8.Height * Projectile.scale;
                }
            }
            Vector2 value30 = vector31;
            vector31 = Projectile.Center.Floor();
            vector31 += value28 * Projectile.scale * 24f;
            rectangle8 = new Rectangle(0, 28, texture2D22.Width, 12); 
            int num232 = 4;
            if (flag24)
            {
                num232 = 2;
            }
            float num233 = num230;
            if (num230 > 0f)
            {
                float num234 = 0f;
                float num235 = num233 / (float)num232;
                num234 += num235 * 0.25f;
                vector31 += value28 * num235 * 0.25f;
                for (int num236 = 0; num236 < num232; num236++)
                {
                    float num237 = num235;
                    if (num236 == 0)
                    {
                        num237 *= 0.75f;
                    }
                    EAU.Sb.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Rectangle?(rectangle8), alpha3, rotation24, new Vector2((float)(rectangle8.Width / 2), 0f), Projectile.scale, SpriteEffects.None, 0f);
                    num234 += num237;
                    vector31 += value28 * num237;
                }
            }
            rectangle8 = new Rectangle(0, 54, texture2D22.Width, 20); //end
            EAU.Sb.Draw(texture2D22, value30 - Main.screenPosition + value29, new Rectangle?(rectangle8), alpha3, rotation24, texture2D22.Frame(1, 1, 0, 0).Top(), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity, (float)Projectile.width * Projectile.scale, DelegateMethods.CutTiles);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float num8 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity, 16f * Projectile.scale, ref num8))
            {
                return true;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Slow, 180, false);
        }
    }
}