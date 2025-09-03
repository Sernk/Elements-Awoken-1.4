using ElementsAwoken.Content.Items.ItemSets.ToySlime;
using ElementsAwoken.Content.Projectiles.NPCProj.ToySlime;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.Loot;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.ItemSets.ToySlime
{
    [AutoloadBossHead]
    public class ToySlime : ModNPC
    {
        private int projectileBaseDamage = 15;
        private float jumpTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float state
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float brickTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float slimeSpawnLife
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 22;
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0.1f;
            NPC.damage = 25;
            NPC.defense = 8;
            NPC.lifeMax = 1200;
            AnimationType = NPCID.RainbowSlime;
            NPC.value = Item.buyPrice(0, 3, 0, 0);
            NPC.alpha = 60;
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.alpha = 75;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            Music = MusicID.Boss1;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.ToySlime"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            ]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 30;
            NPC.lifeMax = 1600;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 2000;
                NPC.damage = 35;
                NPC.defense = 10;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            float spawnchance = Main.expertMode ? 0.006f : 0.005f;  
            MyPlayer modPlayer = spawnInfo.Player.GetModPlayer<MyPlayer>();
            if (modPlayer.toySlimeChanceTimer > 0 && !NPC.AnyNPCs(NPC.type))
            {
                spawnchance = 0.3f;
            }
            return (spawnInfo.SpawnTileY < Main.worldSurface) && NPC.downedBoss3 && !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !NPC.AnyNPCs(NPCType<ToySlime>()) ? spawnchance : 0f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = NPC.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = NPC.frame.Size() * 0.5f;
            Color color = drawColor;
            EAU.Sb.Draw(Request<Texture2D>("ElementsAwoken/Content/NPCs/ItemSets/ToySlime/ToySlimeArmor").Value, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), NPC.frame, color, NPC.rotation, origin, NPC.scale * 0.9f, spriteEffects, 0);
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void OnKill()
        {
            int item = Item.NewItem(EAU.NPCs(NPC), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Gel, Main.rand.Next(10, 35));
            Main.item[item].color = new Color(0, 220, 40, 100);
            MyWorld.downedToySlime = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var _NormalDrop = new LeadingConditionRule(new EAIDRC.DropNormal());
            var _AwakenedModeEssence = new LeadingConditionRule(new EAIDRC.DropAwakened());
            var _AwakenedModeExpert = new LeadingConditionRule(new EAIDRC.DropExpert());
            var _AwakenedMod = new LeadingConditionRule(new EAIDRC.AwakenedModeActive());

            _NormalDrop.OnSuccess(ItemDropRule.Common(ItemType<BrokenToys>(), minimumDropped: 10, maximumDropped: 12));
            npcLoot.Add(_NormalDrop);
            _AwakenedModeExpert.OnSuccess(ItemDropRule.Common(ItemType<BrokenToys>(), minimumDropped: 10, maximumDropped: 25));
            npcLoot.Add(_AwakenedModeExpert);
            _AwakenedModeEssence.OnSuccess(ItemDropRule.Common(ItemType<BrokenToys>(), minimumDropped: 10, maximumDropped: 35));
            npcLoot.Add(_AwakenedModeEssence);
            _AwakenedMod.OnSuccess(ItemDropRule.Common(ItemType<ToySlimeClaw>()));
            npcLoot.Add(_AwakenedMod);

            npcLoot.Add(ItemDropRule.Common(ItemType<ToySlimeMask>(), 10));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            MyPlayer modPlayer = target.GetModPlayer<MyPlayer>();
            if (MyWorld.awakenedMode && modPlayer.toySlimed < -600)
            {
                modPlayer.toySlimedID = NPC.whoAmI;
                modPlayer.toySlimed = 180;
                SoundEngine.PlaySound(SoundID.Item95, NPC.Center);
            }
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];

            #region despawning
            if (!P.active || P.dead)
            {
                NPC.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    NPC.localAI[0]++;
                }
            }
            if (P.active && !P.dead) NPC.localAI[0] = 0;
            if (NPC.localAI[0] >= 300) NPC.active = false;
            #endregion

            float num234 = 1f;
            bool flag8 = false;
            bool hideSlime = false;
            if (slimeSpawnLife == 0f && NPC.life > 0)
            {
                slimeSpawnLife = (float)NPC.lifeMax;
            }
            if (NPC.localAI[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                jumpTimer = -100f;
                NPC.localAI[3] = 1f;
                NPC.TargetClosest(true);
                NPC.netUpdate = true;
            }
            // despawning
            if (Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead)
                {
                    NPC.timeLeft = 0;
                    if (Main.player[NPC.target].Center.X < NPC.Center.X)
                    {
                        NPC.direction = 1;
                    }
                    else
                    {
                        NPC.direction = -1;
                    }
                }
            }
            
            if (state == 5f)
            {
                flag8 = true;
                jumpTimer++;

                num234 = MathHelper.Clamp((60f - jumpTimer) / 60f, 0f, 1f);
                num234 = 0.5f + num234 * 0.5f;
                if (jumpTimer >= 60f)
                {
                    hideSlime = true;
                }
                if (jumpTimer >= 60f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.Bottom = new Vector2(NPC.localAI[1], NPC.localAI[2]);
                    state = 6f;
                    jumpTimer = 0f;
                    NPC.netUpdate = true;
                }
                if (Main.netMode == 1 && jumpTimer >= 120f)
                {
                    state = 6f;
                    jumpTimer = 0f;
                }
                if (!hideSlime)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(NPC.position + Vector2.UnitX * -20f, NPC.width + 40, NPC.height, 4, NPC.velocity.X, NPC.velocity.Y, 150, new Color(0, 220, 40, 100), 2f)];
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            else if (state == 6f)
            {
                flag8 = true;
                jumpTimer++;

                num234 = MathHelper.Clamp(jumpTimer / 30f, 0f, 1f);
                num234 = 0.5f + num234 * 0.5f;
                if (jumpTimer >= 30f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    state = 0f;
                    jumpTimer = 0f;
                    NPC.netUpdate = true;
                    NPC.TargetClosest(true);
                }
                if (Main.netMode == 1 && jumpTimer >= 60f)
                {
                    state = 0f;
                    jumpTimer = 0f;
                    NPC.TargetClosest(true);
                }
                for (int k = 0; k < 10; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(NPC.position + Vector2.UnitX * -20f, NPC.width + 40, NPC.height, 4, NPC.velocity.X, NPC.velocity.Y, 150, new Color(0, 220, 40, 100), 2f)];
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
            }
            NPC.dontTakeDamage = (NPC.hide = hideSlime);
            if (NPC.velocity.Y == 0f) // if its on the ground
            {
                NPC.velocity.X = NPC.velocity.X * 0.8f;
                if (NPC.velocity.X > -0.1 && NPC.velocity.X < 0.1)
                {
                    NPC.velocity.X = 0f;
                }
                if (!flag8)
                {
                    // jumps faster when low health
                    jumpTimer += 2f;                   
                    if (NPC.life < NPC.lifeMax * 0.8)
                    {
                        jumpTimer += 1f;
                    }
                    if (NPC.life < NPC.lifeMax * 0.6)
                    {
                        jumpTimer += 1f;
                    }
                    if (NPC.life < NPC.lifeMax * 0.4)
                    {
                        jumpTimer += 2f;
                    }
                    if (NPC.life < NPC.lifeMax * 0.2)
                    {
                        jumpTimer += 3f;
                    }
                    if (NPC.life < NPC.lifeMax * 0.1)
                    {
                        jumpTimer += 4f;
                    }
                    if (jumpTimer >= 0f)
                    {
                        NPC.netUpdate = true;
                        NPC.TargetClosest(true);
                        if (state == 3f) // big jump
                        {
                            NPC.velocity.Y = -13f;
                            NPC.velocity.X = NPC.velocity.X + 3.5f * NPC.direction;
                            jumpTimer = -200f;
                            state = 0f;
                        }
                        else if (state == 2f) // small jump
                        {
                            NPC.velocity.Y = -6f;
                            NPC.velocity.X = NPC.velocity.X + 4.5f * NPC.direction;
                            jumpTimer = -120f;
                            state += 1f;
                        }
                        else // medium jump
                        {
                            NPC.velocity.Y = -8f;
                            NPC.velocity.X = NPC.velocity.X + 4f * NPC.direction;
                            jumpTimer = -120f;
                            state += 1f;
                        }
                    }
                }
            }
            else if (NPC.target < 255 && ((NPC.direction == 1 && NPC.velocity.X < 3f) || (NPC.direction == -1 && NPC.velocity.X > -3f)))
            {
                if ((NPC.direction == -1 && NPC.velocity.X < 0.1) || (NPC.direction == 1 && NPC.velocity.X > -0.1))
                {
                    NPC.velocity.X = NPC.velocity.X + 0.2f * (float)NPC.direction;
                }
                else
                {
                    NPC.velocity.X = NPC.velocity.X * 0.93f;
                }
            }
            Dust dust1 = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, NPC.velocity.X, NPC.velocity.Y, 255, new Color(0, 220, 40, 100), NPC.scale * 1.2f)];
            dust1.noGravity = true;
            dust1.velocity *= 0.5f;
            if (NPC.life > 0)
            {
                float npcLife = (float)NPC.life / (float)NPC.lifeMax;
                npcLife = npcLife * 0.5f + 0.75f;
                npcLife *= num234;
                if (npcLife != NPC.scale)
                {
                    NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
                    NPC.position.Y = NPC.position.Y + (float)NPC.height;
                    NPC.scale = npcLife;
                    NPC.width = (int)(74f * NPC.scale);
                    NPC.height = (int)(50f * NPC.scale);
                    NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
                    NPC.position.Y = NPC.position.Y - (float)NPC.height;
                }
            }
            // spawn slimes
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
                int type = NPCType<MiniToySlime>();
                int spawnRate = (int)(NPC.lifeMax * 0.05);
                if (NPC.life + spawnRate < slimeSpawnLife)
                {
                    slimeSpawnLife = NPC.life; // assign the current life
                    int numSlimes = Main.rand.Next(1, 2);
                    if (Main.expertMode) numSlimes += Main.rand.Next(1, 2);
                    if (MyWorld.awakenedMode) numSlimes += Main.rand.Next(1, 2);
                    for (int i = 0; i < numSlimes; i++)
                    {
                        NPC slime = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)spawnAt.X, (int)spawnAt.Y, type)];
                        slime.ai[2] = 10000; // to stop it shooting bricks in awakened
                        MiniToySlime toyBoi = (MiniToySlime)slime.ModNPC;
                        toyBoi.dropBlocks = false;
                        NPC.netUpdate = true;
                    }
                }
            }
            if (Main.expertMode)
            {
                brickTimer--;
                if (NPC.life < NPC.lifeMax * 0.5)
                {
                    brickTimer--;
                }
                if (MyWorld.awakenedMode)
                {
                    if (NPC.life < NPC.lifeMax * 0.25)
                    {
                        brickTimer--;
                    }
                    if (NPC.life < NPC.lifeMax * 0.1)
                    {
                        brickTimer--;
                    }
                }
                if (brickTimer <= 0)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y - 10, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-6, -2), ProjectileType<LegoBrick>(), projectileBaseDamage, 0f, Main.myPlayer, 0, 0);
                    }
                    brickTimer = Main.rand.Next(200, 350);
                    if (MyWorld.awakenedMode)
                    {
                        brickTimer = Main.rand.Next(150, 300);
                    }
                }
                NPC.netUpdate = true;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, hit.HitDirection, -1f, 0, new Color(0, 220, 40, 100), 1f);
            }
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, 4, (float)(2 * hit.HitDirection), -2f, NPC.alpha, new Color(0, 220, 40, 100), 1f);
                }
            }
        }
    }
}