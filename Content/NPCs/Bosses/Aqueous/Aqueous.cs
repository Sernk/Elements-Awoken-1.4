using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.Aqueous;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Projectiles.NPCProj.Aqueous;
using ElementsAwoken.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Aqueous
{
    [AutoloadBossHead]
    public class Aqueous : ModNPC
    {
        public bool enraged = false;
        public float[] spinAI = new float[2];
        const int projectileBaseDamage = 55;
        Vector2 staffCenter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(spinAI[0]);
                writer.Write(spinAI[1]);
            }
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                spinAI[0] = reader.ReadSingle();
                spinAI[1] = reader.ReadSingle();
            }
        }
        public override void SetDefaults()
        {
            NPC.width = 132;
            NPC.height = 184;
            NPC.lifeMax = 75000;
            NPC.damage = 75;
            NPC.defense = 52;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit55;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 20, 0, 0);
            Music = MusicID.Boss4;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 90;
            NPC.lifeMax = 100000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 125000;
                NPC.damage = 110;
                NPC.defense = 65;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (NPC.frameCounter > 6)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 4)
            {
                NPC.frame.Y = 0;
            }
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 0.5f, // Мини иконка в бестиарии
                PortraitScale = 0.65f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y -= 38f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            var EALocalization = ModContent.GetInstance<EALocalization>();
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.AqueousBoss"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(1, [.. EAList.AquLoot]));
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<AqueousBag>(), 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterEssence>(), 1, 5, 25));
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AqueousTrophy>(), 10));
        }
        public override void OnKill()
        {
            Main.windSpeedTarget = 0.5f;
            MyWorld.downedAqueous = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData);
        }
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.alpha > 100)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            NPC.TargetClosest(true);

            enraged = false;
            staffCenter = new Vector2(NPC.Center.X + 52 * -NPC.direction, NPC.Center.Y - 80);

            if (!P.ZoneBeach) enraged = true;

            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
                {
                    NPC.localAI[0]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.localAI[0] >= 300)
                    {
                        NPC.active = false;
                    }
                }
                else
                    NPC.localAI[0] = 0;
            }
            if (NPC.life <= NPC.lifeMax * 0.65f && NPC.localAI[1] == 0)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().Aqueous, Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                SoundEngine.PlaySound(SoundID.Item123, NPC.position);
                NPC.localAI[1]++;
                NPC.ai[0] = 0f;
            }
            if (NPC.life <= NPC.lifeMax * 0.3f && NPC.localAI[1] == 1)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().Aqueous1, Color.DarkRed.R, Color.DarkRed.G, Color.DarkRed.B);
                SoundEngine.PlaySound(SoundID.Item119, NPC.position);
                NPC.localAI[1]++;
                NPC.ai[0] = 0f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0] += 1f;
                NPC.ai[1]--;
                NPC.ai[2]--;
                NPC.ai[3]--;
            }
            #region Ai 1
            if (NPC.life > NPC.lifeMax * 0.65f)
            {
                Main.StopRain();

                if (NPC.ai[0] > 1500f)
                {
                    NPC.ai[0] = 0f;
                }

                float movSpeed = 6f;
                if (Main.expertMode) movSpeed += 0.025f;
                if (MyWorld.awakenedMode) movSpeed += 0.05f;
                Move(P, 0.15f);

                if (NPC.ai[0] >= 1 && NPC.ai[0] <= 1000)
                {
                    if (NPC.ai[1] <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            AquaticBolts(P, 14f, projectileBaseDamage + 10);

                            NPC.ai[1] = enraged ? 10 : 30;
                            NPC.ai[1] += Main.rand.Next(1, 20);
                            NPC.netUpdate = true;
                        }
                    }
                }
                if (NPC.ai[0] >= 1000 && NPC.ai[0] <= 1500)
                {
                    NPC.velocity = Vector2.Zero;
                    if (NPC.ai[1] <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            WaterKnives(P, 7.5f, projectileBaseDamage, 2 + Main.rand.Next(1, 3));
                            NPC.ai[1] = enraged ? 10 : 35;
                            NPC.ai[1] += Main.rand.Next(10, 35);
                            NPC.netUpdate = true;
                        }
                    }
                }
            }
            #endregion
            #region Ai 2
            if (NPC.life <= NPC.lifeMax * 0.65f && NPC.life > NPC.lifeMax * 0.3f)
            {
                Main.StopRain();

                float movSpeed = 6f;
                if (Main.expertMode) movSpeed += 0.05f;
                if (MyWorld.awakenedMode) movSpeed += 0.075f;
                Move(P, 0.2f);

                if (NPC.ai[0] > 2500f)
                {
                    NPC.ai[0] = 0f;
                }
                if (NPC.ai[0] <= 750)
                {
                    if (NPC.ai[1] <= 30)
                    {
                        NPC.velocity.X *= 0f;
                        NPC.velocity.Y *= 0f;
                    }
                    if (NPC.ai[1] <= 25)
                    {
                        int maxdusts = 20;
                        for (int i = 0; i < maxdusts; i++)
                        {
                            float dustDistance = 100;
                            float dustSpeed = 6;
                            Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                            Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                            Dust vortex = Dust.NewDustPerfect(NPC.Center + offset, 111, velocity, 0, default(Color), 1.5f);
                            vortex.noGravity = true;
                        }
                    }
                    if (NPC.ai[1] <= 0)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            HomingKnives(P, 6f, projectileBaseDamage - 15);
                            NPC.ai[1] = enraged ? 20 : 80;

                            NPC.ai[1] += Main.rand.Next(10, 35);
                            NPC.netUpdate = true;
                        }
                    }

                }
                if (NPC.ai[0] >= 1000 && NPC.ai[0] <= 1500)
                {
                    NPC.velocity *= 0.00f;
                    if (NPC.ai[1] <= 0)
                    {
                        if (Collision.CanHit(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height))
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                WaterKnives(P, 7.5f, projectileBaseDamage, 3 + Main.rand.Next(1, 3));

                                NPC.ai[1] = enraged ? 10 : 30;
                                NPC.ai[1] += Main.rand.Next(10, 35);
                                NPC.netUpdate = true;
                            }
                        }
                    }
                }
                if (NPC.ai[0] >= 1500 && NPC.ai[0] <= 2000)
                {
                    if (NPC.ai[1] <= 0)
                    {
                        AquaticBolts(P, 14f, projectileBaseDamage + 10);

                        NPC.ai[1] = enraged ? 10 : 30;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            NPC.ai[1] += Main.rand.Next(1, 20);
                            NPC.netUpdate = true;
                        }
                    }
                }
                if (NPC.ai[0] >= 2000 && NPC.ai[0] <= 2500)
                {
                    NPC.velocity *= 0.00f;

                    SpinningAttack(P, 4f, projectileBaseDamage);
                }
                if (NPC.ai[2] <= 0)
                {
                    int damage = Main.expertMode ? projectileBaseDamage + 40 : projectileBaseDamage + 10;
                    Aquanados(damage);

                    NPC.ai[2] = enraged ? 100 : (Main.expertMode ? 450 : 600);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[2] += Main.rand.Next(0, 200);
                        NPC.netUpdate = true;
                    }
                }
            }
            #endregion
            #region Ai 3
            if (NPC.life <= NPC.lifeMax * 0.3f)
            {
                //rain
                Main.StartRain();
                Main.maxRaining = 1f;
                Main.windSpeedTarget = 0.9f;


                float movSpeed = 6f;
                if (Main.expertMode) movSpeed += 0.4f;
                if (MyWorld.awakenedMode) movSpeed += 0.6f;
                MoveDirectly(P, movSpeed);

                //minions
                if (!NPC.AnyNPCs(ModContent.NPCType<AqueousMinion1>()))
                {
                    int minionCount = 3;
                    for (int l = 0; l < minionCount; l++)
                    {
                        //cos = y, sin = x
                        int distance = minionCount * 120;
                        NPC orbital = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)(P.Center.X + (Math.Sin(l * distance) * 150)), (int)(P.Center.Y + (Math.Cos(l * distance) * 150)), ModContent.NPCType<AqueousMinion1>(), NPC.whoAmI, 0, 0, 0, -1)];
                        NPC orbital2 = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)(P.Center.X + (Math.Sin(l * distance) * 150)), (int)(P.Center.Y + (Math.Cos(l * distance) * 150)), ModContent.NPCType<AqueousMinion2>(), NPC.whoAmI, 0, 0, 0, -1)];
                        // where the orbitals is positioned
                        orbital.ai[0] = l * 90;
                        orbital2.ai[0] = l * 90;
                    }
                }
                //teleport
                int alphaReduceRate = 15;
                if (NPC.ai[3] <= 0)
                {
                    NPC.alpha += alphaReduceRate;
                }
                else
                {
                    NPC.alpha -= alphaReduceRate;
                }
                if (NPC.alpha >= 255)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int distance = 400;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        Vector2 offset = new Vector2((float)Math.Sin(angle) * distance, (float)Math.Cos(angle) * distance);

                        NPC.Center = P.Center + offset;
                        NPC.ai[3] = 100f + Main.rand.Next(0, 60);
                        NPC.netUpdate = true;
                    }
                }
            }
            #endregion
        }
        public override bool CheckActive()
        {
            return false;
        }
        private void Move(Player P, float speed)
        {
            int maxDist = 1000;
            if (Vector2.Distance(P.Center, NPC.Center) >= maxDist)
            {
                float moveSpeed = 8f;
                Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                toTarget.Normalize();
                NPC.velocity = toTarget * moveSpeed;
            }
            else
            {
                NPC.spriteDirection = NPC.direction;
                float playerX = P.Center.X - NPC.Center.X;
                float playerY = P.Center.Y - 300f - NPC.Center.Y;
                if (NPC.velocity.X < playerX)
                {
                    NPC.velocity.X = NPC.velocity.X + speed * 2;
                    if (NPC.velocity.X < 0f && playerX > 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + speed * 2;
                    }
                }
                else if (NPC.velocity.X > playerX)
                {
                    NPC.velocity.X = NPC.velocity.X - speed * 2;
                    if (NPC.velocity.X > 0f && playerX < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - speed * 2;
                    }
                }
                if (NPC.velocity.Y < playerY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed;
                    if (NPC.velocity.Y < 0f && playerY > 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + speed;
                        return;
                    }
                }
                else if (NPC.velocity.Y > playerY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed;
                    if (NPC.velocity.Y > 0f && playerY < 0f)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - speed;
                        return;
                    }
                }
            }
        }
        private void MoveDirectly(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, NPC.Center) >= 30)
            {
                NPC.velocity = toTarget * moveSpeed;
            }
        }
        private void AquaticBolts(Player P, float speed, int damage)
        {
            SoundEngine.PlaySound(SoundID.Item21, NPC.position);
            float rotation = (float)Math.Atan2(staffCenter.Y - P.Center.Y, staffCenter.X - P.Center.X);
            Projectile.NewProjectile(EAU.NPCs(NPC), staffCenter.X, staffCenter.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ModContent.ProjectileType<AquaticBolt>(), damage, 0f, Main.myPlayer);
        }
        private void WaterKnives(Player P, float speed, int damage, int numberProjectiles)
        {
            float rotation = (float)Math.Atan2(staffCenter.Y - (P.position.Y + (P.height * 0.5f)), staffCenter.X - (P.position.X + (P.width * 0.5f)));
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1)).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(EAU.NPCs(NPC), staffCenter.X, staffCenter.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<WaterKnife>(), damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }
        private void HomingKnives(Player P, float speed, int damage)
        {
            int type = ModContent.ProjectileType<WaterKnifeHoming>();
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            for (int i = 0; i < 4; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * speed), (float)(Math.Cos(offsetAngle) * speed), type, damage, 0f, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)(-Math.Sin(offsetAngle) * speed), (float)(-Math.Cos(offsetAngle) * speed), type, damage, 0f, Main.myPlayer, 0f, 0f);
            }
        }
        private void SpinningAttack(Player P, float speed, int damage)
        {
            Vector2 offset = new Vector2(400, 0);
            spinAI[0] += enraged ? 0.25f : 0.15f;
            Vector2 shootTarget = NPC.Center + offset.RotatedBy(spinAI[0] * (Math.PI * 2 / 8));

            int type = ModContent.ProjectileType<WaterKnife>();
            spinAI[1]--;
            if (spinAI[1] <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item21, NPC.position);
                float rotation = (float)Math.Atan2(NPC.Center.Y - shootTarget.Y, NPC.Center.X - shootTarget.X);
                int num54 = Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, Main.myPlayer);
                spinAI[1] = enraged ? 2 : 4;
            }
        }
        private void Aquanados(int damage)
        {
            int type = ModContent.ProjectileType<AquanadoBolt>();
            float random = Main.rand.NextFloat(0f, 2f);
            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, -6 + random, -2 + random, type, damage, 0f, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 6 + random, -2 + random, type, damage, 0f, Main.myPlayer, 0f, 0f);
        }
    }
}