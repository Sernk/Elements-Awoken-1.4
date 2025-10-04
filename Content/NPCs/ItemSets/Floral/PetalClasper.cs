using ElementsAwoken.Content.Items.Banners;
using ElementsAwoken.Content.Items.ItemSets.Floral;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Floral
{
    public class PetalClasper : ModNPC
	{
		public override void SetDefaults()
		{
			NPC.aiStyle = 3;
            AIType = NPCID.AnomuraFungus;
            AnimationType = 257;
            NPC.damage = 40;
            NPC.knockBackResist = 0.3f;
            NPC.defense = 18;
            NPC.lifeMax = 70;
            NPC.width = 44;
			NPC.height = 34;
            NPC.value = Item.buyPrice(0, 0, 1, 0);
            NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<PetalClasperBanner>();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.PetalClasper"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnChance = Main.hardMode ? 0.03f : 0.06f;
            return spawnInfo.Player.ZoneJungle &&
                !spawnInfo.Player.ZoneTowerStardust &&
                !spawnInfo.Player.ZoneTowerSolar &&
                !spawnInfo.Player.ZoneTowerVortex &&
                !spawnInfo.Player.ZoneTowerNebula &&
                !spawnInfo.PlayerInTown &&
                NPC.downedQueenBee && !Main.snowMoon && !Main.pumpkinMoon ? spawnChance : 0f;
        }	
		public override void HitEffect(NPC.HitInfo hit)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 32, hit.HitDirection, -1f, 0, default(Color), 1f);
			}
			if (NPC.life <= 0)
			{
				for (int k = 0; k < 20; k++)
				{
					Dust.NewDust(NPC.position, NPC.width, NPC.height, 32, hit.HitDirection, -1f, 0, default(Color), 1f);
				}
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Petal>(), minimumDropped: 1, maximumDropped: 2));
        }
    }
}