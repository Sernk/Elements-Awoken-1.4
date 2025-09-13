using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.ItemSets.Puff;
using ElementsAwoken.Content.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.ItemSets.Puff
{
    public class SpikedPuff : ModNPC
    {
        public float spikeTimer = 60f;

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 50;
            NPC.damage = 24;
            NPC.defense = 6;
            NPC.lifeMax = 32;
            NPC.knockBackResist = 0.5f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 1, 0);
            NPC.aiStyle = 1;
            AIType = 1;
            AnimationType = NPCID.BlueSlime;
            //banner = npc.type;
            //bannerItem = mod.ItemType("SpikedPuffBanner");
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode) target.AddBuff(ModContent.BuffType<Cuddled>(), 2000);
            else target.AddBuff(ModContent.BuffType<Cuddled>(), 500);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot) => npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Puffball>(), 1, 2, 4));
        public override void AI()
        {
            spikeTimer--;
            if (!NPC.wet && !Main.player[NPC.target].npcTypeNoAggro[NPC.type])
            {
                Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                float num8 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector.X;
                float num9 = Main.player[NPC.target].position.Y - vector.Y;
                float num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
                if (Main.expertMode && num10 < 120f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
                {
                    NPC.ai[0] = -40f;
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer <= 0f)
                    {
                        int num = MyWorld.awakenedMode ? 5 : 3;
                        for (int i = 0; i < num; i++)
                        {
                            Vector2 vector2 = new Vector2((float)(i - 2), -4f);
                            vector2.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector2.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                            vector2.Normalize();
                            vector2 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                            int damage = 7;
                            Projectile.NewProjectile(EAU.NPCs(NPC), vector.X, vector.Y, vector2.X, vector2.Y, ModContent.ProjectileType<PuffSpike>(), damage, 0f, Main.myPlayer, 0f, 0f);
                            spikeTimer = 70f;
                        }
                    }
                }
                else if (num10 < 200f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height) && NPC.velocity.Y == 0f)
                {
                    NPC.ai[0] = -40f;
                    if (NPC.velocity.Y == 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X * 0.9f;
                    }
                    if (Main.netMode != NetmodeID.MultiplayerClient && spikeTimer <= 0f)
                    {
                        num9 = Main.player[NPC.target].position.Y - vector.Y - (float)Main.rand.Next(0, 200);
                        num10 = (float)Math.Sqrt((double)(num8 * num8 + num9 * num9));
                        num10 = 4.5f / num10;
                        num8 *= num10;
                        num9 *= num10;
                        spikeTimer = 90f;
                        int damage = 5;
                        Projectile.NewProjectile(EAU.NPCs(NPC), vector.X, vector.Y, num8, num9, ModContent.ProjectileType<PuffSpike>(), damage, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
    }
}
