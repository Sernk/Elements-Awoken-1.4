using ElementsAwoken.Content.Items.Banners.VoidEvent;
using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.VoidStone;
using ElementsAwoken.Content.Items.VoidEventItems;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1
{
    public class ReaverSlime : ModNPC
	{
        private float jumpTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float jumpNum
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        public override void SetDefaults()
		{
            NPC.width = 32;
            NPC.height = 22;
            NPC.aiStyle = -1;
            NPC.damage = 76;
			NPC.defense = 20;
			NPC.lifeMax = 1000;
            NPC.knockBackResist = 0.3f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
			NPC.lavaImmune = false;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<ReaverSlimeBanner>();
            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
            SpawnModBiomes = [ModContent.GetInstance<DOTVBiome>().Type];
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = -1f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.ReaverSlime")]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
            NPC.lifeMax = 2000;
            NPC.defense = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 3000;
                NPC.defense = 65;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage = NPCsGLOBAL.ReducePierceDamage(modifiers.SourceDamage, projectile);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(EAU.HandsOfDespair, 180, false);
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (jumpTimer <= 40) NPC.frameCounter++;
            if (NPC.frameCounter > 8)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 1)  // so it doesnt go over
            {
                NPC.frame.Y = 0;
            }
            if (aiTimer > 310)
            {
                NPC.frame.Y = frameHeight * 2;
            }
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            if (aiTimer == 300)
            {
                NPC.velocity.Y = -20f;
                NPC.velocity.X = NPC.velocity.X + (float)(2 * NPC.direction);
            }
            if (aiTimer >= 300)
            {
                NPC.noGravity = true;
                NPC.velocity *= 0.95f;

                if (aiTimer % 60 == 0 && aiTimer != 300)
                {
                    SoundEngine.PlaySound(SoundID.Item17, NPC.position);

                    float Speed = 6f;
                    float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1) - 2, ModContent.ProjectileType<ReaverGlob>(), 30, 0f, 0);
                }
            }
            if (aiTimer > 600)
            {
                NPC.noGravity = false;
                aiTimer = 0;
            }
            if (aiTimer < 300)
            {
                if (Vector2.Distance(P.Center, NPC.Center) < 600 && Collision.CanHit(NPC.position, NPC.width, NPC.height, P.position, P.width, P.height)) aiTimer++;
                SlimeAI();
            }
            else
            {
                aiTimer++;
            }
        }
        private void SlimeAI()
        {
            NPC.TargetClosest(true);
            if (NPC.wet)
            {
                if (NPC.collideY)
                {
                    NPC.velocity.Y = -2f;
                }
                if (NPC.velocity.Y < 0f && NPC.ai[3] == NPC.position.X)
                {
                    NPC.direction *= -1;
                }
                if (NPC.velocity.Y > 0f)
                {
                    NPC.ai[3] = NPC.position.X;
                }

                if (NPC.velocity.Y > 2f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
                NPC.velocity.Y = NPC.velocity.Y - 0.5f;
                if (NPC.velocity.Y < -4f)
                {
                    NPC.velocity.Y = -4f;
                }
            }

            if (NPC.velocity.Y == 0f)
            {
                if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.position.X = NPC.position.X - (NPC.velocity.X + (float)NPC.direction);
                }
                if (NPC.ai[3] == NPC.position.X)
                {
                    NPC.direction *= -1;
                }
                NPC.ai[3] = 0f;
                NPC.velocity.X = NPC.velocity.X * 0.8f;
                if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
                {
                    NPC.velocity.X = 0f;
                }
                jumpTimer--; // jump speed

                if (jumpTimer <= 0)
                {
                    jumpNum++;
                    NPC.netUpdate = true;
                    if (jumpNum == 4)
                    {
                        NPC.velocity.Y = -8f;
                        NPC.velocity.X = NPC.velocity.X + (float)(3 * NPC.direction);
                        jumpTimer = 90f;
                        NPC.ai[3] = NPC.position.X;
                        jumpNum = 0;
                    }
                    else
                    {
                        NPC.velocity.Y = -6f;
                        NPC.velocity.X = NPC.velocity.X + (float)(2 * NPC.direction);
                        jumpTimer = 40f;
                    }
                }
                else if (jumpTimer >= -30f)
                {
                    return;
                }
            }
            else if (NPC.target < 255 && ((NPC.direction == 1 && NPC.velocity.X < 3f) || (NPC.direction == -1 && NPC.velocity.X > -3f)))
            {
                if (NPC.collideX && Math.Abs(NPC.velocity.X) == 0.2f)
                {
                    NPC.position.X = NPC.position.X - 1.4f * (float)NPC.direction;
                }
                if (NPC.collideY && NPC.oldVelocity.Y != 0f && Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    NPC.position.X = NPC.position.X - (NPC.velocity.X + (float)NPC.direction);
                }
                if ((NPC.direction == -1 && (double)NPC.velocity.X < 0.01) || (NPC.direction == 1 && (double)NPC.velocity.X > -0.01))
                {
                    NPC.velocity.X = NPC.velocity.X + 0.2f * (float)NPC.direction;
                    return;
                }
                NPC.velocity.X = NPC.velocity.X * 0.93f;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidJelly>(), 6, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidStone>(), 2, 3, 5));
        }
    }
}