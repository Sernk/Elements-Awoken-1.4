using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles.NPCProj.RadiantMaster
{
    public class RadiantFireball : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.scale *= 0.6f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
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
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Firework_Pink, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void AI()
        {
            Projectile.velocity.Y = Projectile.velocity.Y + 0.05f;
            if (Projectile.velocity.Y > 12f)
            {
                Projectile.velocity.Y = 12f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation() - 1.57079637f;
            if (!GetInstance<Config>().lowDust)
            {
                if (Main.rand.NextBool(12))
                {
                    int num1489 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Firework_Pink, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num1489].noGravity = true;
                    Main.dust[num1489].fadeIn = 1f;
                    Dust dust = Main.dust[num1489];
                    dust.velocity *= 2f;
                    Main.dust[num1489].noLight = true;
                }
            }
            if (Projectile.ai[0] == 0)
            {
                int maxDist = 50;
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Player player = Main.LocalPlayer;
                    if (!player.dead && player.active && Vector2.Distance(player.Center, Projectile.Center) < maxDist)
                    {
                        KillWithStasis();
                    }
                }
                else
                {
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player player = Main.player[i];
                        if (!player.dead && player.active && Vector2.Distance(player.Center, Projectile.Center) < maxDist)
                        {
                            KillWithStasis();
                        }
                    }
                }
            }
        }
        private void KillWithStasis()
        {
            Projectile.Kill();
            Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ProjectileType<RadiantStasisField>(), 0, 0f, Main.myPlayer);
        }
    }
}