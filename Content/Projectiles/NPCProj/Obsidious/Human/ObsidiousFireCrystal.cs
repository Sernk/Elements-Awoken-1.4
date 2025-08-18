using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Obsidious.Human
{
    public class ObsidiousFireCrystal : ModProjectile
    { 	
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 220;
            Projectile.light = 2f;
            DrawOffsetX = -10;
            DrawOriginOffsetY = -10;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.localAI[0] <= 30)
            {
                Projectile.frame = 0;
            }
            else
            {
                Projectile.frame = 1;
            }
            return true;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] < 30f)
            {
                NPC npc = Main.npc[(int)Projectile.ai[1]];
                if (npc != Main.npc[0])
                {
                    if (npc.direction == -1)
                    {
                        Projectile.position.X = npc.Center.X - 30;
                    }
                    else if (npc.direction == 1)
                    {
                        Projectile.position.X = npc.Center.X + 30;
                    }
                    Projectile.position.Y = npc.Center.Y;
                }
                if (!npc.active)
                {
                    Projectile.Kill();
                }
            }
            else if (Projectile.localAI[0] == 30f)
            {
                float speed = 15f;
                double angle = Math.Atan2(Main.player[Main.myPlayer].position.Y - Projectile.position.Y, Main.player[Main.myPlayer].position.X - Projectile.position.X);
                Projectile.velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
                SoundEngine.PlaySound(SoundID.DD2_SonicBoomBladeSlash, Projectile.position);
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 6, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}