using ElementsAwoken.Content.Items.Banners;
using ElementsAwoken.Content.Items.ItemSets.Stellarium;
using ElementsAwoken.Content.Projectiles.NPCProj;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Stellarium
{
    public class StellarEntity : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 20;
            NPC.damage = 80;
            NPC.defense = 15;
            NPC.lifeMax = 800;
            NPC.knockBackResist = 0.65f;
            NPC.aiStyle = 5;
            AIType = NPCID.EaterofSouls;
            NPC.noGravity = true;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<StellarEntityBanner>();
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.StellarEntity"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky]);
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

            if (NPC.localAI[1] == 0)
            {
                int orbitalCount = 10;
                for (int l = 0; l < orbitalCount; l++)
                {
                    int distance = 360 / orbitalCount;
                    int orbital = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<StellarEntityOrbital>(), NPC.damage, 0f, Main.myPlayer, l * distance, NPC.whoAmI);
                }
                NPC.localAI[1]++;
            }
        }
    }
}