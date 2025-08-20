using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients
{
    public class AncientProjectionSwirl2 : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Content/Projectiles/NPCProj/Ancients/AncientProjection"; } }

        public int type = 0;
        public override void SetDefaults()
        {
            Projectile.width = 88;
            Projectile.height = 102;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10000;
            Projectile.alpha = 255;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool CanHitPlayer(Player target)
        {
            if (Projectile.alpha > 90)
            {
                return false;
            }
            return base.CanHitPlayer(target);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 9)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Color color = Projectile.GetAlpha(Lighting.GetColor((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16.0)));
            Texture2D projTexture = TextureAssets.Projectile[Projectile.type].Value;
            int height = projTexture.Height / Main.projFrames[Projectile.type]; // equals the projectile.height but this is more dynamic
            int drawY = height * (int)type;

            Rectangle rectangle = new Rectangle(0, drawY, projTexture.Width, height);
            Vector2 origin = rectangle.Size() / 4f;
            origin = new Vector2(origin.X - (float)(Projectile.width * 0.5), origin.Y + (float)(Projectile.height * 0.5)); // to get it drawing at the center of the npc
            rectangle = new Rectangle(Projectile.width * Projectile.frame, drawY, Projectile.width, Projectile.height);

            Main.spriteBatch.Draw(projTexture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, color, Projectile.rotation, origin, 1.3f, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0]++;
                type = Main.rand.Next(4);
            }
            if (type == 0)
            {
                Lighting.AddLight(Projectile.Center, 1.2f, 0f, 1.5f);
            }
            else if (type == 1)
            {
                Lighting.AddLight(Projectile.Center, 0, 1.5f, 0.5f);
            }
            else if (type == 2)
            {
                Lighting.AddLight(Projectile.Center, 1.5f, 0f, 0.5f);
            }
            else if (type == 3)
            {
                Lighting.AddLight(Projectile.Center, 0.3f, 0f, 1.5f);
            }
            if (Projectile.alpha > 80)
            {
                Projectile.alpha -= 5;
            }
            Player player = Main.player[Main.myPlayer];


            Vector2 direction = player.Center - Projectile.Center;
            if (direction.X > 0f)
            {
                Projectile.spriteDirection = -1;
            }
            if (direction.X < 0f)
            {
                Projectile.spriteDirection = 1;
            }

            if (Projectile.alpha > 80)
            {
                Projectile.alpha -= 10;
            }
            NPC parent = Main.npc[(int)Projectile.ai[1]];

            Projectile.ai[0] += -1f; // speed
            int distance =  1350;
            double rad = Projectile.ai[0] * (Math.PI / 180); // angle to radians
            Projectile.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - Projectile.width / 2;
            Projectile.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - Projectile.height / 2;
            if (!parent.active || (parent.ai[2] != 7 && parent.ai[2] != 11))
            {
                Projectile.Kill();
            }
        }
    }
}