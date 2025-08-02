using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.RadiantMaster
{
    public class RadiantStasisField : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 420;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Projectile.rotation += 0.02f;
            int maxDist = 75; 
            if (!ModContent.GetInstance<Config>().lowDust)
            {
                for (int i = 0; i < 16; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                    Dust dust = Main.dust[Dust.NewDust(Projectile.Center + offset - Vector2.One * 4, 0, 0, DustID.Firework_Pink, 0, 0, 100)];
                    dust.noGravity = true;
                }
            }
            float slowSpeed = 0.6f;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Player player = Main.LocalPlayer;
                if (!player.dead && player.active && Vector2.Distance(player.Center, Projectile.Center) < maxDist)
                {
                    if (player.velocity.Y > 0) player.velocity.Y = 0f;
                    if (player.velocity.Y < -1) player.velocity.Y *= slowSpeed;
                    if (player.velocity.X > 1 || player.velocity.X < -1) player.velocity.X *= slowSpeed;
                }
            }
            else
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (!player.dead && player.active && Vector2.Distance(player.Center, Projectile.Center) < maxDist)
                    {
                        if (player.velocity.Y > 0.2 || player.velocity.Y < -1) player.velocity.Y *= slowSpeed;
                        if (player.velocity.X > 1 || player.velocity.X < -1) player.velocity.X *= slowSpeed;
                    }
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Texture2D auraTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/RadiantMaster/RadiantStasisField").Value;
            Const.Sb.Draw(auraTex, drawPos - new Vector2(auraTex.Width / 2, auraTex.Height / 2), null, Color.White * 0.6f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Texture2D middleTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/RadiantMaster/RadiantStasisFieldStar").Value;
            Vector2 middleOrigin = new Vector2(middleTex.Width / 2, middleTex.Height / 2);
            Const.Sb.Draw(middleTex, drawPos, null, Color.White, Projectile.rotation, middleOrigin, 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}