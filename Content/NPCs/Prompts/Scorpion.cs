using ElementsAwoken.EASystem.Global;
using ElementsAwoken.EAUtilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Prompts 
{
    public class Scorpion : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 34;
            NPC.damage = 15;
            NPC.defense = 3;
            NPC.lifeMax = 30;
            NPC.knockBackResist = 1f;
            NPC.aiStyle = 3;
            AnimationType = NPCID.Scorpion;
            AIType = NPCID.AnomuraFungus;
            NPC.value = Item.buyPrice(0, 0, 0, 20);
            NPC.HitSound = SoundID.NPCHit31;
            NPC.DeathSound = SoundID.NPCDeath34;
            NPC.friendly = false;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -2f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement(ModContent.GetInstance<EALocalization>().Scorpion),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 60;
            NPC.damage = 35;
            NPC.defense = 8;
            NPC.knockBackResist = 0.6f;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 120;
                NPC.damage = 45;
                NPC.defense = 12;
                NPC.knockBackResist = 0.2f;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, 561, NPC.scale);
                Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, 562, NPC.scale);
                Gore.NewGore(EAU.NPCs(NPC), NPC.position, NPC.velocity, 563, NPC.scale);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.SpawnTileY < Main.rockLayer) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            !spawnInfo.Invasion &&
            MyWorld.desertPrompt > ElementsAwoken.bossPromptDelay ? 0.065f : 0f;
        }
    }
}