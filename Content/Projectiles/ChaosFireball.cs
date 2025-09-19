using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ChaosFireball : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 300;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dying Azure");
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            bool immune = false;
            if (ElementsAwoken.instakillImmune.Contains(target.type)) immune = true;

            if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage && !target.boss && Main.rand.Next(20) == 0 && target.lifeMax < 30000 && !immune && Main.rand.Next(10) == 0)
            {
                target.SimpleStrikeNPC(target.lifeMax, 0, false, 0f, DamageClass.Default, false, 0, false);
                ProjectileUtils.Explosion(Projectile, new int[] { 219 }, Projectile.damage, "melee");
            }
            target.AddBuff(ModContent.BuffType<ChaosBurn>(), 180);
        }
        public override void OnKill(int timeLeft)
        {
            ProjectileUtils.Explosion(Projectile, new int[] { 127 }, Projectile.damage, "melee");
        }
        public override void AI()
        {
            Projectile.velocity.X *= 0.99f;
            Projectile.velocity.Y *= 0.99f;
            Lighting.AddLight(Projectile.Center, 0.9f, 0.1f, 0.2f);

            if (Projectile.ai[0]==0)
            {
                Projectile.scale = 0.01f;
                Projectile.ai[0]++;
            }
            float scaleMax = 0.7f;
            if (Projectile.scale < scaleMax)Projectile.scale += scaleMax / 20f;
            else Projectile.scale = scaleMax;
            Projectile.rotation += 0.02f;

            if (!ModContent.GetInstance<Config>().lowDust)
            {
                ProjectileUtils.CreateDustRing(Projectile,127, 21, 1);
            }
            ProjectileUtils.Home(Projectile, 6f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D shell = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/ChaosFireballShell").Value;
            Vector2 shellOrigin = new Vector2(shell.Width * 0.5f, shell.Height * 0.5f);
            Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, TextureAssets.Projectile[Projectile.type].Value.Height * 0.5f);
            Vector2 drawPos = Projectile.Center  - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor);
            EAU.Sb.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, 0, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            EAU.Sb.Draw(shell, drawPos, null, color, Projectile.rotation, shellOrigin, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}