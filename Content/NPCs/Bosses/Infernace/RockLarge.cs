using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Infernace
{
    public class RockLarge : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 50;
            NPC.lifeMax = 600;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPCID.Sets.NeedsExpertScaling[NPC.type] = true;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath43;
            for (int num2 = 0; num2 < 206; num2++)
            {
                NPC.buffImmune[num2] = true;
            }
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPC.GetGlobalNPC<AwakenedModeNPC>().cantElite = true;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 1200;
            NPC.defense = 30;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 3000;
                NPC.defense = 40;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.localAI[0] == 0)
            {
                NPC.frame.Y = Main.rand.Next(3) * frameHeight;
                NPC.localAI[0]++;
            }
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];

            NPC.spriteDirection = NPC.direction;
            NPC.rotation += NPC.velocity.X * 0.1f;
            NPC parent = Main.npc[(int)NPC.ai[1]];

            if (!parent.active || parent.type != ModContent.NPCType<HealingHearth>()) NPC.active = false;

            float movespeed = 5f;
            if (Vector2.Distance(parent.Center, NPC.Center) >= 120)
            {
                movespeed = 12f;
            }
            Vector2 toTarget = new Vector2(parent.Center.X - NPC.Center.X, parent.Center.Y - NPC.Center.Y);
            toTarget = new Vector2(parent.Center.X - NPC.Center.X, parent.Center.Y - NPC.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(parent.Center, NPC.Center) >= 70)
            {
                NPC.velocity = toTarget * movespeed;
            }
            if (Main.rand.Next(200) == 0)
            {
                NPC.velocity += new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f)); // for some reason they bunch up in a line on the y axis
            }
            Vector2 diff = parent.position - parent.oldPosition;
            NPC.position += diff;

            for (int k = 0; k < Main.npc.Length; k++)
            {
                NPC other = Main.npc[k];
                if (k != NPC.whoAmI && (other.type == NPC.type || other.type == ModContent.NPCType<RockSmall>() || other.type == ModContent.NPCType<RockMedium>())
                     && other.active && Math.Abs(NPC.position.X - other.position.X) + Math.Abs(NPC.position.Y - other.position.Y) < NPC.width)
                {
                    const float pushAway = 0.05f;
                    if (NPC.position.X < other.position.X)
                    {
                        NPC.velocity.X -= pushAway;
                    }
                    else
                    {
                        NPC.velocity.X += pushAway;
                    }
                    if (NPC.position.Y < other.position.Y)
                    {
                        NPC.velocity.Y -= pushAway;
                    }
                    else
                    {
                        NPC.velocity.Y += pushAway;
                    }
                }
            }
        }      
        public override bool CheckActive()
        {
            return false;
        }
    }
}
