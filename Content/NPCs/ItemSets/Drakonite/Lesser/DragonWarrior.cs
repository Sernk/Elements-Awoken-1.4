using ElementsAwoken.Content.Items.Banners;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Drakonite.Lesser
{
    public class DragonWarrior : ModNPC
	{
		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 21;
			NPC.defense = 12;
			NPC.lifeMax = 100;
            NPC.knockBackResist = 0.75f;
            NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath3;
            NPC.value = Item.buyPrice(0, 0, 2, 0);
			NPC.aiStyle = 3;
			AIType = NPCID.Skeleton;
			AnimationType = NPCID.PossessedArmor;
            NPC.buffImmune[24] = true;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DragonWarriorBanner>();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];
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
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.DragonWarrior"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            bool underworld = (spawnInfo.SpawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.SpawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneDungeon && !Main.hardMode ? 0.06f : 0f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode) target.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 150 : 90, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.ItemSets.Drakonite.Regular.Drakonite>(), minimumDropped: 1, maximumDropped: 3));
        }
    }
}
