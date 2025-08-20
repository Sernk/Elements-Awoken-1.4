using ElementsAwoken.Content.NPCs.Bosses.Ancients;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.NPCProj.Ancients
{
    public class AncientProjection : ModProjectile
    {
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
            if (Projectile.alpha > 80)
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
            int drawY = height * (int)Projectile.ai[0];

            Rectangle rectangle = new Rectangle(0, drawY, projTexture.Width, height);
            Vector2 origin = rectangle.Size() / 4f;
            origin = new Vector2(origin.X - (float)(Projectile.width * 0.5), origin.Y + (float)(Projectile.height * 0.5)); // to get it drawing at the center of the npc
            rectangle = new Rectangle(Projectile.width * Projectile.frame, drawY, Projectile.width, Projectile.height);

            Main.spriteBatch.Draw(projTexture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, color, Projectile.rotation, origin, 1.3f, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                Lighting.AddLight(Projectile.Center, 1.2f, 0f, 1.5f);
            }
            else if (Projectile.ai[0] == 1)
            {
                Lighting.AddLight(Projectile.Center, 0, 1.5f, 0.5f);
            }
            else if (Projectile.ai[0] == 2)
            {
                Lighting.AddLight(Projectile.Center, 1.5f, 0f, 0.5f);
            }
            else if (Projectile.ai[0] == 3)
            {
                Lighting.AddLight(Projectile.Center, 0.3f, 0f, 1.5f);
            }      

            Player player = Main.player[Main.myPlayer];

                Projectile.spriteDirection = Projectile.velocity.X > 0 ? -1 : 1;

            if (!NPC.AnyNPCs(ModContent.NPCType<AncientAmalgam>()))
            {
                Projectile.Kill();
            }

            if (Projectile.alpha > 80)
            {
                Projectile.alpha -= 10;
            }

            float rotateIntensity = 1.3f;
            float waveTime = 60f;
            Projectile.localAI[0]++;
            if (Projectile.localAI[1] == 0) // this part is to fix the offset (it is still slightlyyyy offset)
            {
                if (Projectile.localAI[0] > waveTime * 0.5f)
                {
                    Projectile.localAI[0] = 0;
                    Projectile.localAI[1] = 1;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
            }
            else
            {
                if (Projectile.localAI[0] <= waveTime)
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
                else
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.ToRadians(-rotateIntensity));
                    Projectile.velocity = perturbedSpeed;
                }
                if (Projectile.localAI[0] >= waveTime * 2)
                {
                    Projectile.localAI[0] = 0;
                }
            }
        }
    }
}