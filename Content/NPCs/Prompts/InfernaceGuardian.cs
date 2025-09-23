using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Prompts
{
    public class InfernaceGuardian : ModNPC
    {
        private float aiTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float tpLocX
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float tpLocY
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float visualsAI
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.damage = 10;
            NPC.aiStyle = -1;
            NPC.width = 26;
            NPC.height = 50;
            NPC.defense = 25;
            NPC.lifeMax = 75;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.lavaImmune = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.InfernaceGuardian"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 30;
            NPC.lifeMax = 150;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = NPC.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Texture2D eyes = ModContent.Request<Texture2D>("ElementsAwoken/Content/NPCs/Prompts/InfernaceGuardianEyes").Value;
            Texture2D cast = ModContent.Request<Texture2D>("ElementsAwoken/Content/NPCs/Prompts/InfernaceGuadianCast").Value;
            Vector2 castOrigin = new Vector2(cast.Width * 0.5f, cast.Height * 0.5f);
            Vector2 eyesOrigin = new Vector2(eyes.Width * 0.5f, eyes.Height * 0.5f);
            Vector2 castAddition = NPC.direction != 1 ? new Vector2(-2, 6) : new Vector2(2, 6);
            Vector2 eyesAddition = NPC.direction != 1 ? new Vector2(6, 16) : new Vector2(12, 16);
            Vector2 castPos = NPC.position - Main.screenPosition + castOrigin + new Vector2(0f, NPC.gfxOffY) + castAddition;
            Vector2 eyesPos = NPC.position - Main.screenPosition + eyesOrigin + new Vector2(0f, NPC.gfxOffY) + eyesAddition;
            if (visualsAI < 150 && visualsAI > 90) spriteBatch.Draw(eyes, eyesPos, null, Color.White * (1 - ((visualsAI - 90) / 60)), NPC.rotation, eyesOrigin, NPC.scale, spriteEffects, 0f);
            if (visualsAI < 120 && aiTimer > 60) spriteBatch.Draw(cast, castPos, null, Color.White * (1 - ((visualsAI - 60) / 60)), visualsAI / 15f, castOrigin, NPC.scale, spriteEffects, 0f);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                string text = "";
                switch (Main.rand.Next(4))
                {
                    case 0:
                        text = ModContent.GetInstance<EALocalization>().InfernaceGuardian;
                        break;
                    case 1:
                        text = ModContent.GetInstance<EALocalization>().InfernaceGuardian1;
                        break;
                    case 2:
                        text = ModContent.GetInstance<EALocalization>().InfernaceGuardian2;
                        break;
                    case 3:
                        text = ModContent.GetInstance<EALocalization>().InfernaceGuardian3;
                        break;
                    default:
                        break;
                }
                CombatText.NewText(NPC.getRect(), Color.Orange, text);
                for (int i = 0; i < 31; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 6, hit.HitDirection * Main.rand.NextFloat(2f,30f), -Main.rand.NextFloat(-20f, 0f), 100, default(Color), 1.8f)];
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                }
            }
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (aiTimer < 120) aiTimer = 120;
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (aiTimer < 120) aiTimer = 120;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int maxNPCs = Main.expertMode ? 6 : 3;
            if (MyWorld.awakenedMode) maxNPCs = 9;

            float spawnChance = MathHelper.Lerp(0f, 0.15f, ((float)spawnInfo.Player.Center.Y - ((float)Main.maxTilesY * 16) * 0.2f) / ((float)Main.maxTilesY * 16));
            return spawnInfo.SpawnTileY > Main.maxTilesY * 0.8f &&
            !spawnInfo.Player.ZoneTowerStardust &&
            !spawnInfo.Player.ZoneTowerSolar &&
            !spawnInfo.Player.ZoneTowerVortex &&
            !spawnInfo.Player.ZoneTowerNebula &&
            !spawnInfo.PlayerInTown &&
            MyWorld.firePrompt > ElementsAwoken.bossPromptDelay && NPC.CountNPCS(NPC.type) < maxNPCs && !Main.snowMoon && !Main.pumpkinMoon ? spawnChance : 0f;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            if (P.position.Y < Main.maxTilesY * 16 * 0.25f) NPC.active = false;

            Vector2 direction = P.Center - NPC.Center;
            NPC.spriteDirection = Math.Sign(direction.X);
            NPC.velocity.X = 0f;

            aiTimer--;
            visualsAI--;
            if (Main.netMode != NetmodeID.MultiplayerClient && aiTimer == 60f)
            {
                float Speed = 2f;
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<InfernaceGuardianProj>(), 12, 0f, Main.myPlayer);
            }
            if (aiTimer <= 0f)
            {
                aiTimer = 240f;
                visualsAI = 240f;
                Teleport(P, 0);
            }
        }
        private void Teleport(Player P, int attemptNum)
        {
            int playerTileX = (int)P.position.X / 16;
            int playerTileY = (int)P.position.Y / 16;
            int npcTileX = (int)NPC.position.X / 16;
            int npcTileY = (int)NPC.position.Y / 16;
            int maxTileDist = 12;
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
    }
}