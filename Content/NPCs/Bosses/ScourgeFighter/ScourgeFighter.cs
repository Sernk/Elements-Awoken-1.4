using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.BossDrops.ScourgeFighter;
using ElementsAwoken.Content.Projectiles.NPCProj.ScourgeFighter;
using ElementsAwoken.EASystem.Loot;
using ElementsAwoken.Utilities;
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

namespace ElementsAwoken.Content.NPCs.Bosses.ScourgeFighter
{
    [AutoloadBossHead]
    public class ScourgeFighter : ModNPC
    {
        public float burstTimer = 10f;
        public float shootCooldown = 10f;

        public float missileTimer = 30f;
        public float napalmTimer = 25f;

        public int minionTimer = 500;

        public float homingMissileTimer = 50f;

        public float homingMove = 0f;
        public float napalmMove = 0f;

        public int projectileBaseDamage = 40;

        int rocketDirection = 1;

        public override void SetDefaults()
        {
            NPC.width = 96;
            NPC.height = 92;
            NPC.aiStyle = -1;
            NPC.lifeMax = 35000;
            NPC.damage = 60;
            NPC.defense = 35;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(0, 15, 0, 0);
            Music = MusicID.Boss3;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.ScourgeFighterBoss"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
            ]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 75;
            NPC.lifeMax = 50000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 75000;
                NPC.damage = 90;
                NPC.defense = 45;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 drawOrigin = new Vector2(TextureAssets.Npc[NPC.type].Value.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = NPC.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Texture2D texture = TextureAssets.Npc[NPC.type].Value;
                EAU.Sb.Draw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Content/NPCs/Bosses/ScourgeFighter/Glow/ScourgeFighter_Glow").Value;
            Rectangle frame = new Rectangle(0, texture.Height * NPC.frame.Y, texture.Width, texture.Height);
            Vector2 origin = frame.Size() * 0.5f;
            SpriteEffects effects = NPC.direction != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), frame, new Color(255, 255, 255, 0), NPC.rotation, origin, NPC.scale, effects, 0.0f);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(1, [.. EAList.ScoLoot]));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterTrophy>(), 10));

            IItemDropRule weaponDrop = new LeadingConditionRule(new EAIDRC.ScourgeLootCondition());
            weaponDrop.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterRocketLauncher>()));
            weaponDrop.OnSuccess(ItemDropRule.Common(ItemID.RocketI, 1, 50, 150));
            npcLoot.Add(weaponDrop);

            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScourgeFighterMask>(), 10));

            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<ScourgeFighterBag>(), 1));
        }
        public override void OnKill()
        {
            MyWorld.downedScourgeFighter = true;
            Main.NewText(ModContent.GetInstance<EALocalization>().ScourgeFighter, Color.Red.R, Color.Red.G, Color.Red.B);
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }      
        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }
        public override void AI()
        {
            bool dayTime = Main.dayTime;
            Player P = Main.player[NPC.target];
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
            #region despawning
            if (!P.active || P.dead)
            {
                NPC.TargetClosest(true);
                if (!P.active || P.dead)
                {
                    NPC.ai[0]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.ai[0] >= 300)
                    {
                        NPC.active = false;
                    }
                }
                else
                    NPC.ai[0] = 0;
            }
            if (Main.dayTime)
            {
                NPC.ai[0]++;
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                if (NPC.ai[0] >= 300)
                {
                    NPC.active = false;
                }
            }
            #endregion           
            NPC.ai[2] += 1f; //ai timer
            NPC.ai[3] += 1f; //rockets are seperate from the ai
            minionTimer--;
            shootCooldown--;
            napalmTimer--;
            homingMissileTimer--;
            burstTimer--;
            if (shootCooldown <= 0)
            {
                shootCooldown = 80f;
            }

            if (NPC.ai[2] > 1520f) // AI TIMER
            {
                NPC.ai[2] = 0f;
            }

            if (NPC.ai[3] > 1100f) // ROCKETS
            {
                NPC.ai[3] = 0f;
            }
            //minions
            if (minionTimer <= 0)
            {
                Vector2 spawnAt = NPC.Center + new Vector2(0f, (float)NPC.height / 2f);
                NPC.NewNPC(EAU.NPCs(NPC), (int)spawnAt.X, (int)spawnAt.Y, ModContent.NPCType<MiniFighter>());
                minionTimer = 750;
            }
            // fly at player and shoot bullets
            if (NPC.ai[2] <= 720f)
            {
                Move(P, 6.5f);
                if (Main.netMode != NetmodeID.MultiplayerClient && burstTimer <= 0f && shootCooldown <= 30)
                {
                    BulletBurst(P, 10f, projectileBaseDamage);
                    burstTimer = 6f;
                }
            }
            // napalm preperation
            if (NPC.ai[2] == 720f)
            {
                NPC.ai[1] = 0f;
            }
            // napalm
            if (NPC.ai[2] >= 720f && NPC.ai[2] <= 1320f)
            {
                NPC.ai[1]++;

                    if (NPC.ai[1] > 400f) // NAPALM
                {
                    NPC.ai[1] = 0f;
                }
                if (NPC.ai[1] == 0f || NPC.ai[1] == 200f)
                {
                    napalmMove = 0;
                }
                Vector2 targetPos = new Vector2(P.Center.X - 200 + napalmMove, P.Center.Y - 300); // position
                Vector2 toTarget = new Vector2(targetPos.X - NPC.Center.X + napalmMove, targetPos.Y - NPC.Center.Y); // velocity
                if (NPC.ai[1] <= 200f)
                {
                    toTarget = new Vector2(P.Center.X - NPC.Center.X - 200 + napalmMove, P.Center.Y - NPC.Center.Y - 300);
                }
                if (NPC.ai[1] >= 200f)
                {
                    toTarget = new Vector2(P.Center.X - NPC.Center.X + 200 - napalmMove, P.Center.Y - NPC.Center.Y - 300);
                }
                float increase = 9f;
                if (Vector2.Distance(NPC.Center, targetPos) <= 100)
                {
                    increase = 20f;
                }
                napalmMove += increase;

                float moveSpeed = 8f;
                toTarget.Normalize();
                NPC.velocity.X = toTarget.X * moveSpeed;
                NPC.velocity.Y = toTarget.Y * moveSpeed * 1.25f;
                if (napalmTimer <= 0f)
                {
                    SoundEngine.PlaySound(SoundID.Item13, NPC.position);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<Napalm>(), projectileBaseDamage, 0f, Main.myPlayer);
                    napalmTimer = 15f;
                }
            }
            // homing missile preparation
            if (NPC.ai[2] == 1320f)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().ScourgeFighter1, Color.Red.R, Color.Red.G, Color.Red.B);
                NPC.ai[1] = 0f;
            }
            // homing missiles
            if (NPC.ai[2] >= 1320f && NPC.ai[2] <= 1520f)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient && homingMissileTimer <= 0)
                {
                    float Speed = 6f;
                    SoundEngine.PlaySound(SoundID.Item72, NPC.position);
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<ScourgeHomingRocket>(), projectileBaseDamage - 20, 0f, Main.myPlayer);
                    homingMissileTimer = 50f;
                }
                NPC.ai[1]++;
                homingMove += 6.5f;
                if (NPC.ai[1] == 0f || NPC.ai[1] == 100f)
                {
                    homingMove = 0;
                }
                Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X - 200, P.Center.Y - NPC.Center.Y + 300 - homingMove);
                if (NPC.ai[1] <= 100f)
                {
                    toTarget = new Vector2(P.Center.X - NPC.Center.X - 200, P.Center.Y - NPC.Center.Y + 300 - homingMove);
                }
                if (NPC.ai[1] >= 100f)
                {
                    toTarget = new Vector2(P.Center.X - NPC.Center.X + 200, P.Center.Y - NPC.Center.Y - 300 + homingMove);
                }
                float moveSpeed = 7f;
                toTarget.Normalize();
                NPC.velocity = toTarget * moveSpeed;
            }
            //rocket preparation & direction determining
            if (NPC.ai[3] == 1030)
            {
                Main.NewText(ModContent.GetInstance<EALocalization>().ScourgeFighter2, Color.Red.R, Color.Red.G, Color.Red.B);
                switch (Main.rand.Next(2))
                {
                    case 0:
                        rocketDirection = 1;
                        break;
                    case 1:
                        rocketDirection = -1;
                        break;
                    default: break;
                }
            }
            // rockets
            if (NPC.ai[3] == 1040 || NPC.ai[3] == 1070 || NPC.ai[3] == 1100)
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active)
                    {
                        int numMissiles = 15;
                        Vector2 baseSpawn = new Vector2(P.Center.X + (500 * rocketDirection), P.Center.Y - 1000);
                        for (int l = 0; l < numMissiles; l++)
                        {
                            Vector2 spawn = baseSpawn;
                            spawn.X = spawn.X + (l * 200 * rocketDirection) - (numMissiles * 15);
                            Projectile.NewProjectile(EAU.NPCs(NPC), spawn.X, spawn.Y, -6 * rocketDirection, 10, ModContent.ProjectileType<ScourgeRocket>(), projectileBaseDamage + 20, 10f, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
            }
            Vector2 dustPos = new Vector2(0f, 32);
            if (NPC.direction == -1)
            {
                dustPos.X = -4f;
            }
            dustPos = dustPos.RotatedBy((double)NPC.rotation, default(Vector2));
            for (int i = 0; i < 2; i++)
            {
                Dust fire = Main.dust[Dust.NewDust(NPC.Center + dustPos - Vector2.One * 5f, 16, 16, EAU.PinkFlame, 0f, 0f, 0, default(Color), 1f)];
                fire.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                fire.noGravity = true;
                fire.velocity = fire.velocity * 0.2f + Vector2.Normalize(dustPos) * 1f;
                fire.velocity = fire.velocity.RotatedBy((double)(-1.57079637f * (float)NPC.direction), default(Vector2));

                Dust smoke = Main.dust[Dust.NewDust(NPC.Center + dustPos - Vector2.One * 5f, 16, 16, EAU.PinkFlame, 0f, 0f, 0, default(Color), 1f)];
                smoke.scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
                smoke.noGravity = true;
                smoke.fadeIn = 1f + Main.rand.NextFloat(0, 0.5f);
                smoke.velocity = smoke.velocity * 0.05f + Vector2.Normalize(dustPos) * 1f;
                smoke.velocity = smoke.velocity.RotatedBy((double)(-1.57079637f * (float)NPC.direction), default(Vector2));
            }
        }
        private void Move(Player P, float moveSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget.Normalize();
            if (Vector2.Distance(P.Center, NPC.Center) >= 30)
            {
                NPC.velocity = toTarget * moveSpeed;
            }
        }

        private void BulletBurst(Player P, float speed, int damage)
        {
            SoundEngine.PlaySound(SoundID.Item11, NPC.position);

            float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X + 35 - P.Center.X);
            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X + 35, NPC.Center.Y, (float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1), ModContent.ProjectileType<ScourgeBullet>(), damage, 0f, Main.myPlayer);

            float rotation2 = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - 35 - P.Center.X);
            Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X - 35, NPC.Center.Y, (float)((Math.Cos(rotation2) * speed) * -1), (float)((Math.Sin(rotation2) * speed) * -1), ModContent.ProjectileType<ScourgeBullet>(), damage, 0f, Main.myPlayer);
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
