using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Wasteland
{
    public class AcidAuraBase : ModProjectile
    {  	
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1200;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (Vector2.Distance(player.Center, Projectile.Center) < 60)
                {
                    player.AddBuff(ModContent.BuffType<AcidBurn>(), 180);
                }
            }
            Lighting.AddLight(Projectile.Center, 0.35f, 0.5f, 0.24f);
            if (Main.rand.Next(7) == 0)
            {
                Vector2 position = Projectile.Center + Main.rand.NextVector2Circular(50, 50);
                Dust dust = Dust.NewDustPerfect(position, 74, Vector2.Zero, 150);
                dust.noGravity = true;
            }
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] > 900)
            {
                Projectile.alpha += 7;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void PostDraw(Color lightColor)
        {
            Vector2 drawPos = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Texture2D auraTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/Wasteland/AcidAura").Value;
            EAU.Sb.Draw(auraTex, drawPos - new Vector2(auraTex.Width / 2, auraTex.Height / 2), null, Projectile.GetAlpha(lightColor) * 0.3f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Texture2D outlineTex = ModContent.Request<Texture2D>("ElementsAwoken/Content/Projectiles/NPCProj/Wasteland/AcidAuraOutline").Value;
            EAU.Sb.Draw(outlineTex, drawPos - new Vector2(outlineTex.Width / 2, outlineTex.Height / 2), null, Projectile.GetAlpha(lightColor), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override void OnKill(int timeLeft)
        {
            int numDusts = 20;
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = (Vector2.Normalize(Vector2.One) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + Projectile.Center;
                Vector2 velocity = position - Projectile.Center;
                int dust = Dust.NewDust(position + velocity, 0, 0, 74, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 3f;
            }
        }
    }
}