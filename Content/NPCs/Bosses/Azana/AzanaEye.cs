using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Projectiles;
using ElementsAwoken.Content.Projectiles.NPCProj.Azana;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.NPCs.Bosses.Azana
{
    [AutoloadBossHead]
    public class AzanaEye : ModNPC
    {
        int projectileBaseDamage = 0;
        private float dashAI
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float aiState
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float dashTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float attackCool
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override string Texture
        {
            get
            {
                if (ElementsAwoken.aprilFools) return "ElementsAwoken/Content/NPCs/Bosses/Azana/AzanaFools";
                return "ElementsAwoken/Content/NPCs/Bosses/Azana/AzanaEye";
            }
        }
        public override string BossHeadTexture  => "ElementsAwoken/Content/NPCs/Bosses/Azana/AzanaEye_Head_Boss";
        public override void SetDefaults()
        {
            NPC.lifeMax = 300000;
            NPC.damage = 150;
            NPC.defense = 60;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.width = 126;
            NPC.height = 116;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.scale = 1f;
            NPC.HitSound = SoundID.NPCHit8;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 1f;
            Music = MusicID.Boss2;
            NPC.takenDamageMultiplier = 2f;
            NPCsGLOBAL.ImmuneAllEABuffs(NPC);
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            //bossBag = mod.ItemType("AzanaBag");
            NPCID.Sets.TrailCacheLength[NPC.type] = 8;
            NPCID.Sets.TrailingMode[NPC.type] = 3;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = (int)EAU.BalanceDamage(150, balance, bossAdjustment, 300);
            NPC.lifeMax = (int)EAU.BalanceHP(300000, balance, bossAdjustment, 600000);
            NPC.defense = EAU.BalanceDefense(60, 75);
        }
        public override bool CheckDead()
        {
            if (dashAI > -1)
            {
                dashAI = -300;
                NPC.ai[1] = NPC.Center.X;
                NPC.ai[2] = NPC.Center.Y;
                NPC.damage = 0;
                NPC.life = NPC.lifeMax;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, TextureAssets.Npc[NPC.type].Value.Height * 0.5f);
            SpriteEffects spriteEffects = NPC.direction != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = NPC.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY); 
                float alpha = 1 - ((float)k / (float)NPC.oldPos.Length);
                Color color = Color.Lerp(NPC.GetAlpha(drawColor), new Color(196, 58, 76), (float)k / (float)NPC.oldPos.Length) * alpha;
                EAU.Sb.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, null, color, NPC.oldRot[k], drawOrigin, NPC.scale, spriteEffects, 0f);
            }
            DateTime now = DateTime.Today;
            if (ElementsAwoken.aprilFools) return true;
            var texture = TextureAssets.Npc[NPC.type].Value;
            var frame = texture.Frame();
            var origin = frame.Size() * 0.5f;
            EAU.Sb.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - Main.screenPosition, frame, drawColor, NPC.rotation, origin, NPC.scale, spriteEffects, 0f);
            EAU.Sb.Draw(Request<Texture2D>("ElementsAwoken/Content/NPCs/Bosses/Azana/AzanaEye_Glow").Value, NPC.Center - Main.screenPosition, frame, Color.White, NPC.rotation, origin, NPC.scale, spriteEffects, 0f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "ElementsAwoken/Content/NPCs/Bosses/Azana/AzanaEye",
                Scale = 0.6f, // Мини иконка в бестиарии
                PortraitScale = 0.8f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y += 0f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.AzanaEyeBestiary"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<ChaoticGaze>(), 2));
        }
        public override void OnKill()
        {
            if (ElementsAwoken.aprilFools) Main.NewText("April Fools :)", new Color(235, 70, 106));
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
        public override void AI()
        {
            var e = ModContent.GetInstance<EALocalization>();
            Player P = Main.player[NPC.target];
            if (Main.masterMode)
            {
                if (MyWorld.awakenedMode) projectileBaseDamage = 11;
                else projectileBaseDamage = 8;
            }
            if (Main.expertMode)
            {
                if (MyWorld.awakenedMode) projectileBaseDamage = 53;
                else projectileBaseDamage = 50;
            }
            else projectileBaseDamage = 60;
            NPC.TargetClosest(true);
             if (!ElementsAwoken.aprilFools)   Lighting.AddLight(NPC.Center, ((255 - NPC.alpha) * 0.9f) / 255f, ((255 - NPC.alpha) * 0.1f) / 255f, ((255 - NPC.alpha) * 0f) / 255f);
            else Lighting.AddLight(NPC.Center, ((255 - NPC.alpha) * 0.9f) / 255f, ((255 - NPC.alpha) * 0.9f) / 255f, ((255 - NPC.alpha) * 0.9f) / 255f);
            NPC.spriteDirection = NPC.direction;

            if (dashAI < 0)
            {
                dashAI++;
                NPC.Center = new Vector2(NPC.ai[1], NPC.ai[2]);
                int shake = 0;
                if (dashAI > -240 && dashAI < -210) shake = 2;
                else if (dashAI > -180 && dashAI < -150) shake = 3;
                else if(dashAI > -120 && dashAI < -90) shake = 4;
                else if(dashAI > -60) shake = 6;
                NPC.Center += Main.rand.NextVector2Square(-shake, shake);
                if (dashAI == -1)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC aza = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Azana>(), NPC.whoAmI)];
                        aza.Center = NPC.Center;
                        aza.netUpdate = true;
                    }
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead();
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 pos = new Vector2(NPC.position.X + Main.rand.Next(NPC.width), NPC.position.Y + Main.rand.Next(NPC.height));
                        Vector2 vel = NPC.Center - pos;
                        vel.Normalize();
                        vel *= 12f;
                        Projectile.NewProjectile(EAU.NPCs(NPC), pos, vel, ProjectileType<AzanaBlood>(), 0, projectileBaseDamage, Main.myPlayer);
                    }
                }
            }
            else
            { 
                if (NPC.life <= NPC.lifeMax * 0.5f && NPC.localAI[1] == 0)
                {
                    Main.NewText(e.AzanaEye, new Color(235, 70, 106));
                    NPC.localAI[1]++;
                }
                if (NPC.life <= NPC.lifeMax * 0.25f && NPC.localAI[1] == 1)
                {
                    Main.NewText(e.AzanaEye1, new Color(235, 70, 106));
                    NPC.localAI[1]++;
                }
                float num1 = NPC.position.X + (float)(NPC.width / 2) - Main.player[NPC.target].position.X - (float)(Main.player[NPC.target].width / 2);
                float num2 = NPC.position.Y + (float)NPC.height - 59f - Main.player[NPC.target].position.Y - (float)(Main.player[NPC.target].height / 2);
                float num3 = (float)Math.Atan2((double)num2, (double)num1) + 1.57f;
                if (num3 < 0f)
                {
                    num3 += 6.283f;
                }
                else if ((double)num3 > 6.283)
                {
                    num3 -= 6.283f;
                }
                float num4 = 0.15f; // speed?
                if (NPC.rotation < num3)
                {
                    if ((double)(num3 - NPC.rotation) > 3.1415)
                    {
                        NPC.rotation -= num4;
                    }
                    else
                    {
                        NPC.rotation += num4;
                    }
                }
                else if (NPC.rotation > num3)
                {
                    if ((double)(NPC.rotation - num3) > 3.1415)
                    {
                        NPC.rotation += num4;
                    }
                    else
                    {
                        NPC.rotation -= num4;
                    }
                }
                if (NPC.rotation > num3 - num4 && NPC.rotation < num3 + num4)
                {
                    NPC.rotation = num3;
                }
                if (NPC.rotation < 0f)
                {
                    NPC.rotation += 6.283f;
                }
                else if ((double)NPC.rotation > 6.283)
                {
                    NPC.rotation -= 6.283f;
                }
                if (NPC.rotation > num3 - num4 && NPC.rotation < num3 + num4)
                {
                    NPC.rotation = num3;
                }
                if (!P.active || P.dead)
                {
                    NPC.TargetClosest(true);
                    if (!P.active || P.dead)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - 0.04f;
                        NPC.timeLeft--;
                        if (NPC.timeLeft <= 0) NPC.active = false;
                        if (NPC.timeLeft > 200)
                        {
                            NPC.timeLeft = 200;
                            return;
                        }
                    }
                }
                else
                {
                    if (aiState == 0f) // sideways movement
                    {
                        NPC.TargetClosest(true);
                        float num424 = 12f;
                        float speed = 0.1f;
                        int side = 1;
                        if (NPC.position.X + (float)(NPC.width / 2) < Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width)
                        {
                            side = -1;
                        }
                        Vector2 vector44 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                        float targetX = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) + (float)(side * 400) - vector44.X;
                        float targetY = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector44.Y;
                        float targetPos = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                        targetPos = num424 / targetPos;
                        targetX *= targetPos;
                        targetY *= targetPos;
                        if (NPC.velocity.X < targetX)
                        {
                            NPC.velocity.X = NPC.velocity.X + speed;
                            if (NPC.velocity.X < 0f && targetX > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + speed;
                            }
                        }
                        else if (NPC.velocity.X > targetX)
                        {
                            NPC.velocity.X = NPC.velocity.X - speed;
                            if (NPC.velocity.X > 0f && targetX < 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X - speed;
                            }
                        }
                        if (NPC.velocity.Y < targetY)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + speed;
                            if (NPC.velocity.Y < 0f && targetY > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + speed;
                            }
                        }
                        else if (NPC.velocity.Y > targetY)
                        {
                            NPC.velocity.Y = NPC.velocity.Y - speed;
                            if (NPC.velocity.Y > 0f && targetY < 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - speed;
                            }
                        }

                        dashTimer += 1f;
                        if (dashTimer >= 600f) // 10 seconds of shooting
                        {
                            aiState = 1f;
                            dashTimer = 0f;
                            attackCool = 0f;
                            NPC.target = 255;
                            NPC.netUpdate = true;
                        }

                        attackCool += 1f;
                        if (NPC.life < NPC.lifeMax * 0.8)
                        {
                            attackCool += 0.3f;
                        }
                        if (NPC.life < NPC.lifeMax * 0.6)
                        {
                            attackCool += 0.3f;
                        }
                        if (NPC.life < NPC.lifeMax * 0.4)
                        {
                            attackCool += 0.3f;
                        }
                        if (NPC.life < NPC.lifeMax * 0.2)
                        {
                            attackCool += 0.3f;
                        }
                        if (NPC.life < NPC.lifeMax * 0.1)
                        {
                            attackCool += 0.3f;
                        }
                        if (Main.expertMode)
                        {
                            attackCool += 0.5f;
                        }

                        if (attackCool >= 60f && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int numberProjectiles = 3;
                            SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                            Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                            int type = ProjectileType<AzanaMiniBlast>();
                            int damage = projectileBaseDamage;
                            float Speed = 14f;
                            float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                                Projectile.NewProjectile(EAU.NPCs(NPC), vector8.X, vector8.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, 0f, Main.myPlayer, 0f, 0f);
                            }
                            attackCool = 0f;
                        }
                    }
                    else if (aiState == 1f) // dash
                    {
                        if (dashAI == 0f)
                        {
                            NPC.rotation = num3;
                            float dashSpeed = 14f;
                            if (Main.expertMode)
                            {
                                dashSpeed += 4f;
                            }
                            float targetX = P.Center.X - NPC.Center.X;
                            float targetY = P.Center.Y - NPC.Center.Y;
                            float num27 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
                            num27 = dashSpeed / num27;
                            NPC.velocity.X = targetX * num27;
                            NPC.velocity.Y = targetY * num27;
                            dashAI = 1f;
                            NPC.netUpdate = true;
                        }
                        else if (dashAI == 1f)
                        {
                            dashTimer += 1f;
                            if (NPC.life < NPC.lifeMax * 0.8) dashTimer += 0.5f;
                            if (NPC.life < NPC.lifeMax * 0.6) dashTimer += 0.5f;
                            if (NPC.life < NPC.lifeMax * 0.4) dashTimer += 0.5f;
                            if (NPC.life < NPC.lifeMax * 0.2) dashTimer += 0.5f;
                            if (NPC.life < NPC.lifeMax * 0.1) dashTimer += 0.5f;
                            if (Main.expertMode) dashTimer += 1f;
                            if (dashTimer >= 40f)
                            {
                                NPC.velocity *= 0.99f;
                                if (Main.expertMode)
                                {
                                    NPC.velocity *= 0.95f;
                                }
                                if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                                {
                                    NPC.velocity.X = 0f;
                                }
                                if ((double)NPC.velocity.Y > -0.1 && (double)NPC.velocity.Y < 0.1)
                                {
                                    NPC.velocity.Y = 0f;
                                }
                            }
                            else
                            {
                                NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
                            }
                            int dashTime = 80;
                            if (Main.expertMode)
                            {
                                dashTime = 60;
                            }
                            if (dashTimer >= (float)dashTime)
                            {
                                attackCool += 1f;
                                dashTimer = 0f;
                                NPC.target = 255;
                                NPC.rotation = num3;
                                if (attackCool >= 10f)
                                {
                                    dashAI = 0f;
                                    aiState = 0f;
                                    dashTimer = 0f;
                                    attackCool = 0f;
                                }
                                else
                                {
                                    dashAI = 0f;
                                }
                            }
                        }
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}