using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Gores;
using ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Krecheus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Ancients
{
    [AutoloadBossHead]
    public class Krecheus : ModNPC
    {
        public float originX = 0;
        public float originY = 0;
        public int attackType = 0;
        public bool playerUp = false;
        public bool playerLeft = false;
        public float shootTimer = 0;

        public int projectileBaseDamage = 100;

        public float[] dash = new float[3];
        public float spinTimer = 0f;
        public float spinDetectDelay = 0f;
        public Vector2 spinOrigin = new Vector2(0, 0);
        public override void SetDefaults()
        {
            NPC.width = 88;
            NPC.height = 88;
            NPC.aiStyle = -1;
            NPC.lifeMax = 500000;
            NPC.damage = 150;
            NPC.defense = 80;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.Item27;
            NPC.scale *= 1.3f;
            NPC.alpha = 255; // starts transparent
            NPC.value = Item.buyPrice(0, 3, 0, 0);
            NPC.npcSlots = 1f;
            Music = MusicID.LunarBoss;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<ExtinctionCurse>()] = true;
            NPC.buffImmune[ModContent.BuffType<HandsOfDespair>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPC.buffImmune[ModContent.BuffType<AncientDecay>()] = true;
            NPC.buffImmune[ModContent.BuffType<SoulInferno>()] = true;
            //npc.buffImmune[ModContent.BuffType<DragonFire>()] = true;
            NPC.buffImmune[ModContent.BuffType<Discord>()] = true;
            for (int num2 = 0; num2 < 206; num2++)
            {
                NPC.buffImmune[num2] = true;
            }
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "ElementsAwoken/Extra/Bestiary/KrecheusBestiary"
            };
            value.Position.X += 0;
            value.Position.Y -= 0f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.Krecheus"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            ]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)EAU.BalanceHP(250000, balance, bossAdjustment, 600000);
            NPC.damage = (int)EAU.BalanceDamage(150, numPlayers, balance, 200);
            NPC.defense = EAU.BalanceDefense(80, 95);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;

            if (NPC.frameCounter > 6)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }

            if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
            {
                NPC.frame.Y = 0;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (NPC.ai[0] < 180)
            {
                return false;
            }
            return true;
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (NPC.ai[0] < 180)
            {
                return false;
            }
            return base.CanBeHitByProjectile(projectile);
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            if (NPC.ai[0] < 180)
            {
                return false;
            }
            return true;
        }
        public override bool PreKill()
        {
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, 1.5f, 0f, 0.5f);

            // despawn if no players
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
            if (NPC.ai[0] < 180)
            {
                if (NPC.ai[0] == 0)
                {
                    originX = P.Center.X;
                    originY = P.Center.Y;
                    NPC.Center = P.Center;
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] < 60)
                {
                    NPC.alpha = 255;
                }
                else
                {
                    NPC.alpha = 0;
                    Vector2 target = new Vector2(originX - 75, originY - 300);
                    Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                    toTarget.Normalize();
                    if (Vector2.Distance(target, NPC.Center) > 5)
                    {
                        NPC.velocity = toTarget * 6;
                    }
                    else
                    {
                        NPC.velocity *= 0f;
                    }
                }
                NPC.ai[0]++;
            }
            else
            {
                if (NPC.localAI[2] == 0)
                {
                    int orbitalCount = 6;
                    for (int l = 0; l < orbitalCount; l++)
                    {
                        int distance = 360 / orbitalCount;
                        Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<KrecheusBlade>(), NPC.damage / 2, 0f, Main.myPlayer, l * distance, NPC.whoAmI);
                    }
                    NPC.localAI[2]++;
                }
                NPC.ai[1]++;
                if (NPC.ai[1] < 900)
                {
                    dash[0]--;
                    if (dash[0] <= 0)
                    {
                        dash[1]--;
                        if (dash[1] >= 0)
                        {
                            float speed = 10f;
                            float num25 = P.Center.X - NPC.Center.X;
                            float num26 = P.Center.Y - NPC.Center.Y;
                            float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26);
                            num27 = speed / num27;
                            NPC.velocity.X = num25 * num27;
                            NPC.velocity.Y = num26 * num27;
                        }
                        else
                        {
                            dash[0] = 45;
                            dash[1] = 30;
                        }
                        if (Vector2.Distance(P.Center, NPC.Center) < 30)
                        {
                            dash[0] = 45;
                            dash[1] = 30;
                        }
                    }
                    else
                    {
                        NPC.velocity *= 0.94f;
                    }
                }
                else
                {
                    if (NPC.ai[1] == 900)
                    {
                        attackType =  NPC.life < NPC.lifeMax / 3 ? Main.rand.Next(5) : Main.rand.Next(4);

                        playerUp = P.velocity.Y < 0;
                        playerLeft = P.Center.X < NPC.Center.X;

                        originX = P.Center.X;
                        originY = P.Center.Y;

                        spinDetectDelay = 30;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = 0;
                    }
                    if (attackType == 0)
                    {
                        if (NPC.ai[2] == 0)
                        {
                            float targetX = playerLeft ? P.Center.X + 300 : P.Center.X - 300;
                            float targetY = playerUp ? P.Center.Y - 400 : P.Center.Y + 400;
                            Vector2 target = new Vector2(targetX, targetY);

                            Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, NPC.Center) > 20)
                            {
                                NPC.velocity = toTarget * 16;
                            }
                            else
                            {
                                NPC.ai[2] = 1;
                            }
                        }
                        else
                        {
                            NPC.velocity.X = 0;
                            NPC.velocity.Y = playerUp ? 12 : -12;

                            NPC.ai[3]++;
                            shootTimer--;
                            if (shootTimer <= 0)
                            {
                                float speed = 8f;
                                if (playerLeft)
                                {
                                    speed = -8f;
                                }
                                SoundEngine.PlaySound(SoundID.Item21, NPC.position);
                                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, speed, 0f, ModContent.ProjectileType<KrecheusBolt>(), projectileBaseDamage, 0f, Main.myPlayer);
                                shootTimer = 5;
                            }
                            if (NPC.ai[3] >= 60)
                            {
                                NPC.ai[1] = 0;
                                NPC.ai[2] = 0;
                                NPC.ai[3] = 0;
                            }
                        }
                    }
                    else if (attackType == 1)
                    {
                        if (NPC.ai[3] > 0)
                        {
                            spinTimer += 2f; // speed
                        }
                        Vector2 center = new Vector2(originX, originY);

                        int distance = 400;
                        double rad = spinTimer * (Math.PI / 180); // angle to radians
                        float spinX = originX - (int)(Math.Cos(rad) * distance) - NPC.width / 2;
                        float spinY = originY - (int)(Math.Sin(rad) * distance) - NPC.height / 2;
                        Vector2 target = new Vector2(spinX, spinY);

                        if (NPC.ai[2] == 0)
                        {
                            spinOrigin = target;
                            NPC.ai[2]++;
                        }

                        if (NPC.ai[3] == 0)
                        {
                            Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, NPC.Center) > 20)
                            {
                                NPC.velocity = toTarget * 16;
                            }
                            else
                            {
                                NPC.ai[3] = 1;
                            }
                        }
                        else
                        {
                            NPC.velocity *= 0f;

                            NPC.position.X = spinX;
                            NPC.position.Y = spinY;

                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<KrecheusCircle>(), projectileBaseDamage, 0f, Main.myPlayer);
                            spinDetectDelay--;
                            if (spinDetectDelay <= 0)
                            {
                                if (Vector2.Distance(spinOrigin, NPC.Center) < 75)
                                {
                                    NPC.ai[1] = 0;
                                    NPC.ai[2] = 0;
                                    NPC.ai[3] = 0;
                                }
                            }
                        }
                    }
                    else if (attackType == 2)
                    {
                        if (NPC.ai[2] == 0)
                        {
                            float targetX = playerLeft ? P.Center.X + 300 : P.Center.X - 300;
                            float targetY = P.Center.Y;
                            Vector2 target = new Vector2(targetX, targetY);

                            Vector2 toTarget = new Vector2(target.X - NPC.Center.X, target.Y - NPC.Center.Y);
                            toTarget.Normalize();
                            if (Vector2.Distance(target, NPC.Center) > 20)
                            {
                                NPC.velocity = toTarget * 16;
                            }
                            else
                            {
                                NPC.ai[2] = 1;
                            }
                        }
                        else
                        {
                            if (NPC.ai[3] == 0)
                            {
                                int numDusts = 40;
                                for (int i = 0; i < numDusts; i++)
                                {
                                    Vector2 position = (Vector2.Normalize(new Vector2(2,2)) * new Vector2((float)NPC.width / 2f, (float)NPC.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + NPC.Center;
                                    Vector2 velocity = position - NPC.Center;
                                    int dust = Dust.NewDust(position + velocity, 0, 0, ModContent.DustType<AncientRed>(), velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                                    Main.dust[dust].noGravity = true;
                                    Main.dust[dust].noLight = true;
                                    Main.dust[dust].velocity = Vector2.Normalize(velocity) * 4f;
                                }
                            }
                            NPC.ai[3]++;
                            if (NPC.ai[3] > 20)
                            {
                                NPC.velocity.X = playerLeft ? -14 : 14;
                                NPC.velocity.Y = 0;

                                NPC.direction = Math.Sign(NPC.velocity.X);
                                NPC.spriteDirection = Math.Sign(NPC.velocity.X);

                                Vector2 position = NPC.Center + Vector2.Normalize(NPC.velocity) * 10f;
                                Dust dust = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AncientRed>(), 0f, 0f, 0, default(Color), 1.5f)];
                                dust.position = position;
                                dust.velocity = NPC.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * 0.33f + NPC.velocity / 4f;
                                dust.velocity.X -= NPC.velocity.X / 10f;
                                dust.position += NPC.velocity.RotatedBy(1.5707963705062866, default(Vector2));
                                dust.fadeIn = 0.5f;
                                dust.noGravity = true;
                                Dust dust1 = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<AncientRed>(), 0f, 0f, 0, default(Color), 1.5f)];
                                dust1.position = position;
                                dust1.velocity = NPC.velocity.RotatedBy(-1.5707963705062866, default(Vector2)) * 0.33f + NPC.velocity / 4f;
                                dust1.velocity.X -= NPC.velocity.X / 10f;
                                dust1.position += NPC.velocity.RotatedBy(-1.5707963705062866, default(Vector2));
                                dust1.fadeIn = 0.5f;
                                dust1.noGravity = true;
                            }
                            else
                            {
                                NPC.velocity *= 0f;
                            }
                            if (NPC.ai[3] >= 120)
                            {
                                NPC.ai[1] = 0;
                                NPC.ai[2] = 0;
                                NPC.ai[3] = 0;
                            }
                        }
                    }
                    else if (attackType == 3)
                    {
                        NPC.ai[2]++;
                        if (NPC.ai[2] > 200)
                        {
                            NPC.ai[2] = 0;
                            NPC.ai[1] = 0;
                        }
                        NPC.velocity *= 0;
                        if (Main.rand.Next(30) == 0)
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                float posX = i == 1 ? P.Center.X + 1000 : P.Center.X - 1000;
                                float posY = P.Center.Y + Main.rand.Next(-700, 700);
                                Projectile.NewProjectile(EAU.NPCs(NPC), posX, posY, i % 2 == 0 ? 16f : -16f, 0f, ModContent.ProjectileType<KrecheusSide>(), projectileBaseDamage, 0f);
                            }
                        }

                        int numDusts = 2;
                        for (int i = 0; i < numDusts; i++)
                        {
                            float dustDistance = Main.rand.Next(40, 80);
                            float dustSpeed = 4;
                            Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                            Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                            Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y - 30) + offset, ModContent.DustType<AncientRed>(), velocity, 0, default(Color), 1.5f);
                            vortex.noGravity = true;
                            vortex.fadeIn = Main.rand.NextFloat(0.3f, 0.6f);
                        }
                    }
                    else if (attackType == 4)
                    {
                        NPC.ai[2]++;
                        if (NPC.ai[2] > 120)
                        {
                            NPC.ai[2] = 0;
                            NPC.ai[1] = 0;

                            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<KrecheusPortal>(), projectileBaseDamage, 0f);
                        }
                        else
                        {
                            int numDusts = 2;
                            for (int i = 0; i < numDusts; i++)
                            {
                                float dustDistance = Main.rand.Next(40, 80);
                                float dustSpeed = 4;
                                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                                Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y - 30) + offset, ModContent.DustType<AncientRed>(), velocity, 0, default(Color), 1.5f);
                                vortex.noGravity = true;
                                vortex.fadeIn = Main.rand.NextFloat(0.3f, 0.6f);
                            }
                        }
                    }
                }
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<KrecheusShard>(), 0, 0f, Main.myPlayer, i);
                }
                for (int k = 0; k < 80; k++)
                {
                    int dust = Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, ModContent.DustType<AncientRed>(), NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].scale = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
        }
        private void Move(Player P, float speed, Vector2 target)
        {
            Vector2 desiredVelocity = target - NPC.Center;
            if (Main.expertMode) speed *= 1.1f;
            if (MyWorld.awakenedMode) speed *= 1.1f;
            if (Vector2.Distance(P.Center, NPC.Center) >= 2500) speed = 2;

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
                    return;
                }
            }
            else if (NPC.velocity.Y > desiredVelocity.Y)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed;
                if (NPC.velocity.Y > 0f && desiredVelocity.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed;
                    return;
                }
            }
            float slowSpeed = Main.expertMode ? 0.93f : 0.95f;
            if (MyWorld.awakenedMode) slowSpeed = 0.92f;
            int xSign = Math.Sign(desiredVelocity.X);
            if ((NPC.velocity.X < xSign && xSign == 1) || (NPC.velocity.X > xSign && xSign == -1)) NPC.velocity.X *= slowSpeed;

            int ySign = Math.Sign(desiredVelocity.Y);
            if (MathHelper.Distance(target.Y, NPC.Center.Y) > 1000)
            {
                if ((NPC.velocity.X < ySign && ySign == 1) || (NPC.velocity.X > ySign && ySign == -1)) NPC.velocity.Y *= slowSpeed;
            }
        }
    }
}
