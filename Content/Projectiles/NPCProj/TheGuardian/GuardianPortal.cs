using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.TheGuardian
{
    public class GuardianPortal : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.alpha >= 60) return false;
            return base.CanHitPlayer(target);
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.2f, 0.4f);

            if (Main.rand.NextBool(2))
            {
                Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(Projectile.width * 0.5f, Projectile.height * 0.5f);
                Dust dust = Dust.NewDustPerfect(position, 6, Vector2.Zero);
                dust.noGravity = true;
            }
            if (Projectile.ai[0] == 0)
            {
                int swirlCount = 5;
                for (int l = 0; l < swirlCount; l++)
                {
                    int distance = 360 / swirlCount;
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<GuardianPortalSwirl>(), Projectile.damage, Projectile.knockBack, 0, l * distance, Projectile.whoAmI);
                }
                Projectile.ai[0]++;
            }
            if (Projectile.timeLeft <= 60)
            {
                Projectile.alpha += 255 / 60;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
            Projectile.ai[1]++;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, TextureAssets.Projectile[Projectile.type].Value.Height * 0.5f);
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor);
            EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color * 0.5f, Projectile.rotation, drawOrigin, Projectile.scale * ((1+ (float)Math.Sin(Projectile.ai[1] / 6)) * 0.75f), SpriteEffects.None, 0f);
            return true;
        }
    }
}