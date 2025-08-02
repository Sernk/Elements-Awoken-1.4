using ElementsAwoken.EASystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles
{
    public class ArchaicProtection : ModProjectile
    {
        float circleScale = 0f;
        float circleAlpha = 0.7f;
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 46;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 60;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2000;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            Projectile.Center = player.Center - new Vector2(0, 20);
            if (modPlayer.archaicProtectionTimer <= 0) Projectile.Kill();

            Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Const.GetDustID())];
            dust.noGravity = true;
            dust.velocity *= 1.5f;

            if (Projectile.localAI[0] == 0)
            {
                DustBoom();
                SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ChargeShort"), Projectile.position);
                Projectile.localAI[0]++;
            }

            Projectile.ai[0]++;
            int duration = 15;
            int num = 35;
            if (Projectile.ai[0] == num) SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/Explosion"), Projectile.position);
            if (Projectile.ai[0] > num)
            {
                circleScale = ((Projectile.ai[0] - num) / duration) * 0.6f;
                if (Projectile.ai[0] > num + duration / 2) circleAlpha = MathHelper.Lerp(0.7f, 0f, (Projectile.ai[0] - (num + duration /2)) / (duration / 2));
            }
            if (Projectile.ai[0] > num + duration)
            {
                Projectile.ai[0] = 0;
                circleScale = 0f;
                circleAlpha = 0.7f;
                SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/ChargeShort"), Projectile.position);
            }

            int maxDist = 300;

            if (Projectile.ai[0] == num + duration / 2)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, player.Center) < maxDist)
                    {
                        Vector2 toTarget = new Vector2(player.Bottom.X - npc.Center.X, player.Bottom.Y - npc.Center.Y);
                        toTarget.Normalize();
                        npc.velocity -= toTarget * 25 * npc.knockBackResist;
                    }
                }
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.hostile && Vector2.Distance(proj.Center, player.Center) < maxDist)
                    {
                        Vector2 toTarget = new Vector2(player.Bottom.X - proj.Center.X, player.Bottom.Y - proj.Center.Y);
                        toTarget.Normalize();
                        proj.velocity -= toTarget * 10;
                    }
                }
                for (int i = 0; i < Main.maxItems; i++)
                {
                    Item item = Main.item[i];
                    if (item.active && Vector2.Distance(item.Center, player.Center) < maxDist)
                    {
                        Vector2 toTarget = new Vector2(player.Bottom.X - item.Center.X, player.Bottom.Y - item.Center.Y);
                        toTarget.Normalize();
                        item.velocity -= toTarget * 10;
                    }
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            DustBoom();
        }
        private void DustBoom()
        {
            for (int i = 0; i < 31; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Const.GetDustID())];
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ElementsAwoken/Extra/Circle").Value;
            Const.Sb.Draw(tex, Projectile.Center - Main.screenPosition - (tex.Size() * circleScale) / 2, null, Color.White * circleAlpha, Projectile.rotation, Vector2.Zero, circleScale, SpriteEffects.None, 0f);
            return true;
        }
    }
}