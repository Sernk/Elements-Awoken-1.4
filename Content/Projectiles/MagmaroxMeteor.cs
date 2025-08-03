using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class MagmaroxMeteor : ModProjectile
    {
        public float aiTimer = 0;
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector11 = new Vector2((float)(TextureAssets.Projectile[Projectile.type].Value.Width / 2), (float)(TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] / 2));
            Color color9 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
            float num66 = 0f;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 vector39 = Projectile.Center - Main.screenPosition;

            vector39 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.projFrames[Projectile.type])) * Projectile.scale / 2f;
            vector39 += vector11 * Projectile.scale + new Vector2(0f, num66 + Projectile.gfxOffY);
            texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle frame = new Rectangle(0, texture.Height * Projectile.frame, texture.Width, texture.Height);
            Const.Sb.Draw(texture, vector39, frame, Projectile.GetAlpha(color9), Projectile.rotation, vector11, Projectile.scale, spriteEffects, 0f);
            float num143 = 1f / (float)Projectile.oldPos.Length * 0.7f;
            int num144 = Projectile.oldPos.Length - 1;
            while (num144 >= 0f)
            {
                float num145 = (float)(Projectile.oldPos.Length - num144) / (float)Projectile.oldPos.Length;
                Color color34 = Color.Pink;
                color34 *= 1f - num143 * (float)num144 / 1f;
                color34.A = (byte)((float)color34.A * (1f - num145));
                Const.Sb.Draw(texture, vector39 + Projectile.oldPos[num144] - Projectile.position, new Rectangle?(), color34, Projectile.oldRot[num144], vector11, Projectile.scale * MathHelper.Lerp(0.3f, 1.1f, num145), spriteEffects, 0f);
                num144--;
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Rectangle hitbox2 = Projectile.Hitbox;
            for (int num67 = 0; num67 < Projectile.oldPos.Length; num67 += 3)
            {
                hitbox2.X = (int)Projectile.oldPos[num67].X;

                hitbox2.Y = (int)Projectile.oldPos[num67].Y;
                for (int i = 0; i < 2; i++) // normally 5
                {
                    int num69 = Utils.SelectRandom<int>(Main.rand, new int[]
                    {
                            6,
                            259,
                            158
                    });
                    int num70 = Dust.NewDust(hitbox2.TopLeft(), Projectile.width, Projectile.height, num69, 2.5f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num70].alpha = 200;
                    Dust dust = Main.dust[num70];
                    dust.velocity *= 1f; 
                    dust = Main.dust[num70];
                    dust.scale += Main.rand.NextFloat();
                }
            }
        }
        public int initialDamage = 0;
        public override void AI()
        {
            aiTimer++;
            if (Projectile.localAI[1] == 0)
            {
                initialDamage = Projectile.damage;
                Projectile.localAI[1]++;
            }
            if (aiTimer <= 10)
            {
                Projectile.penetrate = -1;
                Projectile.damage = 0;
            }
            else
            {
                Projectile.penetrate = 1;
                Projectile.damage = initialDamage;
            }
            if (Projectile.velocity.Y == 0f && Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 1f;
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
                return;
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.position = Projectile.oldPosition;
                float[] var_9_49F8F_cp_0 = Projectile.ai;
                int var_9_49F8F_cp_1 = 1;
                float num244 = var_9_49F8F_cp_0[var_9_49F8F_cp_1];
                var_9_49F8F_cp_0[var_9_49F8F_cp_1] = num244 + 1f;
                if (Projectile.ai[1] >= 5f)
                {
                    Projectile.active = false;
                }
                return;
            }
            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            if (Projectile.velocity.Y > 12f)
            {
                Projectile.velocity.Y = 12f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                for (int i = 0; i < 3; i++)
                {
                    int num1489 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1489].noGravity = true;
                    Main.dust[num1489].fadeIn = 1f;
                    Dust dust = Main.dust[num1489];
                    dust.velocity *= 2f;
                    Main.dust[num1489].noLight = true;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                int num1491 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                Main.dust[num1491].noGravity = true;
                Dust dust = Main.dust[num1491];
                dust.velocity *= 0.2f;
                Main.dust[num1491].fadeIn = 1f;
                if (Main.rand.Next(6) == 0)
                {
                    dust = Main.dust[num1491];
                    dust.velocity *= 10f;
                    Main.dust[num1491].noGravity = false;
                    Main.dust[num1491].noLight = true;
                }
                else
                {
                    Main.dust[num1491].velocity = Projectile.DirectionFrom(Main.dust[num1491].position) * Main.dust[num1491].velocity.Length();
                }
            }
            return;
        }
    }
}