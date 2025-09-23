using ElementsAwoken.Content.Items.Banners.Elementals;
using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Elementals
{
    public class WaterElemental : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 48;
            NPC.damage = 56; //change
            NPC.defense = 30; //change
            NPC.lifeMax = 300; //change
            NPC.value = Item.buyPrice(0, 0, 80, 0); //change
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 5;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            AIType = NPCID.Wraith;
            AnimationType = NPCID.Wraith;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<WaterElementalBanner>();
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
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.WaterElemental"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneBeach) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedFishron && !Main.dayTime ? 0.08f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.0f, 0.5f, 1.0f);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Slow, 60, false);
            target.AddBuff(BuffID.Wet, 60, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterEssence>(), 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Coral, 1, 0, 5));
            npcLoot.Add(ItemDropRule.Common(ItemID.Starfish, 1, 0, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Seashell, 1, 0, 1));
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossAdjustment);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
    }
}