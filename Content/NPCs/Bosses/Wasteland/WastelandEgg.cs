using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Wasteland
{
    public class WastelandEgg : ModNPC
    {
        public int timer = 200;
        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 42;      
            NPC.npcSlots = 1f;
            NPC.aiStyle = -1;
            NPC.defense = 3;
            NPC.lifeMax = 25;
            NPCID.Sets.NeedsExpertScaling[NPC.type] = true;
            NPC.knockBackResist = 0.1f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 150;
                NPC.defense = 6;
            }
        }
        public override void AI()
        {
            NPC.ai[0]++;
            if (NPC.ai[0] >= 60)
            {
                Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
                NPC.NewNPC(EAU.NPCs(NPC), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<WastelandMinion>());
                NPC.active = false;
                Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, Mod.Find<ModGore>("WastelandEgg").Type, 1f);
                Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, Mod.Find<ModGore>("WastelandEgg2").Type, 1f);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1f;
            if (NPC.frameCounter % 20 == 0)
            {
                NPC.frame.Y += frameHeight;
            }
            if (NPC.frame.Y > frameHeight * 1)
            {
                NPC.frame.Y = 0;
            }
        }
    }
}