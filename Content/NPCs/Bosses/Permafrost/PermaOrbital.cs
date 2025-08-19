using ElementsAwoken.Content.Buffs.Debuffs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Permafrost
{
    public class PermaOrbital : ModNPC
    {
        public float shootTimer1 = 180f;

        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 50;
            NPC.damage = 20;
            NPC.defense = 18;
            NPC.lifeMax = 3000;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
           //AnimationType = NPCID.Harpy;
            NPC.alpha = 255;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;

            NPC.frameCounter++;

            if (NPC.frameCounter >= 4)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
                {
                    NPC.frame.Y = 0;
                }
            }
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Perma Orbital");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 4000;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            if (NPC.alpha > 0)
            {
                NPC.alpha -= 255 / 60;
            }

            NPC parent = Main.npc[(int)NPC.ai[0]];

            NPC.ai[2] += 1f; // wave
            NPC.ai[1] += 2f; // speed
            int distance = 100 + (int)(Math.Sin(NPC.ai[2] / 60) * 30);
            double rad = NPC.ai[1] * (Math.PI / 180); // angle to radians
            NPC.position.X = parent.Center.X - (int)(Math.Cos(rad) * distance) - NPC.width / 2;
            NPC.position.Y = parent.Center.Y - (int)(Math.Sin(rad) * distance) - NPC.height / 2;
            if (!parent.active)
            {
                NPC.active = false;
            }
        }
    }
}