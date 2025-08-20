using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Volcanox
{
    public class Firefly : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.aiStyle = 86;
            NPC.damage = 150;
            NPC.width = 40; 
            NPC.height = 24;
            NPC.defense = 12;
            NPC.lifeMax = 1000;
            NPC.knockBackResist = 0.05f;
            NPC.HitSound = SoundID.NPCHit52;
            NPC.DeathSound = SoundID.NPCDeath55;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.Firefly"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 2000;
            NPC.damage = 175;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 5000;
                NPC.damage = 200;
                NPC.defense = 20;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.direction = -1;
            }
            else
            {
                NPC.direction = 1;
                NPC.spriteDirection = 1;
            }
            NPC.rotation = (float)Math.Atan2((double)(NPC.velocity.Y * (float)NPC.direction), (double)(NPC.velocity.X * (float)NPC.direction));

            NPC.frameCounter += 1;
            if (NPC.frameCounter > 5)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * (Main.npcFrameCount[NPC.type] - 1))
            {
                NPC.frame.Y = 0;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 1.0f, 0.2f, 0.7f);

            int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 400, true);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f, 0, default(Color), 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }
    }
}