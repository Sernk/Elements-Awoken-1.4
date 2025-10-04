using ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Drakonite.Greater
{
    public class GreaterDrakoniteGolem : ModNPC
	{
		public override void SetDefaults()
		{
			NPC.width = 18;
			NPC.height = 40;
			NPC.damage = 50;
			NPC.defense = 30;
			NPC.lifeMax = 900;
            NPC.knockBackResist = 0.50f;
            NPC.aiStyle = 3;
            NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath3;
            NPC.value = Item.buyPrice(0, 0, 50, 0);
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];
			AIType = NPCID.Skeleton;
			AnimationType = NPCID.PossessedArmor;
            NPC.buffImmune[24] = true;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Drakoknight"); // thanks genih
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PossessedArmor];
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.GreaterDrakoniteGolem"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool underworld = (spawnInfo.SpawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.SpawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneDungeon && NPC.downedPlantBoss ? 0.06f : 0f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Buffs.Debuffs.Dragonfire>(), 100, true);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RefinedDrakonite>(), minimumDropped: 1, maximumDropped: 2));
        }
    }
}
