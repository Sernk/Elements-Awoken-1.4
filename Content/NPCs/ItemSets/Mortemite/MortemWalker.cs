using ElementsAwoken.Content.Items.Banners.Mortemite;
using ElementsAwoken.Content.Items.ItemSets.Mortemite;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Mortemite
{
    public class MortemWalker : ModNPC
	{
        public bool hasSpedUp = false;
		public override void SetDefaults()
		{
			NPC.aiStyle = 3;
			NPC.damage = 130;
			NPC.width = 44; //324
			NPC.height = 34; //216
			NPC.defense = 30;
			NPC.lifeMax = 1000;
			NPC.knockBackResist = 0.3f;
			AnimationType = NPCID.Zombie;
			AIType = NPCID.Zombie;
            NPC.value = Item.buyPrice(0, 1, 0, 0);
            NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<MortemWalkerBanner>();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
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
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.MortemWalker"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface, BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.SpawnTileY < Main.rockLayer) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord && !Main.dayTime ? 0.04f : 0f;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * bossAdjustment);
			NPC.damage = (int)(NPC.damage * 0.6f);
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
        public override void AI()
        {
            if (!hasSpedUp)
            {
                NPC.velocity *= 2f;
                hasSpedUp = true;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MortemiteDust>(), minimumDropped: 1, maximumDropped: 2));
        }
    }
}