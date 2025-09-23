using ElementsAwoken.Content.Items.Banners.Mortemite;
using ElementsAwoken.Content.Items.ItemSets.Mortemite;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Mortemite
{
    public class SkyCrawler : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.npcSlots = 0.5f;
            NPC.damage = 200;
            NPC.width = 26; //324
            NPC.height = 20; //216
            NPC.defense = 15;
            NPC.lifeMax = 1000;
            NPC.knockBackResist = 0.65f;
            NPC.value = Item.buyPrice(0, 1, 0, 0);
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<SkyCrawlerBanner>();
            NPC.noGravity = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;
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
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.SkyCrawler"),
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
            !Main.snowMoon && !Main.pumpkinMoon && NPC.downedMoonlord ? 0.05f : 0f;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            ++NPC.frameCounter;
            if (NPC.frameCounter >= 16.0)
                NPC.frameCounter = 0.0;
            NPC.frame.Y = frameHeight * (int)(NPC.frameCounter / 4.0);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MortemiteDust>(), minimumDropped: 1, maximumDropped: 2));
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];

            Lighting.AddLight(NPC.Center, 0.3f, 0.3f, 0.3f);
            if (NPC.direction == 0)
            {
                NPC.TargetClosest(true);
            }
            if (NPC.collideX)
            {
                NPC.velocity.X = NPC.velocity.X * -1f;
                NPC.direction *= -1;
            }
            if (NPC.collideY)
            {
                if (NPC.velocity.Y > 0f)
                {
                    NPC.velocity.Y = Math.Abs(NPC.velocity.Y) * -1f;
                    NPC.directionY = -1;
                    NPC.ai[0] = -1f;
                }
                else if (NPC.velocity.Y < 0f)
                {
                    NPC.velocity.Y = Math.Abs(NPC.velocity.Y);
                    NPC.directionY = 1;
                    NPC.ai[0] = 1f;
                }
            }
            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
            NPC.TargetClosest(false);
            if (!Main.player[NPC.target].dead)
            {
                NPC.velocity *= 0.98f;
                float num263 = 0.2f;
                if (NPC.velocity.X > -num263 && NPC.velocity.X < num263 && NPC.velocity.Y > -num263 && NPC.velocity.Y < num263)
                {
                    NPC.TargetClosest(true);
                    float speed = 12f;
                    Vector2 vector31 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    float targetX = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector31.X;
                    float targetY = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector31.Y;
                    float num267 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                    num267 = speed / num267;
                    targetX *= num267;
                    targetY *= num267;
                    NPC.velocity.X = targetX;
                    NPC.velocity.Y = targetY;
                    return;
                }
            }
            else
            {
                NPC.active = false;
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = 500;
            NPC.lifeMax = 1200;
        }
    }
}