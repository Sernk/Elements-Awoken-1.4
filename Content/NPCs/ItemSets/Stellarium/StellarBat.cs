using ElementsAwoken.Content.Items.Banners;
using ElementsAwoken.Content.Items.ItemSets.Stellarium;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Stellarium
{
    public class StellarBat : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 20;
            NPC.damage = 80;
            NPC.defense = 15;
            NPC.lifeMax = 800;
            NPC.knockBackResist = 0.65f;
            AnimationType = 93;
            NPC.aiStyle = 2;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<StellarBatBanner>();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.StellarBat"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossAdjustment);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneSkyHeight) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord ? 0.15f : 0f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Stellorite>(), minimumDropped: 3, maximumDropped: 6));
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.4f, 0.4f, 0.7f);
        }
    }
}