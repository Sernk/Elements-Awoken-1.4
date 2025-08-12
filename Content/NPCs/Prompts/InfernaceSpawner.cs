using ElementsAwoken.Content.NPCs.Bosses.Infernace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Prompts
{
    public class InfernaceSpawner : ModNPC
    {
        public override string Texture { get { return "ElementsAwoken/Content/NPCs/Prompts/InfernaceGuardian"; } }
        private float visualsAI
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.damage = 10;
            NPC.aiStyle = -1;
            NPC.width = 26;
            NPC.height = 50;
            NPC.alpha = 255;
            NPC.lifeMax = 5;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = NPC.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D cast = ModContent.Request<Texture2D>("ElementsAwoken/Content/NPCs/Prompts/InfernaceGuadianCast").Value;
            Vector2 castOrigin = new Vector2(cast.Width * 0.5f, cast.Height * 0.5f);
            Vector2 castAddition = NPC.direction != 1 ? new Vector2(-2, 6) : new Vector2(2, 6);
            Vector2 castPos = NPC.position - Main.screenPosition + castOrigin + new Vector2(0f, NPC.gfxOffY) + castAddition;
            spriteBatch.Draw(cast, castPos, null, Color.White * (1- (float)NPC.alpha/255f), visualsAI / 15f, castOrigin, NPC.scale, spriteEffects, 0f);
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return false;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            visualsAI++;
            int infernaceID = NPC.FindFirstNPC(ModContent.NPCType<Infernace>());
            if (infernaceID >= 0)
            {
                NPC parent = Main.npc[infernaceID];
                Vector2 direction = parent.Center - NPC.Center;
                NPC.spriteDirection = Math.Sign(direction.X);
                NPC.velocity.X = 0f;

                NPC.Center = parent.Center + new Vector2(200 * NPC.ai[0], 300);
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 255 / 20;
                }
                else
                {
                    Dust dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 6)];
                    Vector2 toTarget = new Vector2(parent.Center.X - NPC.Center.X, parent.Center.Y - NPC.Center.Y);
                    toTarget.Normalize();
                    dust.velocity = toTarget * 28f;
                    dust.noGravity = true;
                    dust.fadeIn = 1.2f;
                    if (parent.alpha <= 0)
                    {
                        NPC.active = false;
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust2 = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 6)];
                            dust2.noGravity = true;
                            dust2.scale = 1f;
                            dust2.velocity *= 0.1f;
                        }
                    }
                }
            }
            else NPC.active = false;
        }
    }
}