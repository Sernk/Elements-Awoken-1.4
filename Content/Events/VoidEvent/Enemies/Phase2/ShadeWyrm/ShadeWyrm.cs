using ElementsAwoken.Content.Items.VoidEventItems;
using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.EASystem.EAPlayer;
using ElementsAwoken.EASystem.Loot;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Events.VoidEvent.Enemies.Phase2.ShadeWyrm
{
    [AutoloadBossHead]

    class ShadeWyrmHead : ShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/Content/Events/VoidEvent/Enemies/Phase2/ShadeWyrm/ShadeWyrmHead"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 64;
            NPC.height = 80;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            SpawnModBiomes = new int[1] { GetInstance<DOTVBiome>().Type };
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new BossBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Bosses.ShadeWyrm"),
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            });
        }
        public override void SetStaticDefaults()
        {
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Scale = 0.8f,
                PortraitScale = 0.8f,
                CustomTexturePath = "ElementsAwoken/Extra/Bestiary/ShadeWyrmBestiary"
            };
            value.Position.X += 15f;
            value.Position.Y += 15f;
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }
        public override void Init()
        {
            base.Init();
            head = true;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            var AwakenedMode = new LeadingConditionRule(new EAIDRC.DropAwakened());
            var ExpertMod = new LeadingConditionRule(new EAIDRC.DropExpert());
            var NormalMod = new LeadingConditionRule(new EAIDRC.DropNormal());

            AwakenedMode.OnSuccess(ItemDropRule.Common(ItemType<ShadeScale>(), minimumDropped: 16, maximumDropped: 26));
            npcLoot.Add(AwakenedMode);
            ExpertMod.OnSuccess(ItemDropRule.Common(ItemType<ShadeScale>(), minimumDropped: 12, maximumDropped: 19));
            npcLoot.Add(ExpertMod);
            NormalMod.OnSuccess(ItemDropRule.Common(ItemType<ShadeScale>(), minimumDropped: 8, maximumDropped: 12));
            npcLoot.Add(NormalMod);
        }
        public override void OnKill()
        {
            MyWorld.downedShadeWyrm = true;
            if (Main.netMode == NetmodeID.Server) NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
    class ShadeWyrmBody : ShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/Content/Events/VoidEvent/Enemies/Phase2/ShadeWyrm/ShadeWyrmBody"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();

            NPC.width = 40;
            NPC.height = 30;

            NPC.value = Item.buyPrice(0, 5, 0, 0);

        }
        public int bodyNum = 0;
        private int projectileBaseDamage = 150;
        public override void CustomBehavior()
        {
            Player P = Main.player[NPC.target];

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num = Main.expertMode ? 3 : 4;
                if (MyWorld.awakenedMode) num = 2;
                if (bodyNum % num == 0)
                {
                    if (aiTimer == 620 + 10 * bodyNum)
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            int projDamage = Main.expertMode ? (int)(projectileBaseDamage * 1.5f) : projectileBaseDamage;
                            if (MyWorld.awakenedMode) projDamage = (int)(projectileBaseDamage * 2f);

                            float speedMult = Main.expertMode ? 9f : 7f;
                            if (MyWorld.awakenedMode) speedMult = 12f;

                            Vector2 projSpeed = new Vector2(0, 1).RotatedBy(NPC.rotation + MathHelper.ToRadians(k == 0 ? 90 : 270));
                            projSpeed.Normalize();
                            projSpeed *= speedMult;

                            Projectile bolt = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center, projSpeed, ProjectileType<ShadeWyrmBolt>(), projDamage, 0f, 0)];
                            bolt.GetGlobalProjectile<ProjectileGlobal>().dontScaleDamage = true;
                        }
                        SoundEngine.PlaySound(SoundID.Item12, NPC.position);
                    }
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }

    class ShadeWyrmTail : ShadeWyrm
    {
        public override string Texture { get { return "ElementsAwoken/Content/Events/VoidEvent/Enemies/Phase2/ShadeWyrm/ShadeWyrmTail"; } }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 40;
            NPC.height = 30;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
        }

        public override void Init()
        {
            base.Init();
            tail = true;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }

    public abstract class ShadeWyrm : ShadeWyrmAI
    {
        public override void SetDefaults()
        {
            NPC.boss = true;
            NPC.lifeMax = 22000;
            NPC.defense = 40;
            NPC.damage = 100;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.behindTiles = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }

            NPC.GetGlobalNPC<AwakenedModeNPC>().dontExtraScale = true;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 50000;
            NPC.damage = 150;
            NPC.defense = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 100000;
                NPC.damage = 200;
                NPC.defense = 60;
            }
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shade Wyrm");
        }

        public override void Init()
        {
            tailType = NPCType<ShadeWyrmTail>();
            bodyType = NPCType<ShadeWyrmBody>();
            headType = NPCType<ShadeWyrmHead>();

            wormLength = 22;

            speed = 10f;
            turnSpeed = 0.1f;
        }
    }

    public abstract class ShadeWyrmAI : ModNPC
    {
        /* ai[0] = follower
		 * ai[1] = following
		 * ai[2] = distanceFromTail
		 * ai[3] = head
		 */
        public bool head;
        public bool tail;
        public int wormLength;
        public int headType;
        public int bodyType;
        public int tailType;
        public bool flies = false;
        public bool directional = false;
        public float speed;
        public float turnSpeed;

        public float aiTimer = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(aiTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aiTimer = reader.ReadSingle();
        }

        public override void AI()
        {
            NPC.ReflectProjectiles(NPC.Hitbox);
            //NPC.reflectingProjectiles = true;

            if (NPC.localAI[1] == 0f)
            {
                NPC.localAI[1] = 1f;
                Init();
            }
            if (aiTimer < 600 || aiTimer > 600) aiTimer++;
            if (aiTimer > 620 + wormLength * 10) aiTimer = 0;

            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (!head && NPC.timeLeft < 300)
            {
                NPC.timeLeft = 300;
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
            {
                NPC.timeLeft = 300;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!tail && NPC.ai[0] == 0f)
                {
                    if (head)
                    {
                        NPC.ai[3] = (float)NPC.whoAmI;
                        NPC.realLife = NPC.whoAmI;
                        NPC.ai[2] = (float)wormLength;
                        NPC.ai[0] = (float)NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), bodyType, NPC.whoAmI);
                    }
                    else if (NPC.ai[2] > 0f)
                    {
                        NPC.ai[0] = (float)NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), NPC.type, NPC.whoAmI);
                        ShadeWyrmBody bodyNPC = (ShadeWyrmBody)NPC.ModNPC;
                        ShadeWyrmBody newBodyNPC = (ShadeWyrmBody)Main.npc[(int)NPC.ai[0]].ModNPC;
                        newBodyNPC.bodyNum = bodyNPC.bodyNum + 1;
                    }
                    else
                    {
                        NPC.ai[0] = (float)NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + (float)(NPC.width / 2)), (int)(NPC.position.Y + (float)NPC.height), tailType, NPC.whoAmI);
                    }
                    Main.npc[(int)NPC.ai[0]].ai[3] = NPC.ai[3];
                    Main.npc[(int)NPC.ai[0]].realLife = NPC.realLife;
                    Main.npc[(int)NPC.ai[0]].ai[1] = (float)NPC.whoAmI;
                    Main.npc[(int)NPC.ai[0]].ai[2] = NPC.ai[2] - 1f;
                    NPC.netUpdate = true;
                }
                if (!head && (!Main.npc[(int)NPC.ai[1]].active || (Main.npc[(int)NPC.ai[1]].type != headType && Main.npc[(int)NPC.ai[1]].type != bodyType)))
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                }
                if (!tail && (!Main.npc[(int)NPC.ai[0]].active || (Main.npc[(int)NPC.ai[0]].type != bodyType && Main.npc[(int)NPC.ai[0]].type != tailType)))
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                }
                if (!NPC.active && Main.netMode == 2)
                {
                    NetMessage.SendData(28, -1, -1, null, NPC.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            int num180 = (int)(NPC.position.X / 16f) - 1;
            int num181 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
            int num182 = (int)(NPC.position.Y / 16f) - 1;
            int num183 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
            if (num180 < 0)
            {
                num180 = 0;
            }
            if (num181 > Main.maxTilesX)
            {
                num181 = Main.maxTilesX;
            }
            if (num182 < 0)
            {
                num182 = 0;
            }
            if (num183 > Main.maxTilesY)
            {
                num183 = Main.maxTilesY;
            }
            bool collision = flies;
            if (!collision)
            {
                for (int num184 = num180; num184 < num181; num184++)
                {
                    for (int num185 = num182; num185 < num183; num185++)
                    {
                        if (Main.tile[num184, num185] != null && ((Main.tile[num184, num185].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num184, num185].TileType] || (Main.tileSolidTop[(int)Main.tile[num184, num185].TileType] && Main.tile[num184, num185].TileFrameY == 0))) || Main.tile[num184, num185].LiquidAmount > 64))
                        {
                            Vector2 vector17;
                            vector17.X = (float)(num184 * 16);
                            vector17.Y = (float)(num185 * 16);
                            if (NPC.position.X + (float)NPC.width > vector17.X && NPC.position.X < vector17.X + 16f && NPC.position.Y + (float)NPC.height > vector17.Y && NPC.position.Y < vector17.Y + 16f)
                            {
                                collision = true;
                                if (Main.rand.Next(100) == 0 && NPC.behindTiles && Main.tile[num184, num185].HasUnactuatedTile)
                                {
                                    WorldGen.KillTile(num184, num185, true, true, false);
                                }
                                if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].TileType == 2)
                                {
                                    ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].TileType;
                                }
                            }
                        }
                    }
                }
            }
            if (!collision && head)
            {
                Rectangle rectangle = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                int num186 = 1000;
                bool flag19 = true;
                for (int num187 = 0; num187 < 255; num187++)
                {
                    if (Main.player[num187].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186, (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            flag19 = false;
                            break;
                        }
                    }
                }
                if (flag19)
                {
                    collision = true;
                }
            }
            if (head && collision && aiTimer == 600)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC other = Main.npc[i];
                    if (other.type == NPCType<ShadeWyrmHead>() || other.type == NPCType<ShadeWyrmBody>() || other.type == NPCType<ShadeWyrmTail>())
                    {
                        ShadeWyrmAI wyrm = (ShadeWyrmAI)Main.npc[i].ModNPC;
                        if (other.ai[3] == NPC.whoAmI && other.active)
                        {
                            wyrm.aiTimer = 601;
                        }
                    }
                }
            }
            if (aiTimer > 600 && head)
            {
                NPC.velocity *= 0.95f;
                return;
            }
            if (directional)
            {
                if (NPC.velocity.X < 0f)
                {
                    NPC.spriteDirection = 1;
                }
                else if (NPC.velocity.X > 0f)
                {
                    NPC.spriteDirection = -1;
                }
            }
            float num188 = speed;
            float num189 = turnSpeed;
            Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
            float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
            num191 = (float)((int)(num191 / 16f) * 16);
            num192 = (float)((int)(num192 / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
            if (NPC.ai[1] > 0f && NPC.ai[1] < (float)Main.npc.Length)
            {
                try
                {
                    vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                    num191 = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
                    num192 = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)System.Math.Atan2((double)num192, (double)num191) + 1.57f;
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                int num194 = NPC.width;
                num193 = (num193 - (float)num194) / num193;
                num191 *= num193;
                num192 *= num193;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num191;
                NPC.position.Y = NPC.position.Y + num192;
                if (directional)
                {
                    if (num191 < 0f)
                    {
                        NPC.spriteDirection = 1;
                    }
                    if (num191 > 0f)
                    {
                        NPC.spriteDirection = -1;
                    }
                }
            }
            else
            {
                if (!collision)
                {
                    NPC.TargetClosest(true);
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.velocity.Y > num188)
                    {
                        NPC.velocity.Y = num188;
                    }
                    if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.4)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
                        }
                    }
                    else if (NPC.velocity.Y == num188)
                    {
                        if (NPC.velocity.X < num191)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189;
                        }
                        else if (NPC.velocity.X > num191)
                        {
                            NPC.velocity.X = NPC.velocity.X - num189;
                        }
                    }
                    else if (NPC.velocity.Y > 4f)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189 * 0.9f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num189 * 0.9f;
                        }
                    }
                }
                else
                {
                    if (!flies && NPC.behindTiles && NPC.soundDelay == 0)
                    {
                        float num195 = num193 / 40f;
                        if (num195 < 10f)
                        {
                            num195 = 10f;
                        }
                        if (num195 > 20f)
                        {
                            num195 = 20f;
                        }
                        NPC.soundDelay = (int)num195;
                        SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                    }
                    num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                    float num196 = System.Math.Abs(num191);
                    float num197 = System.Math.Abs(num192);
                    float num198 = num188 / num193;
                    num191 *= num198;
                    num192 *= num198;
                    if (ShouldRun())
                    {
                        bool flag20 = true;
                        for (int num199 = 0; num199 < 255; num199++)
                        {
                            if (Main.player[num199].active && !Main.player[num199].dead && Main.player[num199].ZoneCorrupt)
                            {
                                flag20 = false;
                            }
                        }
                        if (flag20)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient && (double)(NPC.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
                            {
                                NPC.active = false;
                                int num200 = (int)NPC.ai[0];
                                while (num200 > 0 && num200 < 200 && Main.npc[num200].active && Main.npc[num200].aiStyle == NPC.aiStyle)
                                {
                                    int num201 = (int)Main.npc[num200].ai[0];
                                    Main.npc[num200].active = false;
                                    NPC.life = 0;
                                    if (Main.netMode == 2)
                                    {
                                        NetMessage.SendData(23, -1, -1, null, num200, 0f, 0f, 0f, 0, 0, 0);
                                    }
                                    num200 = num201;
                                }
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(23, -1, -1, null, NPC.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }
                            num191 = 0f;
                            num192 = num188;
                        }
                    }
                    bool flag21 = false;
                    if (NPC.type == 87)
                    {
                        if (((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f) || (NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)) && System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y) > num189 / 2f && num193 < 300f)
                        {
                            flag21 = true;
                            if (System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y) < num188)
                            {
                                NPC.velocity *= 1.1f;
                            }
                        }
                        if (NPC.position.Y > Main.player[NPC.target].position.Y || (double)(Main.player[NPC.target].position.Y / 16f) > Main.worldSurface || Main.player[NPC.target].dead)
                        {
                            flag21 = true;
                            if (System.Math.Abs(NPC.velocity.X) < num188 / 2f)
                            {
                                if (NPC.velocity.X == 0f)
                                {
                                    NPC.velocity.X = NPC.velocity.X - (float)NPC.direction;
                                }
                                NPC.velocity.X = NPC.velocity.X * 1.1f;
                            }
                            else
                            {
                                if (NPC.velocity.Y > -num188)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num189;
                                }
                            }
                        }
                    }
                    if (!flag21)
                    {
                        if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) || (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
                        {
                            if (NPC.velocity.X < num191)
                            {
                                NPC.velocity.X = NPC.velocity.X + num189;
                            }
                            else
                            {
                                if (NPC.velocity.X > num191)
                                {
                                    NPC.velocity.X = NPC.velocity.X - num189;
                                }
                            }
                            if (NPC.velocity.Y < num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num189;
                            }
                            else
                            {
                                if (NPC.velocity.Y > num192)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num189;
                                }
                            }
                            if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f)))
                            {
                                if (NPC.velocity.Y > 0f)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
                                }
                                else
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
                                }
                            }
                            if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)))
                            {
                                if (NPC.velocity.X > 0f)
                                {
                                    NPC.velocity.X = NPC.velocity.X + num189 * 2f;
                                }
                                else
                                {
                                    NPC.velocity.X = NPC.velocity.X - num189 * 2f;
                                }
                            }
                        }
                        else
                        {
                            if (num196 > num197)
                            {
                                if (NPC.velocity.X < num191)
                                {
                                    NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
                                }
                                else if (NPC.velocity.X > num191)
                                {
                                    NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
                                }
                                if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                                {
                                    if (NPC.velocity.Y > 0f)
                                    {
                                        NPC.velocity.Y = NPC.velocity.Y + num189;
                                    }
                                    else
                                    {
                                        NPC.velocity.Y = NPC.velocity.Y - num189;
                                    }
                                }
                            }
                            else
                            {
                                if (NPC.velocity.Y < num192)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
                                }
                                else if (NPC.velocity.Y > num192)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
                                }
                                if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                                {
                                    if (NPC.velocity.X > 0f)
                                    {
                                        NPC.velocity.X = NPC.velocity.X + num189;
                                    }
                                    else
                                    {
                                        NPC.velocity.X = NPC.velocity.X - num189;
                                    }
                                }
                            }
                        }
                    }
                }
                NPC.rotation = (float)System.Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
                if (head)
                {
                    if (collision)
                    {
                        if (NPC.localAI[0] != 1f)
                        {
                            NPC.netUpdate = true;
                        }
                        NPC.localAI[0] = 1f;
                    }
                    else
                    {
                        if (NPC.localAI[0] != 0f)
                        {
                            NPC.netUpdate = true;
                        }
                        NPC.localAI[0] = 0f;
                    }
                    if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
                    {
                        NPC.netUpdate = true;
                        return;
                    }
                }
            }
            CustomBehavior();
        }

        public virtual void Init()
        {
        }

        public virtual bool ShouldRun()
        {
            return false;
        }

        public virtual void CustomBehavior()
        {
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return head ? (bool?)null : false;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ProjectileID.LastPrismLaser && GetInstance<Config>().vItemChangesDisabled) modifiers.SourceDamage = (modifiers.SourceDamage * 0.1f);
            else if (projectile.type == ProjectileID.LastPrismLaser && !GetInstance<Config>().vItemChangesDisabled) modifiers.SourceDamage = (modifiers.SourceDamage * 0.3f);
            else if (projectile.penetrate == -1 && ProjectileID.Sets.YoyosMaximumRange[projectile.type] == 0) modifiers.SourceDamage = (modifiers.SourceDamage * 0.3f);
            else if (projectile.maxPenetrate > 10) modifiers.SourceDamage = (modifiers.SourceDamage * 0.3f);
            else if (projectile.maxPenetrate > 6) modifiers.SourceDamage = (modifiers.SourceDamage * 0.5f);
            else if (projectile.maxPenetrate > 3) modifiers.SourceDamage = (modifiers.SourceDamage * 0.8f);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            int frameHeight = texture.Height / Main.npcFrameCount[NPC.type];
            Vector2 origin = (new Vector2(texture.Width * 0.5f, frameHeight * 0.5f));
            Color color = NPC.GetAlpha(drawColor);
            Main.spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, NPC.frame, color, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = Request<Texture2D>("ElementsAwoken/Content/Events/VoidEvent/Enemies/Phase2/ShadeWyrm/" + GetType().Name + "_Glow").Value;
            Rectangle frame = new Rectangle(0, texture.Height * NPC.frame.Y, texture.Width, texture.Height);
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Color color = Color.White;
            if (Lighting.Brightness((int)(NPC.Center.X / 16), (int)(NPC.Center.Y / 16)) == 0f)
            {
                Tile t = new Tile();
                color = Lighting.GetColor((int)NPC.Center.X / 16, (int)(NPC.Center.Y / 16f));
            }
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), frame, color, NPC.rotation, origin, NPC.scale, effects, 0.0f);
        }
    }
}