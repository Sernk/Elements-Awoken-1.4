using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class FundementalStrike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0 && Projectile.localAI[0] == 0)
            {
                Projectile.penetrate = 1;
                Projectile.tileCollide = true;
                Projectile.localAI[0] = 1;
            }
            if (Projectile.ai[1] != 0)
            {
                Projectile.alpha += 15;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, GetDustID());
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }
        private int GetDustID()
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    return ModContent.DustType<AncientRed>();
                case 1:
                    return ModContent.DustType<AncientGreen>();
                case 2:
                    return ModContent.DustType<AncientBlue>();
                case 3:
                    return ModContent.DustType<AncientPink>();
                default:
                    return ModContent.DustType<AncientRed>();
            }
        }
        public override void OnKill(int timeLeft)
        {
            if (Projectile.ai[0] != 1)
            {
                ProjectileUtils.Explosion(Projectile, new int[] { ModContent.DustType<AncientRed>(), ModContent.DustType<AncientGreen>(), ModContent.DustType<AncientBlue>(), ModContent.DustType<AncientPink>() }, damageType: "melee");

                for (int i = 0; i < 4; i++)
                {
                    Vector2 speed = new Vector2((float)Main.rand.Next(-9, 9), (float)Main.rand.Next(-9, 9));
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<AncientShard>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.ai[0] == 1)
            {
                Projectile.ai[1]++;
            }
            else
            {
                float damageMult = Projectile.damage > 1000 ? 0.4f : 0.75f;
                for (int i = 0; i < 2; i++)
                {                
                    Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X + 500 * (i % 2 == 0 ? -1 : 1), Projectile.Center.Y, 22f * (i % 2 == 0 ? 1 : -1), 0f, ModContent.ProjectileType<FundementalStrike>(), (int)(Projectile.damage * damageMult), Projectile.knockBack, Projectile.owner, 1f, 0f);
                }
                target.immune[Projectile.owner] = 5;
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