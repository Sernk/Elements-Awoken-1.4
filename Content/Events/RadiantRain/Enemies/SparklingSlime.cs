using CalamityMod.Items.Weapons.Rogue;
using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.ItemSets.Radia;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Events.RadiantRain.Enemies
{
    public class SparklingSlime : ModNPC
    {
        private float jumpTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float spikeTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 22;
            NPC.aiStyle = -1;
            NPC.damage = 182;
            NPC.defense = 20;
            NPC.lifeMax = 6000;
            NPC.knockBackResist = 0.2f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            NPC.scale *= 1.2f;
            SpawnModBiomes = [ModContent.GetInstance<RadiantRainBiome>().Type];
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.SparklingSlime")]);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 10000;
            NPC.defense = 50;
            NPC.damage = 210;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 12500;
                NPC.defense = 65;
                NPC.damage = 230;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Radia>()));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (jumpTimer <= 40) NPC.frameCounter++;
            if (NPC.frameCounter > 8)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 4)
            {
                NPC.frame.Y = 0;
            }
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            bool aggro = false;
            if (!Main.dayTime || NPC.life != NPC.lifeMax || Main.slimeRain)
            {
                aggro = true;
            }

            if (aiTimer > 1f)
            {
                aiTimer -= 1f;
            }
            if (NPC.wet)
            {
                if (NPC.collideY)
                {
                    NPC.velocity.Y = -2f;
                }
                if (NPC.velocity.Y < 0f && NPC.ai[3] == NPC.position.X)
                {
                    NPC.direction *= -1;
                    aiTimer = 200f;
                }
                if (NPC.velocity.Y > 0f)
                {
                    NPC.ai[3] = NPC.position.X;
                }

                if (NPC.velocity.Y > 2f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
                NPC.velocity.Y = NPC.velocity.Y - 0.5f;
                if (NPC.velocity.Y < -4f)
                {
                    NPC.velocity.Y = -4f;
                }

                if (aiTimer == 1f & aggro)
                {
                    NPC.TargetClosest(true);
                }
            }
            NPC.aiAction = 0;
            if (aiTimer == 0f)
            {
                jumpTimer = -100f;
                aiTimer = 1f;
                NPC.TargetClosest(true);
            }
            if (NPC.velocity.Y == 0f)
            {
                if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.position.X = NPC.position.X - (NPC.velocity.X + (float)NPC.direction);
                }
                if (NPC.ai[3] == NPC.position.X)
                {
                    NPC.direction *= -1;
                    aiTimer = 200f;
                }
                NPC.ai[3] = 0f;
                NPC.velocity.X = NPC.velocity.X * 0.8f;
                if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                {
                    NPC.velocity.X = 0f;
                }
                if (aggro)
                {
                    jumpTimer += 1f;
                }
                jumpTimer += 1f;

                int num19 = 0;
                if (jumpTimer >= 0f)
                {
                    num19 = 1;
                }
                if (jumpTimer >= -1000f && jumpTimer <= -500f)
                {
                    num19 = 2;
                }
                if (jumpTimer >= -2000f && jumpTimer <= -1500f)
                {
                    num19 = 3;
                }
                if (num19 > 0)
                {
                    NPC.netUpdate = true;
                    if (aggro && aiTimer == 1f)
                    {
                        NPC.TargetClosest(true);
                    }
                    if (num19 == 3)
                    {
                        NPC.velocity.Y = -8f;
                        NPC.velocity.X = NPC.velocity.X + (float)(3 * NPC.direction);
                        jumpTimer = -200f;
                        NPC.ai[3] = NPC.position.X;
                    }
                    else
                    {
                        NPC.velocity.Y = -6f;
                        NPC.velocity.X = NPC.velocity.X + (float)(2 * NPC.direction);
                        jumpTimer = -120f;
                        if (num19 == 1)
                        {
                            jumpTimer -= 1000f;
                        }
                        else
                        {
                            jumpTimer -= 2000f;
                        }
                    }
                }
                else if (jumpTimer >= -30f)
                {
                    NPC.aiAction = 1;
                    return;
                }
            }
            else if (NPC.target < 255 && ((NPC.direction == 1 && NPC.velocity.X < 3f) || (NPC.direction == -1 && NPC.velocity.X > -3f)))
            {
                if (NPC.collideX && Math.Abs(NPC.velocity.X) == 0.2f)
                {
                    NPC.position.X = NPC.position.X - 1.4f * (float)NPC.direction;
                }
                if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.position.X = NPC.position.X - (NPC.velocity.X + (float)NPC.direction);
                }
                if ((NPC.direction == -1 && (double)NPC.velocity.X < 0.01) || (NPC.direction == 1 && (double)NPC.velocity.X > -0.01))
                {
                    NPC.velocity.X = NPC.velocity.X + 0.2f * (float)NPC.direction;
                    return;
                }
                NPC.velocity.X = NPC.velocity.X * 0.93f;
            }
            if (aggro)
            {
                spikeTimer--;
                int projDamage = Main.expertMode ? MyWorld.awakenedMode ? 150 : 100 : 75;
                if (!NPC.wet && !P.npcTypeNoAggro[NPC.type])
                {
                    float num14 = P.Center.X - NPC.Center.X;
                    float num15 = P.Center.Y - NPC.Center.Y;
                    float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                    if (Main.expertMode && num16 < 200f && Collision.CanHit(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height) && NPC.velocity.Y == 0f)
                    {
                        jumpTimer = -40f;
                        if (NPC.velocity.Y == 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X * 0.9f;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer <= 0f)
                        {
                            for (int n = 0; n < 3; n++)
                            {
                                float speed = 6f;
                                Vector2 vector4 = new Vector2(0, -speed).RotatedByRandom(MathHelper.ToRadians(50));
                                Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, vector4.X, vector4.Y, ProjectileType<Projectiles.NPCProj.RadiantStar>(), NPC.damage, 0f, Main.myPlayer, 0f, 0f)];
                                proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                                spikeTimer = 60f;
                            }
                        }
                    }
                    else if (num16 < 350f && Collision.CanHit(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height) && NPC.velocity.Y == 0f)
                    {
                        jumpTimer = -40f;
                        if (NPC.velocity.Y == 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X * 0.9f;
                        }
                        if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer <= 0f)
                        {
                            num15 = P.position.Y - NPC.Center.Y - (float)Main.rand.Next(0, 200);
                            num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
                            num16 = 6.5f / num16;
                            num14 *= num16;
                            num15 *= num16;
                            spikeTimer = 30f;
                            Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, num14, num15, ProjectileType<Projectiles.NPCProj.RadiantStar>(), NPC.damage, 0f, Main.myPlayer, 0f, 0f)];
                            proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                        }
                    }
                }
            }
        }
    }
}