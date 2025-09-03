using ElementsAwoken.Content.Items.BossDrops.Azana;
using ElementsAwoken.Content.Items.Consumable.Potions;
using ElementsAwoken.Content.Projectiles.NPCProj.Azana;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.Loot;
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

namespace ElementsAwoken.Content.NPCs.Bosses.Azana
{
    [AutoloadBossHead]
    public class Azana : ModNPC
    {
        private int projectileBaseDamage = 100;
        private float circleNum = 0;
        private float targetPosX = 0;
        private float targetPosY = 0;
        private int text = 0;
        private int stopHitTimer = 0;
        private float aiState
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float moveAI
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float shootTimer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 64;
            NPC.height = 162;
            NPC.lifeMax = 1000000;
            NPC.damage = 200;
            NPC.defense = 100;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.scale = 1.3f;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 75, 0, 0);
            NPC.npcSlots = 1f;
            Music = MusicID.Boss4;
            NPCsGLOBAL.ImmuneAllEABuffs(NPC);
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * 0.5f * bossAdjustment);
            NPC.damage = 290;
            NPC.defense = 110;
            if (MyWorld.awakenedMode)
            {
                // после               // до
                NPC.lifeMax = 1300000; // 1500000 
                NPC.damage = 350;      // 350
                NPC.defense = 120;     // 130
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            NPC.spriteDirection = NPC.direction;

            if (NPC.alpha == 0)
            {
                Texture2D tex = TextureAssets.Npc[NPC.type].Value;
                Vector2 npcSize = new Vector2(NPC.width, NPC.height);
                for (int k = 0; k < NPC.oldPos.Length; k++)
                {
                    Vector2 drawPos = NPC.oldPos[k] + new Vector2(0, 3) - Main.screenPosition + new Vector2(0f, NPC.gfxOffY);
                    SpriteEffects spriteEffects = NPC.direction != -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                    float alpha = 1 - (float)k / (float)NPC.oldPos.Length;
                    Color color = Color.Lerp(NPC.GetAlpha(drawColor), new Color(196, 58, 76), (float)k / (float)NPC.oldPos.Length) * alpha;
                    EAU.Sb.Draw(tex, drawPos, NPC.frame, color, NPC.rotation, Vector2.Zero, NPC.scale, spriteEffects, 0f);
                }
            }
            return true;
        }
        //public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        //{
        //    if (modifiers.SourceDamage.Base > NPC.lifeMax / 2)
        //    {
        //        Main.NewText("Ah, the spores cannot withstand these godly powers...", new Color(235, 70, 106));
        //    }
        //    return;
        //}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit)
        {
            if (hit.Damage > NPC.lifeMax / 2)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().Azana, new Color(235, 70, 106));
            }
        }
        public override void SetStaticDefaults()
        {
            //Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 0.7f, // Мини иконка в бестиарии
                PortraitScale = 0.8f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y -= 35f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.AzanaBestiary"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule _DropNormal = new LeadingConditionRule(new EAIDRC.DropNormal());
            LeadingConditionRule _DropExpert = new LeadingConditionRule(new EAIDRC.DropAwakened());

            _DropNormal.OnSuccess(ItemDropRule.OneFromOptions(1, [.. EAList.AzaLoot]));
            _DropExpert.OnSuccess(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<AzanaBag>(), 1));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EntropicCoating>(), 7));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AzanaTrophy>(), 10));
            _DropNormal.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AzanaMask>(), 10));
            npcLoot.Add(_DropNormal);
            npcLoot.Add(_DropExpert);
        }
        public void SaveLifeForAzana()
        {
            var p = Main.LocalPlayer;
            var ns = EAU.NPCs(NPC);
            int item = Main.rand.Next(6);
            int r7 = Main.rand.Next(1, 8);
            int r10 = Main.rand.Next(1, 11);
            if (Main.expertMode)
            {
                if (Main.expertMode)
                {
                    Item.NewItem(ns, NPC.Hitbox, ModContent.ItemType<AzanaBag>());
                }
            }
            else
            {
                item = item switch
                {
                    0 => ModContent.ItemType<Anarchy>(),
                    1 => ModContent.ItemType<PurgeRifle>(),
                    2 => ModContent.ItemType<ChaoticImpaler>(),
                    3 => ModContent.ItemType<Pandemonium>(),
                    4 => ModContent.ItemType<Pandemonium>(),
                    5 => ModContent.ItemType<AzanaMinionStaff>(),
                    _ => ItemID.DirtBlock,
                };
                Item.NewItem(ns, NPC.Hitbox, item);
                if (r7 == 7)
                {
                    Item.NewItem(ns, NPC.Hitbox, ModContent.ItemType<EntropicCoating>());
                }
                if (r10 == 10)
                {
                    Item.NewItem(ns, NPC.Hitbox, ModContent.ItemType<AzanaMask>());
                    Item.NewItem(ns, NPC.Hitbox, ModContent.ItemType<AzanaTrophy>());
                }
            }
        }
        public override void OnKill()
        {
            MyWorld.downedAzana = true;
            MyWorld.sparedAzana = false;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ModContent.ItemType<EpicHealingPotion>();
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            stopHitTimer = 0;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Content.Projectiles.Other.InfectionHeart>(), 0, 0f, Main.myPlayer);
                Main.NewText(ModContent.GetInstance<EALocalization>().Azana1, new Color(235, 70, 106));
            }
        }
        public override void AI()
        {
            var e = ModContent.GetInstance<EALocalization>();
            Player P = Main.player[NPC.target];
            NPC.TargetClosest(true);
            Lighting.AddLight(NPC.Center, (255 - NPC.alpha) * 0.9f / 255f, (255 - NPC.alpha) * 0.1f / 255f, (255 - NPC.alpha) * 0f / 255f);

            if (!P.active || P.dead)
            {
                NPC.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    NPC.localAI[0]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.localAI[0] >= 300) NPC.active = false;
                }
                else NPC.localAI[0] = 0;
            }
            if (NPC.immortal)
            {
                NPC.color = new Color(196, 58, 76);
                NPC.alpha = 100;
            }
            else
            {
                NPC.color = Color.White;
                NPC.alpha = 0;
            }
            Vector2 maskCenter = new Vector2(NPC.Center.X, NPC.Center.Y - 73);

            bool canBeSpared = NPC.life <= NPC.lifeMax * 0.05f;
            if (canBeSpared) stopHitTimer++;
            bool halfLife = NPC.life <= NPC.lifeMax * 0.50f;
            bool lowLife = NPC.life <= NPC.lifeMax * 0.25f;
            #region text & stophit
            if (Main.netMode != 2)
            {
                MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

                string speech = "";
                if (NPC.life <= NPC.lifeMax * 0.90f && text == 0)
                {
                    if (modPlayer.talkToAzana) speech = e.Azana2;
                    else speech = e.Azana3;
                    Main.NewText(speech, new Color(235, 70, 106));
                    text++;
                }
                if (NPC.life <= NPC.lifeMax * 0.75f && text == 1)
                {
                    if (modPlayer.talkToAzana) speech = e.Azana4;
                    else speech = e.Azana5;
                    Main.NewText(speech, new Color(235, 70, 106));
                    text++;
                }
                if (NPC.life <= NPC.lifeMax * 0.50f && text == 2)
                {
                    if (modPlayer.talkToAzana) speech = e.Azana6;
                    else speech = e.Azana7;
                    Main.NewText(speech, new Color(235, 70, 106));
                    text++;
                }
                if (NPC.life <= NPC.lifeMax * 0.35f && text == 3)
                {
                    if (modPlayer.talkToAzana) speech = e.Azana8;
                    else speech = e.Azana9;
                    Main.NewText(speech, new Color(235, 70, 106));
                    text++;
                }
                if (NPC.life <= NPC.lifeMax * 0.2f && text == 4)
                {
                    if (modPlayer.talkToAzana) speech = e.Azana10;
                    else speech = e.Azana11;
                    Main.NewText(speech, new Color(235, 70, 106));
                    text++;
                }
                if (NPC.life <= NPC.lifeMax * 0.1f && text == 5)
                {
                    if (modPlayer.talkToAzana) speech = e.Azana12;
                    else speech = e.Azana13;
                    Main.NewText(speech, new Color(235, 70, 106));
                    text++;
                }
                if (NPC.life <= NPC.lifeMax * 0.05f && text == 6)
                {
                    speech = e.Azana14;
                    Main.NewText(speech, new Color(235, 70, 106));
                    text++;
                    stopHitTimer = 0;
                }
            }
            if (stopHitTimer == 900)
            {
                Main.NewText(e.Azana15, new Color(235, 70, 106));
            }
            else if (stopHitTimer == 1800)
            {
                Main.NewText(e.Azana16, new Color(235, 70, 106));
            }
            else if (stopHitTimer >= 2700)
            {
                Main.NewText(e.Azana17, new Color(235, 70, 106));
                if (Main.netMode != NetmodeID.MultiplayerClient) Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Content.Projectiles.Other.InfectionHeart>(), 0, 0f, Main.myPlayer);
                SaveLifeForAzana();
                MyWorld.sparedAzana = true;
                MyWorld.downedAzana = false;
                NPC.active = false;
                if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
            }
            #endregion
            if (aiTimer != 0)
            {
                if (Vector2.Distance(P.Center, NPC.Center) >= 2500) // too far
                {
                    int dist = 500;
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                    Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                    NPC.netUpdate = true;
                }
            }
            if (aiState == 0)
            {
                aiTimer++;
                Vector2 target = P.Center + new Vector2(600f * moveAI, -400);
                if (moveAI == 0) moveAI = -1;
                if (MathHelper.Distance(target.X, NPC.Center.X) <= 20)
                {
                    moveAI *= -1;
                }
                Move(P, 0.1f, target);
                shootTimer--;
                if (NPC.life <= NPC.lifeMax * 0.75f) shootTimer -= 0.5f;
                if (NPC.life <= NPC.lifeMax * 0.5f) shootTimer -= 0.5f;
                if (NPC.life <= NPC.lifeMax * 0.25f) shootTimer -= 0.5f;
                if (NPC.life <= NPC.lifeMax * 0.1f) shootTimer -= 0.5f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item21, NPC.position);
                    float speed = 18f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(rotation) * speed * -1), (float)(Math.Sin(rotation) * speed * -1), ModContent.ProjectileType<AzanaMiniBlast>(), projectileBaseDamage, 0f, Main.myPlayer, 1);
                    shootTimer = 60;
                }
                if (aiTimer >= 600)
                {
                    aiTimer = 0;
                    aiState = 1;
                    shootTimer = 0;
                }
            }
            else if (aiState == 1)
            {
                aiTimer++;
                Move(P, 0.2f, P.Center);
                shootTimer--;
                if (NPC.life <= NPC.lifeMax * 0.75f) shootTimer -= 0.5f;
                if (NPC.life <= NPC.lifeMax * 0.5f) shootTimer -= 0.5f;
                if (NPC.life <= NPC.lifeMax * 0.25f) shootTimer -= 0.5f;
                if (NPC.life <= NPC.lifeMax * 0.1f) shootTimer -= 0.5f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/GiantLaser"), NPC.position);

                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    float projSpeed = 18f;
                    Vector2 speed = new Vector2((float)(Math.Cos(rotation) * projSpeed * -1), (float)(Math.Sin(rotation) * projSpeed * -1));
                    Vector2 vector14 = speed;
                    vector14.Normalize();
                    vector14 *= 124f;

                    int numProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 5 : 3 : 2;
                    for (int i = 0; i < numProjectiles; i++)
                    {
                        float num124 = i - (numProjectiles - 1f) / 2f;
                        Vector2 vector15 = vector14.RotatedBy((double)(Math.PI / 10 * num124), default);

                        int num125 = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X + vector15.X, NPC.Center.Y + vector15.Y, speed.X, speed.Y, ModContent.ProjectileType<AzanaBeam>(), projectileBaseDamage, 0f, Main.myPlayer);
                        Main.projectile[num125].noDropItem = true;
                    }
                    shootTimer = 180;
                }
                if (aiTimer >= 600)
                {
                    aiTimer = 0;
                    aiState = 2;
                    shootTimer = 0;
                }
            }
            else if (aiState == 2)
            {
                aiTimer++;
                shootTimer--;
                NPC.velocity *= 0.96f;
                if (Main.expertMode)
                {
                    NPC.immortal = true;
                    NPC.dontTakeDamage = true;
                }

                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);

                    float speed = 18f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(rotation) * speed * -1), (float)(Math.Sin(rotation) * speed * -1), ModContent.ProjectileType<AzanaGiantCloud>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 3000;
                }

                if (aiTimer >= 600)
                {
                    aiTimer = 0;
                    aiState = 3;
                    shootTimer = 0;
                    NPC.immortal = false;
                    NPC.dontTakeDamage = false;
                }
            }
            else if (aiState == 3)
            {
                aiTimer++;
                Vector2 target = P.Center + new Vector2(400f * moveAI, 400 * moveAI);
                if (moveAI == 0) moveAI = -1;
                if (MathHelper.Distance(target.X, NPC.Center.X) <= 20)
                {
                    moveAI *= -1;
                }
                Move(P, 0.1f, target);
                shootTimer++;
                bool shoot = shootTimer % 20 == 0 && shootTimer > 0;
                if (lowLife) shoot = shootTimer % 90 == 0;
                if (shootTimer > 80 && !lowLife) shootTimer = -60;
                if (shoot && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float speed = 8;
                    if (lowLife) speed = 2;
                    float numberProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 8 : 6 : 4;
                    float rotation = MathHelper.ToRadians(360);
                    int dir = Main.rand.NextBool() ? -1 : 1;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;

                        if (lowLife)
                        {
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AzanaInfection>(), projectileBaseDamage, 2f, Main.myPlayer, P.whoAmI);
                        }
                        else
                        {
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AzanaMiniBlastSpiral>(), projectileBaseDamage, 2f, Main.myPlayer, dir);
                        }
                    }
                }
                if (aiTimer >= 420)
                {
                    aiTimer = 0;
                    aiState = 4;
                    shootTimer = 0;
                }
            }
            else if (aiState == 4)
            {
                aiTimer++;
                int numPoints = 180;
                Vector2 target = P.Center + new Vector2(0, 400).RotatedBy(circleNum * (Math.PI * 2f / numPoints));
                if (Vector2.Distance(target, NPC.Center) < 100) circleNum++;
                Move(P, 0.1f, target);
                shootTimer--; 
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float width = 1800;
                    float numProj = 10;
                    float distBetween = width / numProj;
                    for (int i = 0; i < numProj; i++)
                    {
                        Vector2 pos = new Vector2(P.Center.X - width / 2 + distBetween * i, P.Center.Y + 500);
                        Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), pos.X, pos.Y, 0, 0, ModContent.ProjectileType<AzanaLaserWarning>(), 0, 0f, Main.myPlayer)];
                        proj.localAI[1] = 125;
                    }
                    shootTimer = 180;
                }
                if (aiTimer >= 600)
                {
                    aiTimer = 0;
                    aiState = 5;
                    shootTimer = 0;
                }
            }
            else if (aiState == 5)
            {
                aiTimer++;
                Vector2 target = P.Center + new Vector2(400f * moveAI, 400 * moveAI);
                if (moveAI == 0) moveAI = -1;
                if (MathHelper.Distance(target.X, NPC.Center.X) <= 20)
                {
                    moveAI *= -1;
                }
                Move(P, 0.1f, target);
                shootTimer--;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = -1; i < 2; i += 2)
                    {
                        float posX = P.Center.X + 1000 * i;
                        float posY = P.Center.Y + Main.rand.Next(-1000, 1000);
                        Projectile.NewProjectile(EAU.NPCs(NPC), posX, posY, -18f * i, 0f, ModContent.ProjectileType<AzanaMiniBlastWave>(), projectileBaseDamage, 0f, Main.myPlayer, 1f);
                    }
                    shootTimer = 20;
                }
                if (aiTimer >= 600)
                {
                    aiTimer = 0;
                    aiState = 6;
                    shootTimer = 0;
                }
            }
            else if (aiState == 6)
            {
                aiTimer++;
                shootTimer--;
                Vector2 target = P.Center + new Vector2(400f * moveAI, 400 * moveAI);
                if (moveAI == 0) moveAI = -1;
                if (MathHelper.Distance(target.X, NPC.Center.X) <= 20)
                {
                    moveAI *= -1;
                }
                Move(P, 0.1f, target);
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                    Projectile.NewProjectile(EAU.NPCs(NPC), P.Center, Vector2.Zero, ModContent.ProjectileType<AzanaTarget>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 120;
                }
                if (aiTimer >= 600)
                {
                    aiTimer = 0;
                    aiState = 7;
                    shootTimer = 30;

                    if (!halfLife) aiState = 50;
                    else
                    {
                        int dist = 500;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                        Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                    }
                }
            }
            else if (aiState == 7)
            {
                aiTimer++;
                if (Main.expertMode)
                {
                    NPC.immortal = true;
                    NPC.dontTakeDamage = true;
                }
                NPC.velocity = Vector2.Zero;
                shootTimer--;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item78, NPC.Center);

                    float speed = 24f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Vector2 projSpeed = new Vector2((float)(Math.Cos(rotation) * speed * -1), (float)(Math.Sin(rotation) * speed * -1));
                    projSpeed = projSpeed.RotatedByRandom(MathHelper.ToRadians(30));
                    Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center, projSpeed, ModContent.ProjectileType<AzanaInfectionPillar>(), projectileBaseDamage, 0f, Main.myPlayer, 0f, 0f)];
                    proj.localAI[1] = 200;

                    shootTimer = 30;
                }
                if (aiTimer >= 600)
                {
                    aiTimer = 0;
                    aiState = 8;
                    shootTimer = 0;
                    NPC.immortal = false;
                    NPC.dontTakeDamage = false;
                }
            }
            else if (aiState == 8)
            {
                aiTimer++;
                Move(P, 0.2f, P.Center - new Vector2(0, 400));
                shootTimer--;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item95, NPC.Center);
                    int numProj = Main.expertMode ? MyWorld.awakenedMode ? 10 : 8 : 6;
                    for (int i = 0; i < numProj; i++)
                    {
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(14, 22), ModContent.ProjectileType<AzanaGlob>(), projectileBaseDamage, 0f, Main.myPlayer);
                    }
                    shootTimer = 60;
                }
                if (aiTimer >= 600)
                {
                    aiTimer = 0;
                    aiState = 9;
                    shootTimer = 0;
                }
            }
            else if (aiState == 9)
            {
                aiTimer++;
                Vector2 target = P.Center + new Vector2(600f * moveAI, -400);
                if (moveAI == 0) moveAI = -1;
                if (MathHelper.Distance(target.X, NPC.Center.X) <= 20)
                {
                    moveAI *= -1;
                }
                Move(P, 0.1f, target);
                shootTimer++;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (shootTimer == 90)
                    {
                        SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/GiantLaser"), NPC.position);

                        Vector2 speed = new Vector2(0, 1);
                        for (int i = 0; i < 4; i++)
                        {
                            speed = speed.RotatedBy(MathHelper.ToRadians(90));
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center + speed * 50, speed, ModContent.ProjectileType<AzanaBeam>(), projectileBaseDamage, 0f, Main.myPlayer);
                        }
                    }
                    else if (shootTimer == 180)
                    {
                        SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/GiantLaser"), NPC.position);

                        Vector2 speed = new Vector2(1, 1);
                        for (int i = 0; i < 4; i++)
                        {
                            speed = speed.RotatedBy(MathHelper.ToRadians(90));
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center + speed * 50, speed, ModContent.ProjectileType<AzanaBeam>(), projectileBaseDamage, 0f, Main.myPlayer);
                        }
                    }
                    else if (shootTimer == 270)
                    {
                        SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/Item/GiantLaser"), NPC.position);

                        Vector2 speed = new Vector2(0, 1);
                        for (int i = 0; i < 8; i++)
                        {
                            speed = speed.RotatedBy(MathHelper.ToRadians(45));
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center + speed * 50, speed, ModContent.ProjectileType<AzanaBeam>(), projectileBaseDamage, 0f, Main.myPlayer);
                        }
                        shootTimer = 0;
                    }
                }
                if (aiTimer >= 350)
                {
                    aiTimer = 0;
                    aiState = 10;
                    shootTimer = 0;
                    if (!lowLife) aiState = 50;
                }
            }
            else if (aiState == 10)
            {
                aiTimer++;
                NPC.velocity = Vector2.Zero;
                if (Main.expertMode)
                {
                    NPC.immortal = true;
                    NPC.dontTakeDamage = true;
                }
                shootTimer++;
                if (shootTimer > 80) shootTimer = -60;
                if (shootTimer % 20 == 0 && shootTimer > 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);

                    float shootSpeed = 8;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Vector2 speed = new Vector2((float)(Math.Cos(rotation) * shootSpeed * -1), (float)(Math.Sin(rotation) * shootSpeed * -1)).RotatedByRandom(MathHelper.ToRadians(30));

                    NPC monster = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<InfectionMouth>())];
                    monster.velocity = speed;
                }
                if (aiTimer >= 200 || canBeSpared)
                {
                    aiTimer = 0;
                    aiState = 50;
                    shootTimer = 0;
                    NPC.immortal = false;
                    NPC.dontTakeDamage = false;
                }

            }
            else if (aiState == 50) // tp spam
            {
                NPC.velocity = Vector2.Zero;
                Dust dust = Main.dust[Dust.NewDust(maskCenter, NPC.width, NPC.height, 127, 0f, 0f, 200, default(Color), 0.5f)];
                dust.noGravity = true;
                dust.fadeIn = 1.3f;
                Vector2 vector = Main.rand.NextVector2Square(-1, 1f);
                vector.Normalize();
                vector *= 5f;
                dust.velocity = vector;
                dust.position = maskCenter - vector * 25;
                aiTimer++;

                if (NPC.life <= NPC.lifeMax * 0.5f) aiTimer++;
                if (aiTimer >= 180)
                {
                    aiTimer = 0;
                    aiState = 51;
                    shootTimer = 0;
                }
            }
            else if (aiState == 51)
            {
                NPC.velocity = Vector2.Zero;
                aiTimer++;

                shootTimer--;
                int shootCD = 30;
                if (NPC.life <= NPC.lifeMax * 0.5f) shootCD = 20;
                if (NPC.life <= NPC.lifeMax * 0.1f) shootCD = 15;
                if (shootTimer == (int)(shootCD * 0.75f))
                {
                    int dist = 700;
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                    Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                }
                if (shootTimer <= 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                        float speed = Main.expertMode ? MyWorld.awakenedMode ? 8 : 7 : 5;
                        float numberProjectiles = Main.expertMode ? MyWorld.awakenedMode ? 8 : 6 : 4;
                        float rotation = MathHelper.ToRadians(360);
                        int dir = Main.rand.NextBool() ? -1 : 1;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = Vector2.One.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * speed;
                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AzanaWave>(), projectileBaseDamage, 2f, Main.myPlayer, dir);
                        }
                        shootTimer = shootCD;
                    }
                }

                if (aiTimer >= 300)
                {
                    aiTimer = 0;
                    aiState = 52;
                    shootTimer = 30;
                }
            }
            else if (aiState == 52)
            {
                NPC.velocity = Vector2.Zero;
                aiTimer++;

                shootTimer--;
                if (shootTimer == 20)
                {
                    int dist = 700;
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                    Teleport(P.Center.X + offset.X, P.Center.Y + offset.Y);
                }
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    for (int i = 0; i < 6; i++)
                    {
                        float speed = 8 + Main.rand.NextFloat(-2, 2);
                        Vector2 perturbedSpeed = new Vector2((float)(Math.Cos(rotation) * speed * -1), (float)(Math.Sin(rotation) * speed * -1)).RotatedByRandom(MathHelper.ToRadians(5));
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<AzanaMiniBlastAccelerate>(), projectileBaseDamage - 10, 0f, 0);
                    }
                    shootTimer = 50;
                }

                if (aiTimer >= 300)
                {
                    aiTimer = 0;
                    aiState = 53;
                    shootTimer = 0;
                }
            }
            else if (aiState == 53)
            {
                NPC.velocity = Vector2.Zero;
                aiTimer++;

                float shootDelay = Main.expertMode ? MyWorld.awakenedMode ? 8f : 9f : 12f;
                float moveSpeed = 10f;
                if (halfLife) shootDelay *= 0.9f;
                Vector2 diagSpeed = new Vector2();
                int distance = 500;
                //moving to top left
                if (aiTimer == 1) Teleport(P.position.X + distance, P.position.Y + distance);
                if (aiTimer >= 1 && aiTimer < 120)
                {
                    NPC.velocity = new Vector2(-8f, -8f);
                    diagSpeed = new Vector2(moveSpeed, -moveSpeed);
                }
                //moving to top right
                if (aiTimer == 120) Teleport(P.position.X - distance, P.position.Y + distance);
                if (aiTimer >= 120 && aiTimer < 240)
                {
                    NPC.velocity = new Vector2(8f, -8f);
                    diagSpeed = new Vector2(-moveSpeed, -moveSpeed);
                }
                //moving to bottom right
                if (aiTimer == 240) Teleport(P.position.X - distance, P.position.Y - distance);
                if (aiTimer >= 240 && aiTimer < 360)
                {
                    NPC.velocity = new Vector2(8f, 8f);
                    diagSpeed = new Vector2(moveSpeed, -moveSpeed);
                }                    //moving to bottom left
                if (aiTimer == 360) Teleport(P.position.X + distance, P.position.Y - distance);
                if (aiTimer >= 360)
                {
                    NPC.velocity = new Vector2(-8f, 8f);
                    diagSpeed = new Vector2(-moveSpeed, -moveSpeed);
                }
                shootTimer--;
                if (shootTimer <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item21, NPC.position);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, diagSpeed.X, diagSpeed.Y, ModContent.ProjectileType<AzanaMiniBlast>(), projectileBaseDamage, 0f, Main.myPlayer);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, -diagSpeed.X, -diagSpeed.Y, ModContent.ProjectileType<AzanaMiniBlast>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = shootDelay;
                }
                if (aiTimer >= 480)
                {
                    aiTimer = 0;
                    aiState = 54;
                    shootTimer = 0;
                }
            }
            else if (aiState == 54)
            {
                NPC.velocity = Vector2.Zero;
                aiTimer++;

                int distance = 700;
                double rad = aiTimer * (Math.PI / 180);
                NPC.Center = new Vector2(P.Center.X - (int)(Math.Cos(rad) * distance), P.Center.Y - (int)(Math.Sin(rad) * distance));

                shootTimer--;
                if (NPC.life <= NPC.lifeMax * 0.5f) shootTimer -= 0.5f;
                if (shootTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                    float speed = Main.rand.NextFloat(19, 30);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(rotation) * speed * -1), (float)(Math.Sin(rotation) * speed * -1), ModContent.ProjectileType<AzanaCloud>(), projectileBaseDamage, 0f, Main.myPlayer);
                    shootTimer = 10;
                }
                if (aiTimer >= 180)
                {
                    aiTimer = 0;
                    aiState = 55;
                    shootTimer = 0;
                }
            }
            else if (aiState == 55)
            {
                aiTimer++;
                shootTimer++;
                if (shootTimer == 1)
                {
                    NPC.velocity = Vector2.Zero;
                    int dist = 400;
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                    Teleport(P.position.X + offset.X, P.position.Y + offset.Y);
                }
                if (shootTimer == 30)
                {
                    float speed = 18f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    NPC.velocity = new Vector2((float)(Math.Cos(rotation) * speed * -1), (float)(Math.Sin(rotation) * speed * -1));
                }
                if (shootTimer == 90)
                {
                    shootTimer = 0;
                }

                if (aiTimer >= 360)
                {
                    aiTimer = 0;
                    shootTimer = 0;
                    if (MyWorld.awakenedMode) aiState = 56;
                    else
                    {
                        int dist = 700;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                        Teleport(P.position.X + offset.X, P.position.Y + offset.Y);
                        SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/NPC/AzanaPowerDown"), NPC.position);
                        aiState = 57;
                    }
                }
            }
            else if (aiState == 56)
            {
                aiTimer++;
                int aiLength = 360;
                int aiHalf = aiLength / 2;
                float moveSpeed = Main.expertMode ? MyWorld.awakenedMode ? 12f : 9f : 6f;
                float shootDelay = Main.expertMode ? MyWorld.awakenedMode ? 2f : 4f : 6f;
                if (aiTimer == 1)
                {
                    Teleport(P.Center.X + 900, P.Center.Y - 900);
                    targetPosX = P.Center.X - 600;
                    targetPosY = P.Center.Y - 4000;
                }
                if (aiTimer >= 1 && aiTimer < aiHalf)
                {
                    NPC.velocity = new Vector2(-moveSpeed, 0);
                }
                if (aiTimer == aiHalf)
                {
                    Teleport(P.Center.X - 900, P.Center.Y + 900);
                    targetPosX = P.Center.X + 600;
                    targetPosY = P.Center.Y - 4000;
                }
                if (aiTimer >= aiHalf && aiTimer < aiLength)
                {
                    NPC.velocity = new Vector2(moveSpeed, 0);
                }
                shootTimer--;
                if (shootTimer <= 0)
                {
                    SoundEngine.PlaySound(SoundID.Item90, NPC.Center);
                    float speed = aiTimer >= aiHalf ? -5 : 5;
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center, new Vector2(0, speed), ModContent.ProjectileType<AzanaBeamQuick>(), projectileBaseDamage, 0f, Main.myPlayer);
                    Projectile.NewProjectile(EAU.NPCs(NPC), new Vector2(targetPosX, targetPosY), new Vector2(0, 5), ModContent.ProjectileType<AzanaBeamQuick>(), (int)(projectileBaseDamage * 1.5f), 0f, Main.myPlayer);
                    if (P.Center.X < targetPosX && aiTimer < aiHalf || P.Center.X > targetPosX && aiTimer >= aiHalf)
                    {
                        Projectile.NewProjectile(EAU.NPCs(NPC), new Vector2(P.Center.X, targetPosY), new Vector2(0, 5), ModContent.ProjectileType<AzanaBeamQuick>(), (int)(projectileBaseDamage * 2f), 0f, Main.myPlayer);
                    }
                    shootTimer = shootDelay;
                }

                if (aiTimer >= aiLength)
                {
                    aiTimer = 0;
                    aiState = 57;
                    shootTimer = 0;

                    int dist = 700;
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * dist, (float)Math.Cos(angle) * dist);
                    Teleport(P.position.X + offset.X, P.position.Y + offset.Y);
                    SoundEngine.PlaySound(new SoundStyle("ElementsAwoken/Sounds/NPC/AzanaPowerDown"), NPC.position);
                }
            }
            else if (aiState == 57)
            {
                aiTimer++;
                if (NPC.life <= NPC.lifeMax * 0.5f) aiTimer++;
                if (NPC.life <= NPC.lifeMax * 0.25f) aiTimer++;
                NPC.velocity = Vector2.Zero;
                if (aiTimer >= 300)
                {
                    aiTimer = 0;
                    aiState = 0;
                    shootTimer = 0;
                }
            }           
        }
        public override bool CheckActive()
        {
            return false;
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - NPC.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;

            if (NPC.velocity.X < desiredVelocity.X)
            {
                NPC.velocity.X = NPC.velocity.X + speed;
                if (NPC.velocity.X < 0f && desiredVelocity.X > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X + speed;
                }
            }
            else if (NPC.velocity.X > desiredVelocity.X)
            {
                NPC.velocity.X = NPC.velocity.X - speed;
                if (NPC.velocity.X > 0f && desiredVelocity.X < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X - speed;
                }
            }
            if (NPC.velocity.Y < desiredVelocity.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y + speed;
                if (NPC.velocity.Y < 0f && desiredVelocity.Y > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed;
                }
            }
            else if (NPC.velocity.Y > desiredVelocity.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed;
                if (NPC.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed;
                }
            }
            float slowSpeed = Main.expertMode ? 0.93f : 0.95f;
            if (MyWorld.awakenedMode) slowSpeed = 0.92f;
            int xSign = Math.Sign(desiredVelocity.X);
            if (NPC.velocity.X < xSign && xSign == 1 || NPC.velocity.X > xSign && xSign == -1) NPC.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, NPC.Center.Y) > 1000)
            {
                if (NPC.velocity.X < ySign && ySign == 1 || NPC.velocity.X > ySign && ySign == -1) NPC.velocity.Y *= slowSpeed;
            }
        }
        private void Teleport(float posX, float posY)
        {
            SoundEngine.PlaySound(SoundID.Item8, NPC.Center);
            NPC.position.X = posX;
            NPC.position.Y = posY;
            int numDusts = 36;
            for (int i = 0; i < numDusts; i++)
            {
                Vector2 position = Vector2.Normalize(Vector2.One * new Vector2((float)NPC.width / 2f, (float)NPC.height) * 0.75f * 0.5f).RotatedBy((double)((i - (numDusts / 2 - 1)) * 6.28318548f / numDusts), default(Vector2)) + NPC.Center;
                Vector2 velocity = position - NPC.Center;
                int dust = Dust.NewDust(position + velocity, 0, 0, 127, velocity.X * 2f, velocity.Y * 2f, 100, default, 1.4f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
                Main.dust[dust].velocity = Vector2.Normalize(velocity) * 9f;
            }
            NPC.netUpdate = true;
        }
    }
}