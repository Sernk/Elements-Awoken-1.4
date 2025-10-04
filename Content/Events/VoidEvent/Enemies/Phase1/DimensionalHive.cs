using ElementsAwoken.Content.Items.Essence;
using ElementsAwoken.Content.Items.Placeable.VoidStone;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase1
{
    public class DimensionalHive : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 120;
            NPC.aiStyle = -1;
            NPCID.Sets.NeedsExpertScaling[NPC.type] = true;
            NPC.defense = 35;
            NPC.lifeMax = 10000;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath5;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
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
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.DimensionalHive")]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 15000;
            NPC.defense = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 20000;
                NPC.defense = 65;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidEssence>(), 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VoidStone>(), 2, 3, 5));
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (ModContent.GetInstance<Config>().lowDust)
            {
                float maxDist = 600;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC other = Main.npc[i];
                    if (VoidEvent.phase1NPCs.Contains(other.type) && Vector2.Distance(NPC.Center, other.Center) < maxDist)
                    {
                        Texture2D texture = ModContent.Request<Texture2D>("ElementsAwoken/Content/Events/VoidEvent/Enemies/Phase1/HiveBeam").Value;

                        Vector2 position = NPC.Center;
                        Vector2 mountedCenter = other.Center;
                        Rectangle? sourceRectangle = new Rectangle?();
                        Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
                        float num1 = (float)texture.Height;
                        Vector2 vector2_4 = mountedCenter - position;
                        float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
                        bool flag = true;
                        if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                            flag = false;
                        if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                            flag = false;
                        while (flag)
                        {
                            if ((double)vector2_4.Length() < (double)num1 + 1.0)
                            {
                                flag = false;
                            }
                            else
                            {
                                Vector2 vector2_1 = vector2_4;
                                vector2_1.Normalize();
                                position += vector2_1 * num1;
                                vector2_4 = mountedCenter - position;

                                Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                            }
                        }
                    }
                }
            }
            return true;
        }
        public override void AI()
        {
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];

            if (NPC.localAI[0] == 0)
            {
                NPC.ai[0] = NPC.position.Y;
                NPC.localAI[0]++;
            }
            NPC.ai[1]++;
            NPC.position.Y = NPC.ai[0] + (float)Math.Sin(NPC.ai[1] / 30) * 30;


            float maxDist = 600;
            int maxHealing = 5;
            int numHealing = 0;
            bool runCode = true;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC other = Main.npc[i];
                if (other.active)
                {
                    if (VoidEvent.phase1NPCs.Contains(other.type) && Vector2.Distance(NPC.Center, other.Center) < maxDist && other.type != NPC.type && other.type != ModContent.NPCType<VoidFly>() && numHealing < maxHealing)
                    {
                        numHealing++;
                        int healamount = (int)(other.lifeMax * 0.05f);
                        if (other.life <= other.lifeMax - healamount && NPC.ai[1] % 20 == 0) other.life += healamount;
                        if (!ModContent.GetInstance<Config>().lowDust)
                        {
                            for (int k = 0; k < 10; k++)
                            {
                                Dust d = Main.dust[Dust.NewDust(NPC.Center + (other.Center - NPC.Center) * Main.rand.NextFloat() - new Vector2(4, 4), 0, 0, 127)];
                                d.noGravity = true;
                                d.velocity *= 0.04f;
                                d.scale *= 0.8f;
                            }
                        }
                    }
                    if (other.whoAmI != NPC.whoAmI && other.type == NPC.type && Vector2.Distance(NPC.Center, other.Center) < maxDist * 1.75 && NPC.ai[1] < other.ai[1])
                    {
                        NPC.active = false;
                        runCode = false; // so the dust isnt created when it vanishes
                    }
                }
            }
            if (runCode)
            {
                for (int i = 0; i < 120; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                    Dust dust = Main.dust[Dust.NewDust(NPC.Center + offset - new Vector2(4, 4), 0, 0, 127)];
                    dust.noGravity = true;
                }
            }
        }
    }
}