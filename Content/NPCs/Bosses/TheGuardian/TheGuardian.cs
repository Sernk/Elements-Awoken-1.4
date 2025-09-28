using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Projectiles.NPCProj.TheGuardian;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.TheGuardian
{
    [AutoloadBossHead]
    public class TheGuardian : ModNPC
    {
        private int projectileBaseDamage = 60;
        private float despawnTimer = 0;
        private float shootTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float dropAI
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float isTransforming
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(despawnTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            despawnTimer = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            NPC.width = 92;
            NPC.height = 152;
            NPC.aiStyle = -1;
            NPC.lifeMax = 50000;
            NPC.damage = 120;
            NPC.defense = 35;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.netAlways = true;
            NPC.scale = 1.2f;
            NPC.alpha = 255;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            Music = MusicID.GoblinInvasion;
            for (int num2 = 0; num2 < 206; num2++)
            {
                NPC.buffImmune[num2] = true;
            }
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 13;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "ElementsAwoken/Extra/Bestiary/TheGuardianBestiary",
                Scale = 1f, // Мини иконка в бестиарии
                PortraitScale = 0.7f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y += 41f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.TheGuardian"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)EAU.BalanceHP(40000, balance, bossAdjustment, 75000);
            NPC.damage = (int)EAU.BalanceDamage(120, numPlayers, balance, 160);
            NPC.defense = EAU.BalanceDefense(35, 45);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (isTransforming == 0)
            {
                if (NPC.frameCounter > 5)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 4)  // so it doesnt go over
                {
                    NPC.frame.Y = 0;
                }
            }
            else
            {
                if (NPC.frameCounter > 7)
                {
                    NPC.frame.Y = NPC.frame.Y + frameHeight;
                    NPC.frameCounter = 0.0;
                }
                if (NPC.frame.Y > frameHeight * 12)
                {
                    NPC.immortal = false;
                    NPC.SimpleStrikeNPC(9999, 0);
                }
            }
        }
        public override int SpawnNPC(int tileX, int tileY)
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            Vector2 pos = (P.Center - new Vector2(0, 900)) / 16;
            Main.NewText("F");
            return base.SpawnNPC((int)pos.X, (int)pos.Y);
        }
        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255) NPC.TargetClosest(true);
            Lighting.AddLight(NPC.Center, 1f, 1f, 1f);
            Player P = Main.player[NPC.target];

            #region despawning
            if (Main.dayTime) despawnTimer++;
            else if (!P.active || P.dead || Vector2.Distance(P.Center, NPC.Center) > 5000)
            {
                NPC.TargetClosest(true);
                if (!P.active || P.dead || Vector2.Distance(P.Center, NPC.Center) > 5000) despawnTimer++;
            }
            if (despawnTimer >= 300) NPC.active = false;
            #endregion
            if (NPC.life <= 1000)
            {
                isTransforming = 1;
                NPC.immortal = true;
                NPC.dontTakeDamage = true;
                NPC.life = 1000;
            }
            if (dropAI == 0)
            {
                NPC.alpha -= 5;
                if (NPC.alpha > 0)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                }
                if (NPC.alpha <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item69, NPC.position);
                    dropAI = 1;
                    NPC.velocity.Y = 5f;
                    shootTimer = 120; 
                }
            }
            else if (dropAI == 1)
            {
                if (NPC.velocity.Y == 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item69, NPC.position);

                    if (Main.netMode != NetmodeID.Server)
                    {
                        Player shakeP = Main.LocalPlayer;
                        MyPlayer modPlayer = shakeP.GetModPlayer<MyPlayer>();
                        if (Vector2.Distance(shakeP.Center, NPC.Center) <= 2000) modPlayer.screenshakeAmount = 8;
                    }

                    for (int k = 0; k < 200; k++)
                    {
                        int dust = Dust.NewDust(new Vector2(NPC.position.X, NPC.Center.Y + 45), NPC.width, 8, 0, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity *= 1.5f;
                    }
                    dropAI = -1;
                }
            }
            else
            {
                if (isTransforming == 0)
                {
                    #region circle shield and player movement
                    int maxDist = 1000;
                    for (int i = 0; i < 80; i++)
                    {
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                        Dust dust = Main.dust[Dust.NewDust(NPC.Center + offset, 0, 0, 6, 0, 0, 100)];
                        dust.noGravity = true;
                    }
                    for (int i = 0; i < Main.player.Length; i++)
                    {
                        Player player = Main.player[i];
                        if (player.active && !P.dead && Vector2.Distance(player.Center, NPC.Center) > maxDist)
                        {
                            Vector2 toTarget = new Vector2(NPC.Center.X - player.Center.X, NPC.Center.Y - player.Center.Y);
                            toTarget.Normalize();
                            float speed = MathHelper.Lerp(0.5f, 2.5f, (Vector2.Distance(P.Center, NPC.Center) - maxDist) / 400);
                            player.velocity += toTarget * speed;

                            player.dashDelay = 2; // to stop dashing away
                            player.grappling[0] = -1; // to stop grappling
                            player.grapCount = 0;
                            for (int p = 0; p < Main.maxProjectiles; p++)
                            {
                                if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7)
                                {
                                    Main.projectile[p].Kill();
                                }
                            }
                        }
                    }
                    #endregion
                    aiTimer++;
                    if (aiTimer > 1000f) aiTimer = 0f;
                    shootTimer--;
                    if (shootTimer <= 0 && aiTimer <= 500)
                    {
                        float Speed = 17f;
                        SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                        float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<GuardianShot>(), projectileBaseDamage, 0f, Main.myPlayer);
                        shootTimer = 50;
                    }
                    if (aiTimer >= 500)
                    {
                        if (shootTimer == 80)
                        {
                            Projectile.NewProjectile(EAU.NPCs(NPC), P.Center.X, P.Center.Y, 0f, 0f, ModContent.ProjectileType<GuardianTargeter>(), 0, 0f, Main.myPlayer, 0, P.whoAmI);
                        }
                        if (shootTimer <= 0)
                        {
                            int target = FindTargeter();
                            if (target != -1)
                            {
                                Projectile targetNPC = Main.projectile[target];
                                SoundEngine.PlaySound(SoundID.Item20, NPC.position);

                                float Speed = 15f;
                                float rotation = (float)Math.Atan2(NPC.Center.Y - targetNPC.Center.Y, NPC.Center.X - targetNPC.Center.X);
                                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<GuardianBeam>(), projectileBaseDamage + 40, 0f, Main.myPlayer);
                                targetNPC.Kill();
                            }
                            shootTimer = 100;
                        }
                    }
                    else
                    {
                        int target = FindTargeter();
                        if (target != -1)
                        {
                            Projectile targetNPC = Main.projectile[target];
                            targetNPC.Kill();
                        }
                    }
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Content/NPCs/Bosses/TheGuardian/" + GetType().Name + "_Glow").Value;
            Rectangle frame = new Rectangle(0, NPC.frame.Y, texture.Width, texture.Height / Main.npcFrameCount[NPC.type]);
            Vector2 origin = new Vector2(texture.Width * 0.5f, (texture.Height / Main.npcFrameCount[NPC.type]) * 0.5f);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            EAU.Sb.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY) + new Vector2(0,4), frame, new Color(255, 255, 255, 0), NPC.rotation, origin, NPC.scale, effects, 0.0f);
        }
        public override bool PreKill()
        {
            return false;
        }
        private int FindTargeter()
        {
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile other = Main.projectile[k];
                if (other.type == ModContent.ProjectileType<GuardianTargeter>() && other.active)
                {
                    return other.whoAmI;
                }
            }
            return -1;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
                NPC.NewNPC(EAU.NPCs(NPC), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<TheGuardianFly>());
            }
        }
        public override bool CheckDead()
        {
            Main.NewText(ModContent.GetInstance<EALocalization>().TheGuardian, Color.Orange.R, Color.Orange.G, Color.Orange.B);
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}