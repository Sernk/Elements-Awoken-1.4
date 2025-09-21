using ElementsAwoken.Content.Items.Banners.VoidEvent;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.VoidStone;
using ElementsAwoken.Content.Items.VoidEventItems;
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
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1
{
    public class ZergCaster : ModNPC
    {
        public bool casting = false;
        private float shootTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float teleportTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float tpLocX
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float tpLocY
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 48;        
            NPC.damage = 45;
            NPC.defense = 25;
            NPC.lifeMax = 500;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<ZergCasterBanner>();
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            SpawnModBiomes = [ModContent.GetInstance<DOTVBiome>().Type];
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage = NPCsGLOBAL.ReducePierceDamage(modifiers.SourceDamage, projectile);
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            Main.npcFrameCount[NPC.type] = 2;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.ZergCaster")]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 900;
            NPC.defense = 35;
            NPC.damage = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 1750;
                NPC.defense = 45;
                NPC.damage = 60;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CastersCurse>(), 40));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidStone>(), 2, 3, 5));
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];

            Vector2 direction = P.Center - NPC.Center;
            NPC.spriteDirection = Math.Sign(direction.X);
            NPC.velocity.X = 0f;

            if (shootTimer > 0f)shootTimer -= 1f;

             casting = shootTimer <= 30;

            if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer <= 0f)
            {
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);

                float Speed = 8f;
                float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<ZergFireball>(), 30, 0f, 0);
                shootTimer = 120f;
            }

            teleportTimer--;
            if (teleportTimer <= 0f)
            {
                teleportTimer = 400f;
                Teleport(P, 0);
            }
        }
        private void Teleport(Player P, int attemptNum)
        {
            int playerTileX = (int)P.position.X / 16;
            int playerTileY = (int)P.position.Y / 16;
            int npcTileX = (int)NPC.position.X / 16;
            int npcTileY = (int)NPC.position.Y / 16;
            int maxTileDist = 20;
            bool foundNewLoc = false;
            int targetX = Main.rand.Next(playerTileX - maxTileDist, playerTileX + maxTileDist);
            for (int targetY = Main.rand.Next(playerTileY - maxTileDist, playerTileY + maxTileDist); targetY < playerTileY + maxTileDist; ++targetY)
            {
                if ((targetY < playerTileY - 4 ||
                    targetY > playerTileY + 4 ||
                    (targetX < playerTileX - 4 || targetX > playerTileX + 4)) &&
                    (targetY < npcTileY - 1 || targetY > npcTileY + 1 || (targetX < npcTileX - 1 || targetX > npcTileX + 1)) && Main.tile[targetX, targetY].HasUnactuatedTile)
                {
                    bool flag2 = true;
                    if ((Main.tile[targetX, targetY - 1].LiquidType == LiquidID.Lava)) flag2 = false;

                    if (flag2 && Main.tileSolid[(int)Main.tile[targetX, targetY].TileType] && !Collision.SolidTiles(targetX - 1, targetX + 1, targetY - 4, targetY - 1))
                    {
                        tpLocX = (float)targetX;
                        tpLocY = (float)targetY;
                        foundNewLoc = true;
                        break;
                    }
                    if (ModContent.GetInstance<Config>().debugMode)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(new Vector2(targetX * 16, targetY * 16), 16, 16, 62)];
                            dust.noGravity = true;
                            dust.scale = 1f;
                            dust.velocity *= 0.1f;
                        }
                    }
                }
            }
            SoundEngine.PlaySound(SoundID.Item8, NPC.position);
            if (tpLocX != 0 && tpLocY != 0 && foundNewLoc)
            {
                NPC.position.X = (float)((double)tpLocX * 16.0 - (double)(NPC.width / 2) + 8.0);
                NPC.position.Y = tpLocY * 16f - (float)NPC.height;
                NPC.netUpdate = true;

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 6)];
                    dust.noGravity = true;
                    dust.scale = 1f;
                    dust.velocity *= 0.1f;
                }
            }
            else if (attemptNum < 10) Teleport(P, attemptNum + 1);
            else ElementsAwoken.DebugModeText("Failed TP");
        }
        public override void FindFrame(int frameHeight)
        {
            if (!casting)
            {
                NPC.frame.Y = 0 * frameHeight;
            }
            if (casting)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
        }
    }
}