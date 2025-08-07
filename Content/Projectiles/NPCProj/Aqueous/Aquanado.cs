using ElementsAwoken.Content.NPCs.Bosses.Aqueous;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Aqueous
{
    public class Aquanado : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 150;
            Projectile.height = 42;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = 540;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(102, 255, 216);
        }
        public override void AI()
        {
            int num606 = 10;
            int num607 = 15; 
            float num608 = 1f;
            int num609 = 150;
            int num610 = 42;
            if (Projectile.velocity.X != 0f)
            {
                Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
            }
            int num3 = Projectile.frameCounter;
            Projectile.frameCounter = num3 + 1;
            if (Projectile.frameCounter > 2)
            {
                num3 = Projectile.frame;
                Projectile.frame = num3 + 1;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 6)
            {
                Projectile.frame = 0;
            }
            if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
            {
                Projectile.localAI[0] = 1f;
                Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
                Projectile.scale = ((float)(num606 + num607) - Projectile.ai[1]) * num608 / (float)(num607 + num606);
                Projectile.width = (int)((float)num609 * Projectile.scale);
                Projectile.height = (int)((float)num610 * Projectile.scale);
                Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[1] != -1f)
            {
                Projectile.scale = ((float)(num606 + num607) - Projectile.ai[1]) * num608 / (float)(num607 + num606);
                Projectile.width = (int)((float)num609 * Projectile.scale);
                Projectile.height = (int)((float)num610 * Projectile.scale);
            }
            if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.alpha -= 30;
                if (Projectile.alpha < 60)
                {
                    Projectile.alpha = 60;
                }
            }
            else
            {
                Projectile.alpha += 30;
                if (Projectile.alpha > 150)
                {
                    Projectile.alpha = 150;
                }
            }
            if (Projectile.ai[0] > 0f)
            {
                float[] var_2_19A93_cp_0 = Projectile.ai;
                int var_2_19A93_cp_1 = 0;
                float num73 = var_2_19A93_cp_0[var_2_19A93_cp_1];
                var_2_19A93_cp_0[var_2_19A93_cp_1] = num73 - 1f;
            }
            if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
                Vector2 center = Projectile.Center;
                center.Y -= (float)num610 * Projectile.scale / 2f;
                float num611 = ((float)(num606 + num607) - Projectile.ai[1] + 1f) * num608 / (float)(num607 + num606);
                center.Y -= (float)num610 * num611 / 2f;
                center.Y += 2f;
                Projectile.NewProjectile(EAU.Proj(Projectile), center.X, center.Y, Projectile.velocity.X, Projectile.velocity.Y, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 10f, Projectile.ai[1] - 1f);
                int num612 = 2;
                if ((int)Projectile.ai[1] % num612 == 0 && Projectile.ai[1] != 0f)
                {
                    int num614 = NPC.NewNPC(EAU.Proj(Projectile), (int)center.X, (int)center.Y, ModContent.NPCType<AquaticReaper>(), 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num614].velocity = Projectile.velocity;
                    Main.npc[num614].netUpdate = true;
                    if (Projectile.type == 386)
                    {
                        Main.npc[num614].ai[2] = (float)Projectile.width;
                        Main.npc[num614].ai[3] = -1.5f;
                    }
                }
            }
            if (Projectile.ai[0] <= 0f)
            {
                float num615 = 0.104719758f;
                float num616 = (float)Projectile.width / 5f;
                if (Projectile.type == 386)
                {
                    num616 *= 2f;
                }
                float num617 = (float)(Math.Cos((double)(num615 * -(double)Projectile.ai[0])) - 0.5) * num616;
                Projectile.position.X = Projectile.position.X - num617 * (float)(-(float)Projectile.direction);
                float[] var_2_19D98_cp_0 = Projectile.ai;
                int var_2_19D98_cp_1 = 0;
                float num73 = var_2_19D98_cp_0[var_2_19D98_cp_1];
                var_2_19D98_cp_0[var_2_19D98_cp_1] = num73 - 1f;
                num617 = (float)(Math.Cos((double)(num615 * -(double)Projectile.ai[0])) - 0.5) * num616;
                Projectile.position.X = Projectile.position.X + num617 * (float)(-(float)Projectile.direction);
                return;
            }
        }
    }
}