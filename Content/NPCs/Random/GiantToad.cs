using ElementsAwoken.Content.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Random
{
    public class GiantToad : ModNPC
    {
        private float aiState
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float jumpTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float agag
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 32;
            NPC.aiStyle = -1;
            NPC.lifeMax = 3000;
            NPC.damage = 100;
            NPC.defense = 28;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath53;
            NPC.value = Item.buyPrice(0, 1, 50, 0);
            NPC.knockBackResist = 0.5f;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -2f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.GiantToad"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 4000;
            NPC.damage = 120;
            NPC.defense = 32;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 5000;
                NPC.damage = 170;
                NPC.defense = 38;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            !spawnInfo.Invasion &&
            spawnInfo.Player.ZoneJungle &&
            NPC.downedMoonlord ? 0.2f : 0f;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter += 1;
            if (NPC.velocity.Y != 0) NPC.frame.Y = 0;
            else if (aiState == 1)
            {
                if (NPC.frameCounter > 6)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y < frameHeight * 3) NPC.frame.Y = frameHeight * 3;
                if (NPC.frame.Y > frameHeight * 4) NPC.frame.Y = frameHeight * 4;
            }
            else
            {
                if (NPC.frameCounter > 20)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 2)
                {
                    NPC.frame.Y = frameHeight * 1;
                }
            }
        }
        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            if (aiState == 0)
            {
                aiTimer--;
                if (jumpTimer >= 60) NPC.velocity.X = 4 * NPC.direction;
                if (NPC.velocity.Y == 0)
                {
                    jumpTimer++;
                    NPC.TargetClosest(true);

                    if (jumpTimer == 60)NPC.velocity.Y = -7f;
                    else if (jumpTimer > 60) jumpTimer = 0;
                    NPC.velocity.X = NPC.velocity.X * 0.8f;
                    if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                    {
                        NPC.velocity.X = 0f;
                    }
                    if (aiTimer <= 0)
                    {
                        if (Vector2.Distance(NPC.Center, P.Center) < 300)
                        {
                            aiState = 1;
                            aiTimer = 0;
                        }
                    }
                }
                FallThroughPlatforms();
            }
            else
            {
                NPC.velocity.X = 0;
                aiTimer++;
                if (aiTimer == 6 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toadMouth = NPC.Center + new Vector2(12 * NPC.direction, 8);
                    float Speed = 12;
                    float rotation = (float)Math.Atan2(toadMouth.Y - P.Center.Y, toadMouth.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                    Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), toadMouth, projSpeed, ProjectileType<ToadTongue>(), 100, 0f, Main.myPlayer)];
                    proj.localAI[0] = 60;
                }
                if (aiTimer > 60)
                {
                    aiState = 0;
                    aiTimer = 120;
                }
            }
            if (NPC.wet)
            {
                NPC.TargetClosest(true);

                if (NPC.collideY)
                {
                    NPC.velocity.Y = -2f;
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
                if (NPC.velocity.X > -4 && NPC.velocity.X < 4) NPC.velocity.X += NPC.direction * 0.2f;
            }
        }
        private void FallThroughPlatforms()
        {
            Player P = Main.player[NPC.target];
            Vector2 platform = NPC.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.TileType] && NPC.Bottom.Y < P.Bottom.Y && platformTile.HasTile) NPC.position.Y += 0.3f;
        }
    }
}
