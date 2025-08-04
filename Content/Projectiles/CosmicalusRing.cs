using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class CosmicalusRing : ModProjectile
    {
        public int shootTimer = 100;
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            Projectile.Center = new Vector2(player.Center.X, player.Center.Y - 60);
            if (player.direction == -1)
            {
                Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.spriteDirection = 1;
            }
            if (!modPlayer.cosmicalusArmor)
            {
                Projectile.Kill();
            }
            shootTimer--;
            if (shootTimer <= 15 && shootTimer >= 0)
            {
                int maxdusts = 4;
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = Main.rand.Next(30, 45);
                    float dustSpeed = 4.5f;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(Projectile.Center + offset, 220, velocity, 0, default(Color), 1f);
                    vortex.noGravity = true;
                }
            }
            if (Projectile.owner == Main.myPlayer)
            {
                float max = 500f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 12f;
                        float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                        if (shootTimer <= 0)
                        {
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            SoundEngine.PlaySound(SoundID.Item30, Projectile.position);
                            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<PlanetarySpike>(), 30, Projectile.knockBack, Projectile.owner);
                            shootTimer = 100;
                        }
                    }
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            float scale = MathHelper.Clamp((float)Math.Sin((Main.time / 19.99f)) * 0.2f, 0f, 1f);
            SpriteEffects spriteEffects = Projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                float trailScale = (1f + scale) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, trailScale, spriteEffects, 0f);
            }
            Vector2 glowPos = Projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color1 = Projectile.GetAlpha(lightColor) * 0.4f;
            EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, glowPos, null, color1, Projectile.rotation, drawOrigin, 1f + scale, spriteEffects, 0f);
            return true;
        }
    }
}