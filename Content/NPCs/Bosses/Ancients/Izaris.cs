using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Dusts.Ancients;
using ElementsAwoken.Content.NPCs.Bosses.Ancients.Minions;
using ElementsAwoken.Content.Projectiles.NPCProj.Ancients.Gores;
using ElementsAwoken.EAUtilities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Ancients
{
    [AutoloadBossHead]
    public class Izaris : ModNPC
    {
        public float originX = 0;
        public float originY = 0;

        public int moveAi = 0;
        public override void SetDefaults()
        {
            NPC.width = 88;
            NPC.height = 88;
            NPC.aiStyle = -1;
            NPC.lifeMax = 300000;
            NPC.damage = 60;
            NPC.defense = 60;
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
                CustomTexturePath = "ElementsAwoken/Extra/Bestiary/IzarisBestiary"
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
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.IzarisBoss"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            ]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)EAU.BalanceHP(200000, balance, bossAdjustment, 500000);
            NPC.defense = EAU.BalanceDefense(60, 80);
            NPC.damage = (int)EAU.BalanceDamage(60, numPlayers, balance, 80);
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

            // MASTER NPC
            Player P = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, 1.2f, 0f, 1.5f);

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

            if (NPC.localAI[1] == 0)
            {
                NPC.ai[1] = 600;
                NPC.localAI[1]++;
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
                    MoonlordDeathDrama.RequestLight(1f, NPC.Center);
                    NPC.alpha = 255;
                }
                else
                {
                    NPC.alpha = 0;
                    Vector2 target = new Vector2(originX + 75, originY - 300);
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
                if (NPC.ai[0] == 299)
                {
                    if (!MyWorld.downedAncients)
                    {
                        Main.NewText(ModContent.GetInstance<EALocalization>().Izaris, new Color(3, 188, 127));
                    }
                    else
                    {
                        Main.NewText(ModContent.GetInstance<EALocalization>().Izaris1, new Color(3, 188, 127));
                    }
                }
            }
            else
            {
                float speed = 0.2f;
                float playerX = P.Center.X;
                float playerY = P.Center.Y - 400f;
                if (moveAi == 0)
                {
                    playerX = P.Center.X - 600f;
                    if (Math.Abs(P.Center.X - 600f - NPC.Center.X) <= 20)
                    {
                        moveAi = 1;
                    }
                }
                if (moveAi == 1)
                {
                    playerX = P.Center.X + 600f;
                    if (Math.Abs(P.Center.X + 600f - NPC.Center.X) <= 20)
                    {
                        moveAi = 0;
                    }
                }
                Move(P, speed, new Vector2(playerX,playerY));

                NPC.ai[1]--;
                if (NPC.ai[1] <= 0f)
                {
                    if (NPC.CountNPCS(ModContent.NPCType<CrystalSerpentHead>()) < 4)
                    {
                        NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<CrystalSerpentHead>(), NPC.whoAmI);
                    }
                    NPC.ai[1] = Main.rand.Next(600, 1200);
                    NPC.netUpdate = true;
                }
                NPC.ai[2]++;
                if (NPC.ai[2] > 1800)
                {
                    if (NPC.CountNPCS(ModContent.NPCType<EnergySeeker>()) < 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            NPC seeker = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<EnergySeeker>(), NPC.whoAmI)];
                            seeker.ai[1] = -(i * 40);
                        }
                    }
                    else if (NPC.CountNPCS(ModContent.NPCType<EnergySeeker>()) < 8)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            NPC seeker = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<EnergySeeker>(), NPC.whoAmI)];
                            seeker.ai[1] = -(i * 40);
                        }
                    }
                    NPC.ai[2] = 0;
                }
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<IzarisShard>(), 0, 0f, Main.myPlayer, i);
                }
                for (int k = 0; k < 80; k++)
                {
                    int dust = Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, ModContent.DustType<AncientPink>(), NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f, 100, default(Color), 2f);
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