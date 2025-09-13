using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class RadiantKatanaStar : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.scale *= 0.7f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 15f)
            {
                ProjectileUtils.Home(Projectile, 15f);
            }
            if (Projectile.oldPos[^1] == Projectile.position && Projectile.ai[1] != 0) Projectile.Kill();
        }
        private void Explosion()
        {
            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num71 = 24f;
            Vector2 value = new Vector2(1.05f, 1f);
            float num74;
            for (float num72 = 0f; num72 < num71; num72 = num74 + 1f)
            {
                int num73 = Dust.NewDust(Projectile.Center, 0, 0, EAU.PinkFlame, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num73].position = Projectile.Center;
                Main.dust[num73].velocity = spinningpoint.RotatedBy((double)(6.28318548f * num72 / num71), default(Vector2)) * value * (0.8f + Main.rand.NextFloat() * 0.4f) * 2f;
                Main.dust[num73].color = Color.SkyBlue;
                Main.dust[num73].noGravity = true;
                Dust dust = Main.dust[num73];
                dust.scale += 0.5f + Main.rand.NextFloat();
                num74 = num72;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Starstruck>(), 300);
            Projectile.ai[1] = 1;
            Explosion();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[1] = 1;
            Explosion();
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] != 0) return false;
            return base.CanHitNPC(target);
        }
        public override bool ShouldUpdatePosition()
        {
            if (Projectile.ai[1] != 0) return false;
            return base.ShouldUpdatePosition();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector11 = new Vector2((float)(TextureAssets.Projectile[Projectile.type].Value.Width / 2), (float)(TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] / 2));

            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 vector40 = Projectile.Center - Main.screenPosition;
            vector40 -= new Vector2((float)texture.Width, (float)(texture.Height / Main.projFrames[Projectile.type])) * Projectile.scale / 2f;
            vector40 += vector11 * Projectile.scale + new Vector2(0f, Projectile.gfxOffY);
            float num147 = 1f / (float)Projectile.oldPos.Length * 1.1f;
            int num148 = Projectile.oldPos.Length - 1;
            while ((float)num148 >= 0f)
            {
                if (Projectile.oldPos[num148] != Projectile.position)
                {
                    float num149 = (float)(Projectile.oldPos.Length - num148) / (float)Projectile.oldPos.Length;
                    Color color35 = Color.White;
                    color35 *= 1f - num147 * (float)num148 / 1f;
                    color35.A = (byte)((float)color35.A * (1f - num149));
                    Main.spriteBatch.Draw(texture, vector40 + Projectile.oldPos[num148] - Projectile.position, null, color35, Projectile.oldRot[num148], vector11, Projectile.scale * MathHelper.Lerp(0.8f, 0.3f, num149), spriteEffects, 0f);
                }
                num148--;
            }
            texture = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/RadiantStar").Value;
            if (Projectile.ai[1] == 0) Main.spriteBatch.Draw(texture, vector40, null, Color.White, 0f, texture.Size() / 2f, Projectile.scale, spriteEffects, 0f);
            return false;
        }
    }
}