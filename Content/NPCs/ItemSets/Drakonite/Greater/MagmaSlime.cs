using ElementsAwoken.Content.Items.ItemSets.Drakonite.Refined;
using ElementsAwoken.Content.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Drakonite.Greater
{
    public class MagmaSlime : ModNPC
    {
        public float spikeTimer = 60f;

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 50;
            NPC.damage = 64;
            NPC.defense = 18;
            NPC.lifeMax = 600;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.knockBackResist = 0.5f;
            AIType = 1;
            NPC.aiStyle = 1;
            AnimationType = NPCID.BlueSlime;
            NPC.buffImmune[24] = true;
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
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.MagmaSlime"),
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
        public override void AI()
        {
            Vector3 RGB = new Vector3(2.0f, 0.75f, 1.5f);
            float multiplier = 1;
            float max = 2.25f;
            float min = 1.0f;
            RGB *= multiplier;
            if (RGB.X > max)
            {
                multiplier = 0.5f;
            }
            if (RGB.X < min)
            {
                multiplier = 1.5f;
            }
            if (spikeTimer > 0f)
            {
                spikeTimer -= 1f;
            }
            int damage = 40;
            if (!NPC.wet && !Main.player[NPC.target].npcTypeNoAggro[NPC.type])
            {
                Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num14 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector3.X;
                float num15 = Main.player[NPC.target].position.Y - vector3.Y;
                float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                if (Main.expertMode && num16 < 200f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
                {
                    NPC.ai[0] = -40f;
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer == 0f)
                    {
                        for (int n = 0; n < 5; n++)
                        {
                            Vector2 vector4 = new Vector2((float)(n - 2), -4f);
                            vector4.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector4.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector4.Normalize();
                            vector4 *= 6f + (float)Main.rand.Next(-50, 51) * 0.01f;
                            Projectile.NewProjectile(EAU.NPCs(NPC), vector3.X, vector3.Y, vector4.X, vector4.Y, ModContent.ProjectileType<MagmaSlimeMagma>(), damage, 0f, Main.myPlayer, 0f, 0f);
                            spikeTimer = 30f;
                        }
                    }
                }
                else if (num16 < 350f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
                {
                    NPC.ai[0] = -40f;
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer == 0f)
                    {
                        num15 = Main.player[NPC.target].position.Y - vector3.Y - (float)Main.rand.Next(0, 200);
                        num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                        num16 = 6.5f / num16;
                        num14 *= num16;
                        num15 *= num16;
                        spikeTimer = 50f;
                        Projectile.NewProjectile(EAU.NPCs(NPC), vector3.X, vector3.Y, num14, num15, ModContent.ProjectileType<MagmaSlimeMagma>(), damage, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
    }
}
