using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Thrown
{
    public class FetidBladeP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.ai[1] == 0)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Texture2D tailTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/Thrown/FetidBladeTrail").Value;
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    float scale = 1 - ((float)k / (float)Projectile.oldPos.Length);
                    Color color = Color.Lerp(Color.White, new Color(0, 150, 88), (float)k / (float)Projectile.oldPos.Length) * scale;
                    EAU.Sb.Draw(tailTex, drawPos, null, color, Projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        } 
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.timeLeft < 120)
            {
                Projectile.alpha += 255 / 120;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
            if (Projectile.ai[1] != 0)
            {
                Projectile.tileCollide = false;
                NPC stick = Main.npc[(int)Projectile.ai[0]];
                if (stick.active)
                {
                    Projectile.Center = stick.Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = stick.gfxOffY;
                }
                else Projectile.Kill();
            }
            else
            {
                if (Main.rand.NextBool(3))
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 46, 0, 0, 150);
                    Main.dust[dust].velocity *= 0.1f;
                    Main.dust[dust].scale *= 1.5f;
                    Main.dust[dust].noGravity = true;
                }
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] == 1) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numAttached = 0;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == Projectile.type && proj.alpha < 100 && proj.ai[1] != 0 && proj.ai[0] == target.whoAmI)
                {
                    numAttached++;
                }
            }
            if (numAttached < 3)
            {
                Projectile.ai[0] = target.whoAmI;
                Projectile.ai[1] = 1;
                Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
                Projectile.netUpdate = true;
            }
            else Projectile.Kill();
        }
    }
}