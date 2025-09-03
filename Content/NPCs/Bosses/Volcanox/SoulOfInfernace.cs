using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Projectiles.NPCProj.Infernace;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Volcanox
{
    [AutoloadBossHead]
    public class SoulOfInfernace : ModNPC
    {
        public float shootTimer1 = 0f;
        public float shootTimer2 = 0f;
        public float fireTimer = 0f;
        public float fireAITimer = 0f;
        public float tpCooldown1 = 300f;
        public float tpDustCooldown = 10f;

        int projectileBaseDamage = 90;

        bool runTPAlphaChange = false;
        int tpAlphaChangeTimer = 0;
        float telePosX = 0;
        float telePosY = 0;
        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 90;
            NPC.lifeMax = 50000;
            NPC.damage = 90;
            NPC.defense = 55;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit52;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.alpha = 150;
            NPC.value = Item.buyPrice(0, 3, 0, 0);
            NPC.npcSlots = 1f;
            //bossBag = mod.ItemType("InfernaceBag");
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 120;
            NPC.lifeMax = 75000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 100000;
                NPC.damage = 150;
                NPC.defense = 70;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
            ++NPC.frameCounter;
            if (NPC.frameCounter >= 30.0)
                NPC.frameCounter = 0.0;
            NPC.frame.Y = frameHeight * (int)(NPC.frameCounter / 6.0);

            NPC.rotation = NPC.velocity.X * 0.1f;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            // despawn
            if (!NPC.AnyNPCs(ModContent.NPCType<Volcanox>()))
            {
                NPC.active = false;
            }

            Lighting.AddLight(NPC.Center, ((255 - NPC.alpha) * 0.4f) / 255f, ((255 - NPC.alpha) * 0.1f) / 255f, ((255 - NPC.alpha) * 0f) / 255f);
            int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;

            Vector2 infernaceCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
            NPC.ai[1] += 1f;
            fireTimer--;
            tpCooldown1--;
            tpDustCooldown--;
            if (shootTimer1 > 0f)
            {
                shootTimer1 -= 1f;
            }
            if (shootTimer2 > 0f)
            {
                shootTimer2 -= 1f;
            }

            if (NPC.life > NPC.lifeMax * 0.75f)
            {
                if (NPC.ai[1] > 1060f)
                {
                    NPC.ai[1] = 0f;
                }
            }
            else if (NPC.life <= NPC.lifeMax * 0.75f && NPC.life > NPC.lifeMax * 0.5f)
            {
                if (NPC.ai[1] > 1660f)
                {
                    NPC.ai[1] = 0f;
                }
            }
            else if (NPC.life <= NPC.lifeMax * 0.45f)
            {
                if (NPC.ai[1] > 1900f)
                {
                    NPC.ai[1] = 0f;
                }
            }

            if (runTPAlphaChange)
            {
                tpAlphaChangeTimer++;
                if (tpAlphaChangeTimer < 20)
                {
                    NPC.alpha += 8;
                }
                if (tpAlphaChangeTimer == 20)
                {
                    NPC.position.X = telePosX;
                    NPC.position.Y = telePosY;
                }
                if (tpAlphaChangeTimer > 20)
                {
                    NPC.alpha -= 8;
                    if (NPC.alpha <= 0)
                    {
                        runTPAlphaChange = false;
                    }
                }
            }
            else
            {
                NPC.alpha = 0;
            }
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            if (NPC.alpha > 150)
            {
                NPC.alpha = 150;
            }

            MoveDirect(P, 3f);
            if (NPC.ai[1] < 700f)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && shootTimer1 == 0f)
                {
                    Spike(P, 12f, projectileBaseDamage);
                    shootTimer1 = 100f;
                }
                int maxdusts = 20;
                if (tpCooldown1 <= 20f && tpDustCooldown <= 0)
                {
                    for (int i = 0; i < maxdusts; i++)
                    {
                        float dustDistance = 100;
                        float dustSpeed = 10;
                        Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                        Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                        Dust vortex = Dust.NewDustPerfect(NPC.Center + offset, 6, velocity, 0, default(Color), 1.5f);
                        vortex.noGravity = true;

                        tpDustCooldown = 5;
                    }
                }
                if (tpCooldown1 <= 0f)
                {
                    int distance = 200 + Main.rand.Next(0, 200);
                    int choice = Main.rand.Next(4);
                    if (choice == 0)
                    {
                        Teleport(Main.player[NPC.target].position.X + distance, Main.player[NPC.target].position.Y - distance);
                    }
                    if (choice == 1)
                    {
                        Teleport(Main.player[NPC.target].position.X - distance, Main.player[NPC.target].position.Y - distance);
                    }
                    if (choice == 2)
                    {
                        Teleport(Main.player[NPC.target].position.X + distance, Main.player[NPC.target].position.Y + distance);
                    }
                    if (choice == 3)
                    {
                        Teleport(Main.player[NPC.target].position.X - distance, Main.player[NPC.target].position.Y + distance);
                    }
                    tpCooldown1 = 300f;
                }
            }
            if (NPC.ai[1] == 700f)
            {
                fireAITimer = 0f;
            }
            if (NPC.ai[1] >= 700f && NPC.ai[1] <= 1060)
            {
                fireAITimer++;
                NPC.velocity.X = 0;
                NPC.velocity.Y = -6;
                if (fireAITimer == 1f)
                {
                    Teleport(Main.player[NPC.target].position.X + 300, Main.player[NPC.target].position.Y + 200);
                }
                if (fireAITimer == 180f)
                {
                    Teleport(Main.player[NPC.target].position.X - 300, Main.player[NPC.target].position.Y + 200);
                }
                float projSpeedX = fireAITimer < 180f ? -5f : 5f;
                if (fireAITimer >= 20)
                {
                    if (fireTimer <= 0f)
                    {
                        int type = ModContent.ProjectileType<InfernaceFire>();
                        SoundEngine.PlaySound(SoundID.Item13, NPC.position);
                        Projectile.NewProjectile(EAU.NPCs(NPC), infernaceCenter.X, infernaceCenter.Y, projSpeedX, -1, type, projectileBaseDamage, 0f, Main.myPlayer);
                        fireTimer = 10f + Main.rand.Next(0, 15);
                    }
                }
            }
            if (NPC.ai[1] > 1060f && NPC.ai[1] <= 1660)
            {
                if (NPC.ai[1] == 1070f)
                {
                    Teleport(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y - 250);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.velocity *= 0;
                    if (shootTimer2 == 0f)
                    {
                        Waves(P, 10f, projectileBaseDamage - 5, 4);
                        shootTimer2 = 50 + Main.rand.Next(0, 30);
                    }
                }
            }
            if (NPC.ai[1] == 1660)
            {
                NPC.ai[2] = 0;
            }
            if (NPC.ai[1] > 1660)
            {
                float speed = 8f;
                float speedX = 0f;
                float speedY = 0f;

                NPC.ai[2]++;
                if (NPC.ai[2] == 1)
                {
                    Teleport(Main.player[NPC.target].position.X + 500, Main.player[NPC.target].position.Y + 500);
                }
                if (NPC.ai[2] >= 1 && NPC.ai[2] < 140)
                {
                    NPC.velocity.X = -8f;
                    NPC.velocity.Y = -8f;

                    speedX = speed;
                    speedY = -speed;
                }
                if (NPC.ai[2] == 120)
                {
                    Teleport(Main.player[NPC.target].position.X - 500, Main.player[NPC.target].position.Y + 500);
                }
                if (NPC.ai[2] >= 140 && NPC.ai[2] < 240)
                {
                    NPC.velocity.X = 8f;
                    NPC.velocity.Y = -8f;

                    speedX = -speed;
                    speedY = -speed;
                }
            }
        }
        private void MoveDirect(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget.Normalize();
            NPC.velocity = toTarget * moveSpeed;
        }
        private void Move(Player P, float speed, float playerX, float playerY)
        {
            int maxDist = 1500;
            if (Vector2.Distance(P.Center, NPC.Center) >= maxDist)
            {
                float moveSpeed = 14f;
                Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                toTarget.Normalize();
                NPC.velocity = toTarget * moveSpeed;
            }
            else
            {
                if (Main.expertMode)
                {
                    speed += 0.1f;
                }
                if (NPC.velocity.X < playerX)
                {
                    NPC.velocity.X = NPC.velocity.X + speed * 2;
                }
                else if (NPC.velocity.X > playerX)
                {
                    NPC.velocity.X = NPC.velocity.X - speed * 2;
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
        private void Spike(Player P, float speed, int damage)
        {
            int type = ModContent.ProjectileType<InfernaceSpike>();
            SoundEngine.PlaySound(SoundID.Item20, NPC.position);
            float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), type, damage, 0f, Main.myPlayer);
        }
        private void Waves(Player P, float speed, int damage, int numberProj)
        {
            for (int k = 0; k < numberProj; k++)
            {
                Vector2 perturbedSpeed = new Vector2(speed, speed).RotatedByRandom(MathHelper.ToRadians(15));
                Vector2 vector8 = new Vector2(NPC.Center.X - 46, NPC.Center.Y - 69);
                float rotation = (float)Math.Atan2(vector8.Y - P.Center.Y, vector8.X - P.Center.X);
                int num54 = Projectile.NewProjectile(EAU.NPCs(NPC), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * perturbedSpeed.X) * -1), (float)((Math.Sin(rotation) * perturbedSpeed.Y) * -1), ModContent.ProjectileType<InfernaceWave>(), damage, 0f, Main.myPlayer);
            }
        }
        private void Teleport(float posX, float posY)
        {
            runTPAlphaChange = true;
            tpAlphaChangeTimer = 0;
            telePosX = posX;
            telePosY = posY;
        }
    }
}