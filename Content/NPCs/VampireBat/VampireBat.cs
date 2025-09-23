using ElementsAwoken.Content.Items.Banners;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.VampireBat
{
    public class VampireBat : ModNPC
    {
        public float shootTimer = 180f;

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 20;      
            NPC.aiStyle = 14;
            AIType = NPCID.CaveBat;
            NPC.damage = 13;
            NPC.defense = 2;
            NPC.lifeMax = 15;
            NPC.knockBackResist = 0.25f;
            AnimationType = 93;
            NPC.value = Item.buyPrice(0, 0, 2, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath4;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<VampireBatBanner>();
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 26;
            NPC.lifeMax = 40;
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
            Main.npcCatchable[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.VampireBat"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int x = spawnInfo.SpawnTileX;
            int y = spawnInfo.SpawnTileY;
            int tile = (int)Main.tile[x, y].TileType;
            bool oUnderworld = (y <= (Main.maxTilesY * 0.6f));
            bool oRockLayer = (y >= (Main.maxTilesY * 0.4f));
            return oUnderworld && oRockLayer && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneDungeon && !Main.hardMode ? 0.05f : 0f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.ChainKnife, 249));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (NPC.life + 2 < NPC.lifeMax)
            {
                NPC.life += 3;
                NPC.HealEffect(3);
            }
        }
    }
}