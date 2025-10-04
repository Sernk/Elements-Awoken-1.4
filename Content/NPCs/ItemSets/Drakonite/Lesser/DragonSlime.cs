using ElementsAwoken.Content.Items.Banners;
using ElementsAwoken.Content.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Drakonite.Lesser
{
    public class DragonSlime : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 30;
            NPC.aiStyle = 1;
            AIType = 1;
            AnimationType = NPCID.BlueSlime;
            NPC.damage = 24;
            NPC.defense = 6;
            NPC.lifeMax = 32;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 2, 0);
            NPC.knockBackResist = 0.5f;
            NPC.buffImmune[24] = true;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DragonSlimeBanner>();
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.DragonSlime"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground]);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool underworld = (spawnInfo.SpawnTileY >= (Main.maxTilesY - 200));
            bool rockLayer = (spawnInfo.SpawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && rockLayer && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneDungeon && !Main.hardMode ? 0.06f : 0f;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonSlime" + i).Type, NPC.scale);
                }
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode) target.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 150 : 90, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.ItemSets.Drakonite.Regular.Drakonite>(), minimumDropped: 1, maximumDropped: 3));
        }
        public override void AI()
        {
            if (NPC.localAI[1] > 0f)
            {
                NPC.localAI[1] -= 1f;
            }
            if (!NPC.wet && !Main.player[NPC.target].npcTypeNoAggro[NPC.type])
            {
                Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num14 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector3.X;
                float num15 = Main.player[NPC.target].position.Y - vector3.Y;
                float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                if (Main.expertMode && num16 < 120f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
                {
                    NPC.ai[0] = -40f;
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[1] == 0f)
                    {
                        int num = MyWorld.awakenedMode ? 5 : 3;
                        for (int n = 0; n < num; n++)
                        {
                            float speed = 4f;
                            Vector2 vector4 = new Vector2(0, -speed).RotatedByRandom(MathHelper.ToRadians(50));
                            Projectile.NewProjectile(EAU.NPCs(NPC), vector3.X, vector3.Y, vector4.X, vector4.Y, ModContent.ProjectileType<DragonSlimeSpike>(), 9, 0f, Main.myPlayer, 0f, 0f);
                            NPC.localAI[1] = 30f;
                        }
                    }
                }
                else if (num16 < 200f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
                {
                    NPC.ai[0] = -40f;
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[1] == 0f)
                    {
                        num15 = Main.player[NPC.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
                        num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                        num16 = 4.5f / num16;
                        num14 *= num16;
                        num15 *= num16;
                        NPC.localAI[1] = 50f;
                        Projectile.NewProjectile(EAU.NPCs(NPC), vector3.X, vector3.Y, num14, num15, ModContent.ProjectileType<DragonSlimeSpike>(), 9, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
    }
}