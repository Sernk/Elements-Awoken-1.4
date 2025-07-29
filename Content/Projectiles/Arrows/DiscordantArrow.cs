using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Arrows
{
    public class DiscordantArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            AIType = ProjectileID.WoodenArrowFriendly;

            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaotron Arrow");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ChaosBurn>(), 200);
            target.immune[Projectile.owner] = 5;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.6f, 0.1f, 0.3f);

            int numChaosArrows = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<DiscordantArrowChaos>()) numChaosArrows++;
            }
            if (numChaosArrows < 100)
            {
                if (Main.rand.Next(30) == 0)
                {
                    float max = 400f;
                    NPC npc = null;
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC test = Main.npc[i];
                        if (test.active && !test.dontTakeDamage && Vector2.Distance(Projectile.Center, test.Center) <= max)
                        {
                            npc = test;
                        }
                    }
                    if (npc != null)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(Projectile.Center.Y - npc.Center.Y, Projectile.Center.X - npc.Center.X);

                        Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<DiscordantArrowChaos>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                    else Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.9f, Projectile.velocity.Y * 0.9f, ModContent.ProjectileType<DiscordantArrowChaos>(), (int)(Projectile.damage * 1.5f), Projectile.knockBack, Projectile.owner);
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
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/Arrows/DiscordantArrowChaos").Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}