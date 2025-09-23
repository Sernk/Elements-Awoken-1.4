using ElementsAwoken.Content.Items.BossSummons;
using ElementsAwoken.EASystem.EAPlayer;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.MutatedScorpion 
{
    public class MutatedScorpion : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 26;
            NPC.npcSlots = 1f;
            NPC.aiStyle = 3;
            AIType = NPCID.AnomuraFungus; 
            AnimationType = 257;
            NPC.lifeMax = 30;
            NPC.damage = 20;
            NPC.defense = 6;
            NPC.knockBackResist = 0.3f;
            NPC.value = Item.buyPrice(0, 0, 2, 0);
            NPC.rarity = 4;
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath34;
            NPC.catchItem = (short)ModContent.ItemType<WastelandSummon>();
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 60;
            NPC.damage = 35;
            NPC.defense = 8;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 120;
                NPC.damage = 45;
                NPC.defense = 12;
            }
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -2f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
            Main.npcCatchable[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.Scorpion"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
            });
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MutatedScorpion").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MutatedScorpion1").Type, NPC.scale);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MutatedScorpion2").Type, NPC.scale);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneDesert) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            NPC.downedBoss1 &&!Main.snowMoon && !Main.pumpkinMoon ? 0.055f : 0f;
        }
    }
}