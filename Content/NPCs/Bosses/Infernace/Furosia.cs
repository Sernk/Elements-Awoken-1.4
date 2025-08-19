using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Projectiles.NPCProj.Infernace;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Infernace
{
    [AutoloadBossHead]
    public class Furosia : ModNPC
    {
        private int projectileBaseDamage = 35;
        private const int tpDuration = 40;
        private float telePosX = 0;
        private float telePosY = 0;
        public float dashAI = 0;

        private float aiTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float despawnTimer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float tpAlphaChangeTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float tpDir
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(telePosX);
            writer.Write(telePosY);
            writer.Write(dashAI);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            telePosX = reader.ReadSingle();
            telePosY = reader.ReadSingle();
            dashAI = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            NPC.width = 188;
            NPC.height = 190;
            NPC.aiStyle = -1;
            NPC.lifeMax = 2000;
            NPC.damage = 15;
            NPC.defense = 6;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.scale = 1f;
            NPC.HitSound = SoundID.NPCHit52;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 1f;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[ModContent.BuffType<IceBound>()] = true;
            NPC.buffImmune[ModContent.BuffType<EndlessTears>()] = true;
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            NPC.rarity = 4;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 0.8f, // Мини иконка в бестиарии
                PortraitScale = 0.6f, // При нажатии на иконку в бестиарии
            };
            value.Position.X += 0f;
            value.Position.Y -= 35f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.Furosia"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
            });
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 30;
            NPC.lifeMax = 3000;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 5000;
                NPC.damage = 45;
                NPC.defense = 10;
            }
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
            if (NPC.frame.Y > frameHeight * 4)
            {
                NPC.frame.Y = 0;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            if (!P.active || P.dead) NPC.TargetClosest(true);

            NPC.direction = Math.Sign(P.Center.X - NPC.Center.X);
            NPC.spriteDirection = NPC.direction;
            Lighting.AddLight(NPC.Center, ((255 - NPC.alpha) * 0.4f) / 255f, ((255 - NPC.alpha) * 0.1f) / 255f, ((255 - NPC.alpha) * 0f) / 255f);
            NPC.rotation = NPC.velocity.X * 0.1f;

            int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 6);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1f;
            Main.dust[dust].velocity *= 0.1f;
            if (tpAlphaChangeTimer > 0)
            {
                tpAlphaChangeTimer--;
                if (tpAlphaChangeTimer > (int)(tpDuration / 2))
                {
                    NPC.alpha += 26;
                }
                if (tpAlphaChangeTimer == (int)(tpDuration / 2) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.position.X = telePosX - NPC.width / 2;
                    NPC.position.Y = telePosY - NPC.height / 2;
                    NPC.netUpdate = true;
                }
                if (tpAlphaChangeTimer < (int)(tpDuration / 2))
                {
                    NPC.alpha -= 26;
                    if (NPC.alpha <= 0)
                    {
                        tpAlphaChangeTimer = 0;
                    }
                }
            }
            NPC daddy = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<Infernace>())];
            if (daddy.ai[1] < 0)
            {
                NPC.rotation = 0;
                despawnTimer++;
                NPC.velocity.Y = 0;
                if (NPC.velocity.X > -20 && NPC.velocity.X < 20) NPC.velocity.X += Math.Sign(NPC.position.X - P.position.X) * 0.25f;
                if (despawnTimer > 120)
                {
                    NPC.alpha++;
                    if (NPC.alpha >= 255) NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                aiTimer++;
                if (aiTimer < 300)
                {
                    dashAI++;
                    if (dashAI == 30)
                    {
                        SoundEngine.PlaySound(SoundID.Item69, NPC.position);
                        float dashSpeed = Main.expertMode ? 7 : 5;
                        if (MyWorld.awakenedMode) dashSpeed = 9;
                        Dash(P, dashSpeed);
                    }
                    if (dashAI > 70) NPC.velocity *= 0.96f;
                    if (dashAI >= 120) dashAI = 0;
                }
                else
                {
                    NPC.velocity = Vector2.Zero;
                    if (aiTimer == 300)
                    {
                        tpDir = Main.rand.Next(2) == 0 ? -1 : 1;
                        Teleport(P.Center.X + 600 * tpDir, P.Center.Y);
                    }
                    if ((aiTimer == 380 || (aiTimer == 390 && Main.expertMode) || (aiTimer == 400 && MyWorld.awakenedMode)) && Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                        float projSpeed = Main.expertMode ? 14 : 10;
                        if (MyWorld.awakenedMode) projSpeed = 18;

                        int damage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : (int)(projectileBaseDamage);
                        if (MyWorld.awakenedMode) damage = (int)(projectileBaseDamage * 2f);

                        Projectile proj = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, -tpDir * projSpeed, 0, ModContent.ProjectileType<FurosiaSpike>(), damage, 0f, Main.myPlayer)];
                        proj.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                    }
                    if (aiTimer == 420)
                    {
                        Teleport(P.Center.X + Main.rand.Next(400, 400), P.Center.Y - 200);
                        aiTimer = 0;
                    }
                }
            }
        }
        private void Dash(Player P,float dashSpeed)
        {
            Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
            toTarget.Normalize();
            NPC.velocity = toTarget * dashSpeed;
        }
        private void Teleport(float posX, float posY)
        {
            tpAlphaChangeTimer = tpDuration;
            telePosX = posX;
            telePosY = posY;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}