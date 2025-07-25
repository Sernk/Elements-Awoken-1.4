using ElementsAwoken.Content.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Projectiles
{
    public class PutridCloud : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 4)
                    Projectile.frame = 0;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation -= 0.08f * Projectile.localAI[0];
            Projectile.ai[1]++;
            int lowestAlpha = Projectile.ai[0] == 1 ? 210 : 170;
            int diff = 255 - lowestAlpha;
            if (Projectile.ai[1] < 90)
            {
                if (Projectile.alpha > lowestAlpha) Projectile.alpha -= diff / 20;
            }
            else
            {
                Projectile.alpha++;
                if (Projectile.alpha >= 255) Projectile.Kill();
            }
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.Hitbox.Intersects(Projectile.Hitbox) && !npc.townNPC) npc.AddBuff(BuffType<FastPoison>(), 60);
            }
        }
    }
}