using ElementsAwoken.Content.Items.Banners;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Drakonite.Lesser
{
    public class DrakoniteElemental : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 36;           
            NPC.aiStyle = -1;
            NPC.damage = 15;
            NPC.defense = 8;
            NPC.lifeMax = 40;
            NPC.knockBackResist = 0.5f;
            NPC.value = Item.buyPrice(0, 0, 2, 0);
            NPC.npcSlots = 0.5f;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.buffImmune[BuffID.OnFire] = true;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DrakoniteElementalBanner>();
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 22;
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
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.DrakoniteElemental"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossAdjustment);
            NPC.damage = (int)(NPC.damage * 0.75f);
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = (int)(NPC.lifeMax * 1.75f);
                NPC.damage = (int)(NPC.damage * 1.3f);
                NPC.defense = 12;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int maxElementals =  1;
            if (MyWorld.awakenedMode) maxElementals = 2;
            bool underworld = (spawnInfo.SpawnTileY >= (Main.maxTilesY - 200));
            bool caverns = (spawnInfo.SpawnTileY >= (Main.maxTilesY * 0.4f));
            return !underworld && caverns && NPC.CountNPCS(NPC.type) < maxElementals && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneDungeon && !Main.hardMode ? 0.06f : 0f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode) target.AddBuff(BuffID.OnFire, MyWorld.awakenedMode ? 150 : 90, false);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.ItemSets.Drakonite.Regular.Drakonite>(), minimumDropped: 1, maximumDropped: 3));
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] == -1f)
            {
                NPC.frameCounter += 1.0;
                if (NPC.frameCounter > 4.0)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 21)
                {
                    NPC.frame.Y = frameHeight * 21;
                }
                else if (NPC.frame.Y < frameHeight * 13)
                {
                    NPC.frame.Y = frameHeight * 13;
                }
                NPC.rotation += NPC.velocity.X * 0.2f;
                return;
            }
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 4.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 10)
            {
                NPC.frame.Y = 0;
            }
            NPC.rotation = NPC.velocity.X * 0.1f;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.dontTakeDamage = false;

            if (MyWorld.awakenedMode) NPC.ai[3]++;
            if (MyWorld.awakenedMode && NPC.ai[3] > 600 && Collision.CanHit(NPC.Center, 1, 1, P.Center, 1, 1))
            {
                NPC.ai[0] = 5f;
                NPC.ai[1] = 0f;
                NPC.ai[3] = 0;
            }

            if (NPC.justHit && Main.netMode != 1 && ((Main.expertMode && Main.rand.Next(6) == 0) || (MyWorld.awakenedMode && Main.rand.Next(4) == 0)))
            {
                NPC.netUpdate = true;
                NPC.ai[0] = -1f;
                NPC.ai[1] = 0f;
            }
            if (NPC.ai[0] == -1f)
            {
                NPC.dontTakeDamage = true;
                NPC.noGravity = false;
                NPC.velocity.X = NPC.velocity.X * 0.98f;
                NPC.ai[1] += 1f;
                if (NPC.ai[1] >= 120f)
                {
                    NPC.ai[0] = (NPC.ai[1] = (NPC.ai[2] = 0f));
                    return;
                }
            }
            else if (NPC.ai[0] == 0f)
            {
                NPC.TargetClosest(true);
                if (Collision.CanHit(NPC.Center, 1, 1, P.Center, 1, 1))
                {
                    NPC.ai[0] = 1f;
                    return;
                }
                Vector2 toTarget = P.Center - NPC.Center;
                toTarget.Y -= (float)(P.height / 4);
                if (toTarget.Length() > 800f) // go through walls
                {
                    NPC.ai[0] = 2f;
                    return;
                }
                Vector2 center30 = NPC.Center;
                center30.X = P.Center.X;
                Vector2 vector243 = center30 - NPC.Center;
                if (vector243.Length() > 8f && Collision.CanHit(NPC.Center, 1, 1, center30, 1, 1))
                {
                    NPC.ai[0] = 3f;
                    NPC.ai[1] = center30.X;
                    NPC.ai[2] = center30.Y;
                    Vector2 center31 = NPC.Center;
                    center31.Y = P.Center.Y;
                    if (vector243.Length() > 8f && Collision.CanHit(NPC.Center, 1, 1, center31, 1, 1) && Collision.CanHit(center31, 1, 1, P.position, 1, 1))
                    {
                        NPC.ai[0] = 3f;
                        NPC.ai[1] = center31.X;
                        NPC.ai[2] = center31.Y;
                    }
                }
                else
                {
                    center30 = NPC.Center;
                    center30.Y = P.Center.Y;
                    if ((center30 - NPC.Center).Length() > 8f && Collision.CanHit(NPC.Center, 1, 1, center30, 1, 1))
                    {
                        NPC.ai[0] = 3f;
                        NPC.ai[1] = center30.X;
                        NPC.ai[2] = center30.Y;
                    }
                }
                if (NPC.ai[0] == 0f)
                {
                    NPC.localAI[0] = 0f;
                    toTarget.Normalize();
                    toTarget *= 0.5f;
                    NPC.velocity += toTarget;
                    NPC.ai[0] = 4f;
                    NPC.ai[1] = 0f;
                    return;
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                Vector2 value47 = P.Center - NPC.Center;
                float num1382 = value47.Length();
                float num1383 = 2f;
                num1383 += num1382 / 200f;
                int num1384 = 50;
                value47.Normalize();
                value47 *= num1383;
                NPC.velocity = (NPC.velocity * (float)(num1384 - 1) + value47) / (float)num1384;
                if (!Collision.CanHit(NPC.Center, 1, 1, P.Center, 1, 1))
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                    return;
                }
            }
            else if (NPC.ai[0] == 2f) // go through walls
            {
                NPC.noTileCollide = true;
                Vector2 value48 = P.Center - NPC.Center;
                float num1385 = value48.Length();
                float scaleFactor23 = 2f;
                int num1386 = 4;
                value48.Normalize();
                value48 *= scaleFactor23;
                NPC.velocity = (NPC.velocity * (float)(num1386 - 1) + value48) / (float)num1386;
                if (num1385 < 600f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.ai[0] = 0f;
                    return;
                }
            }
            else if (NPC.ai[0] == 3f)
            {
                Vector2 value49 = new Vector2(NPC.ai[1], NPC.ai[2]);
                Vector2 value50 = value49 - NPC.Center;
                float num1387 = value50.Length();
                float num1388 = 1f;
                float num1389 = 3f;
                value50.Normalize();
                value50 *= num1388;
                NPC.velocity = (NPC.velocity * (num1389 - 1f) + value50) / num1389;
                if (NPC.collideX || NPC.collideY)
                {
                    NPC.ai[0] = 4f;
                    NPC.ai[1] = 0f;
                }
                if (num1387 < num1388 || num1387 > 800f || Collision.CanHit(NPC.Center, 1, 1, P.Center, 1, 1))
                {
                    NPC.ai[0] = 0f;
                    return;
                }

            }
            else if (NPC.ai[0] == 4f)
            {
                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.velocity.X * -0.8f;
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.velocity.Y * -0.8f;
                }
                Vector2 value51;
                if (NPC.velocity.X == 0f && NPC.velocity.Y == 0f)
                {
                    value51 = P.Center - NPC.Center;
                    value51.Y -= (float)(P.height / 4);
                    value51.Normalize();
                    NPC.velocity = value51 * 0.1f;
                }
                float scaleFactor24 = 1.5f;
                float num1390 = 20f;
                value51 = NPC.velocity;
                value51.Normalize();
                value51 *= scaleFactor24;
                NPC.velocity = (NPC.velocity * (num1390 - 1f) + value51) / num1390;
                NPC.ai[1] += 1f;
                if (NPC.ai[1] > 180f)
                {
                    NPC.ai[0] = 0f;
                    NPC.ai[1] = 0f;
                }
                if (Collision.CanHit(NPC.Center, 1, 1, P.Center, 1, 1))
                {
                    NPC.ai[0] = 0f;
                }
                NPC.localAI[0] += 1f;
                if (NPC.localAI[0] >= 5f && !Collision.SolidCollision(NPC.position - new Vector2(10f, 10f), NPC.width + 20, NPC.height + 20))
                {
                    NPC.localAI[0] = 0f;
                    Vector2 center32 = NPC.Center;
                    center32.X = P.Center.X;
                    if (Collision.CanHit(NPC.Center, 1, 1, center32, 1, 1) && Collision.CanHit(NPC.Center, 1, 1, center32, 1, 1) && Collision.CanHit(P.Center, 1, 1, center32, 1, 1))
                    {
                        NPC.ai[0] = 3f;
                        NPC.ai[1] = center32.X;
                        NPC.ai[2] = center32.Y;
                        return;
                    }
                    center32 = NPC.Center;
                    center32.Y = P.Center.Y;
                    if (Collision.CanHit(NPC.Center, 1, 1, center32, 1, 1) && Collision.CanHit(P.Center, 1, 1, center32, 1, 1))
                    {
                        NPC.ai[0] = 3f;
                        NPC.ai[1] = center32.X;
                        NPC.ai[2] = center32.Y;
                        return;
                    }
                }
            }
            else if (NPC.ai[0] == 5f)
            {
                NPC.velocity *= 0.97f;
                NPC.ai[1]++;
                if (NPC.ai[1] < 180)
                {
                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, 0f, 0f, 200, default(Color), 0.5f)];
                        dust.noGravity = true;
                        dust.fadeIn = 1.3f;
                        Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                        vector.Normalize();
                        vector *= 3f;
                        dust.velocity = vector;
                        dust.position = NPC.Center - vector * 15;
                    }
                }
                else if (NPC.ai[1] == 180 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float Speed = 10f;
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile beam = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<DrakoniteElementalBeam>(), 30, 0f, 0)];
                    beam.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                }
                else if (NPC.ai[1] >= 210)
                {
                    NPC.ai[0] = 3f;
                    NPC.ai[1] = P.Center.X;
                    NPC.ai[2] = P.Center.Y;
                }
            }
        }
    }
}