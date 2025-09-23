using ElementsAwoken.Content.Items.Banners.Elementals;
using ElementsAwoken.Content.Items.Essence;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Elementals
{
    public class FireElemental : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 48;
            NPC.damage = 18; //change
            NPC.defense = 10; //change
            NPC.lifeMax = 150; //change
            NPC.value = Item.buyPrice(0, 0, 20, 0); //change
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 5;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            AIType = NPCID.Wraith;
            AnimationType = NPCID.Wraith;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<FireElementalBanner>();
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
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.FireElemental"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneUnderworldHeight) &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedBoss3 && !Main.dayTime ? 0.08f : 0f;
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 1.0f, 0.6f, 0.0f);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 60, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireEssence>(), 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.LivingFireBlock, 1, 1, 10));
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossAdjustment);
            NPC.damage = (int)(NPC.damage * 0.75f);
        }
    }
}