using ElementsAwoken.Content.Dusts.Ancients;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ShimmersparkStrike : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, GetDustID());
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
            if (Main.rand.Next(2) == 0)
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X * 0.05f, Projectile.velocity.Y * 0.05f).RotatedByRandom(MathHelper.ToRadians(7));
                Projectile.NewProjectile(Const.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ShimmerShrapnel>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
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
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Const.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            //ProjectileUtils.Explosion(projectile, new int[] { ModContent.DustType<AncientRed>(), ModContent.DustType<AncientGreen>(), ModContent.DustType<AncientBlue>(), ModContent.DustType<AncientPink>() }, damageType: "ranged");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 0;
            //ProjectileUtils.Explosion(projectile, new int[] { ModContent.DustType<AncientRed>(), ModContent.DustType<AncientGreen>(), ModContent.DustType<AncientBlue>(), ModContent.DustType<AncientPink>() }, damageType: "ranged");
        }
    }
}