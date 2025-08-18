using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious
{
    public class ObsidiousFirePortal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 126;
            Projectile.height = 126;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hostile = true;
            Projectile.alpha = 255;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color color = Projectile.GetAlpha(lightColor);
            Texture2D headTexture = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/Obsidious/ObsidiousFirePortalCenter").Value;
            Vector2 drawOrigin = new Vector2(headTexture.Width * 0.5f, headTexture.Height * 0.5f);
            EAU.Sb.Draw(headTexture, new Vector2(Projectile.Center.X, Projectile.Center.Y) - Main.screenPosition - drawOrigin, null, color, 0f, Vector2.Zero, Projectile.scale, SpriteEffects.None, 0f);
            return true;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] < 1)
            {
                Projectile.scale = 0.01f;
            }
                if (Projectile.localAI[0] <= 60)
            {
                Projectile.alpha -= 15;
                Projectile.scale += 0.05f;
            }
            if (Projectile.localAI[0] >= 360)
            {
                Projectile.alpha += 15;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.scale > 1f)
            {
                Projectile.scale = 1f;
            }
            Projectile.rotation += 0.15f;

            int maxdusts = 10;
            for (int i = 0; i < maxdusts; i++)
            {
                float dustDistance = 75;
                float dustSpeed = 6;
                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                Dust vortex = Dust.NewDustPerfect(Projectile.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                vortex.noGravity = true;
            }
            Player player = Main.player[Main.myPlayer];
            Vector2 playerPos = new Vector2(Main.player[Main.myPlayer].Center.X, Main.player[Main.myPlayer].Center.Y);
            Projectile.localAI[1]--;
            if (Projectile.localAI[0] >= 60 && Projectile.localAI[0] <= 360)
            {
                if (Projectile.localAI[1] <= 0)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Vector2 shootVel = playerPos - Projectile.Center;

                        Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
                        int type = ModContent.ProjectileType<SolarFragmentProj>();
                        float Speed = 12f;
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int damage = Projectile.damage / 3;
                        Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(20));
                        Projectile.NewProjectile(EAU.Proj(Projectile), vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 0f, Main.myPlayer, 0f, 0f);
                        Projectile.localAI[1] = Main.rand.Next(8, 24);
                    }
                }
            }
        }

    }
}
