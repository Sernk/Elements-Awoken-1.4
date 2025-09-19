using ElementsAwoken.EASystem.EAPlayer;
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
    public class BlazeguardWave : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 86;
            Projectile.height = 86;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.light = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blazeguard");
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                float alpha = (1 - ((float)Projectile.alpha / 255f)) - ((float)k / (float)Projectile.oldPos.Length);
                float scale = 1 - ((float)k / (float)Projectile.oldPos.Length);
                Color color = Color.Lerp(Color.White, new Color(252, 32, 3), (float)k / (float)Projectile.oldPos.Length) * alpha;
                EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale * scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            int hitboxSize = 16;
            if (Collision.SolidCollision(Projectile.Center - new Vector2(hitboxSize / 2, hitboxSize / 2), hitboxSize, hitboxSize)) Projectile.ai[1] = 2;

            if (Projectile.ai[1] > 0 || Projectile.timeLeft < 20)
            {
                int amount = 20;
                if (Projectile.ai[1] == 2) amount = 10;
                ProjectileUtils.FadeOut(Projectile, 255 / amount);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] > 0) return false;
            return base.CanHitNPC(target);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[1]++;
            SoundEngine.PlaySound(SoundID.Item14.WithPitchOffset(-0.5f), Projectile.position);
            int numberProjectiles = Main.rand.Next(2, 5);
            float max = target.height * 3f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile wave = Main.projectile[Projectile.NewProjectile(EAU.Proj(Projectile), target.Center + Main.rand.NextVector2Square(-max, max),Vector2.Zero, ModContent.ProjectileType<BlazeguardWave2>(), Projectile.damage / 2, 0, Projectile.owner)];
                Vector2 toTarget = target.Center - wave.Center;
                toTarget.Normalize();
                wave.velocity = toTarget * 6f;
            }
        }
        public override void OnKill(int timeLeft) { }
    }
}