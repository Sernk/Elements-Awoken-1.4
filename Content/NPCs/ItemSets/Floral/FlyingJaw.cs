using ElementsAwoken.Content.Items.Banners;
using ElementsAwoken.Content.Items.ItemSets.Floral;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Floral
{
    public class FlyingJaw : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.aiStyle = 2;
            NPC.damage = 35;
            NPC.width = 30;
            NPC.height = 32;
            NPC.defense = 15;
            NPC.lifeMax = 80;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 0, 1, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<FlyingJawBanner>();
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("FlyingJaw" + i).Type, NPC.scale);
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.direction = Math.Sign(NPC.velocity.X);
            NPC.spriteDirection = NPC.direction;
            NPC.rotation = (float)Math.Atan2((double)(NPC.velocity.Y * (float)NPC.direction), (double)(NPC.velocity.X * (float)NPC.direction));
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
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
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.FlyingJaw"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnChance = Main.hardMode ? 0.015f : 0.045f;
            return spawnInfo.Player.ZoneJungle &&
                !spawnInfo.Player.ZoneTowerStardust &&
                !spawnInfo.Player.ZoneTowerSolar &&
                !spawnInfo.Player.ZoneTowerVortex &&
                !spawnInfo.Player.ZoneTowerNebula &&
                !spawnInfo.PlayerInTown &&
                NPC.downedBoss3 && !Main.snowMoon && !Main.pumpkinMoon ? spawnChance : 0f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Petal>(), minimumDropped: 1, maximumDropped: 2));
        }
    }
}