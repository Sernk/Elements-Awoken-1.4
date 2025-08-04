using ElementsAwoken.Content.Dusts.Ancients;
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
    public class AtaxiaBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.scale = MathHelper.Lerp(0.3f, 1.3f, Projectile.ai[0] / 300);

            Vector2 size = new Vector2(Projectile.width * Projectile.scale, Projectile.height * Projectile.scale);
            int dust = Dust.NewDust(Projectile.Center - size/2, (int)size.X, (int)size.Y, ModContent.DustType<AncientRed>());
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }
        public override void OnKill(int timeLeft)
        {
            var p = EAU.Proj(Projectile);
            if (Projectile.ai[0] > 100)
            {
                float sizeScale = MathHelper.Lerp(0.3f, 1.3f, (Projectile.ai[0] - 100) / 200);
                Projectile.NewProjectile(p, Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<Explosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                for (int num369 = 0; num369 < 20; num369++)
                {
                    int num370 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num370].velocity *= 1.4f * sizeScale;
                }
                for (int num371 = 0; num371 < 10; num371++)
                {
                    int num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AncientRed>(), 0f, 0f, 100, default(Color), 2.5f);
                    Main.dust[num372].noGravity = true;
                    Main.dust[num372].velocity *= 5f * sizeScale;
                    num372 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AncientRed>(), 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num372].velocity *= 3f * sizeScale;
                }
                int num373 = Gore.NewGore(p, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore85 = Main.gore[num373];
                gore85.velocity.X = gore85.velocity.X + 1f;
                Gore gore86 = Main.gore[num373];
                gore86.velocity.Y = gore86.velocity.Y + 1f;
                num373 = Gore.NewGore(p, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore87 = Main.gore[num373];
                gore87.velocity.X = gore87.velocity.X - 1f;
                Gore gore88 = Main.gore[num373];
                gore88.velocity.Y = gore88.velocity.Y + 1f;
                num373 = Gore.NewGore(p, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore89 = Main.gore[num373];
                gore89.velocity.X = gore89.velocity.X + 1f;
                Gore gore90 = Main.gore[num373];
                gore90.velocity.Y = gore90.velocity.Y - 1f;
                num373 = Gore.NewGore(p, new Vector2(Projectile.position.X, Projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore91 = Main.gore[num373];
                gore91.velocity.X = gore91.velocity.X - 1f;
                Gore gore92 = Main.gore[num373];
                gore92.velocity.Y = gore92.velocity.Y - 1f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }

            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Rectangle rectangle = new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]);
                EAU.Sb.Draw(tex, drawPos, rectangle, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}