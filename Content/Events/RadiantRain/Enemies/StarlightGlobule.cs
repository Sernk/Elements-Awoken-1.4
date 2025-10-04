using ElementsAwoken.Content.Buffs.Debuffs;
using ElementsAwoken.Content.Items.ItemSets.Radia;
using ElementsAwoken.Content.Projectiles.NPCProj;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static ElementsAwoken.EASystem.Biome.EABiomes;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Content.Events.RadiantRain.Enemies
{
    public class StarlightGlobule : ModNPC
    {
        private float timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 26;
            NPC.aiStyle = -1;
            NPC.lifeMax = 7600;
            NPC.damage = 100;
            NPC.defense = 40;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 3, 0, 0);
            NPC.knockBackResist = 0.75f;
            SpawnModBiomes = [ModContent.GetInstance<RadiantRainBiome>().Type];
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([new FlavorTextBestiaryInfoElement("Mods.ElementsAwoken.Bestiary.Enemies.StarlightGlobule")]);
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 15000;
            NPC.damage = 150;
            NPC.defense = 50;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 20000;
                NPC.damage = 200;
                NPC.defense = 65;
            }
        }
        public override bool PreKill()
        {
            if (NPC.scale < 1) return false;
            return base.PreKill();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<Radia>()));
            npcLoot.Add(ItemDropRule.Common(ItemType<GlobuleCannon>(), 30));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffType<Starstruck>(), 300);
        }
        public override void AI()
        {
            NPC.TargetClosest(false);
            Player P = Main.player[NPC.target];
            if (NPC.scale < 1)
            {
                NPC.ai[0]++;
                if (NPC.ai[0] > 420)
                {
                    for (int p = 1; p <= 3; p++)
                    {
                        float strength = p * 2f;
                        int numDusts = p * 10;
                        for (int i = 0; i < numDusts; i++)
                        {
                            Vector2 position = (Vector2.One * new Vector2((float)NPC.width / 2f, (float)NPC.height) * 0.75f * 0.5f).RotatedBy((double)((float)(i - (numDusts / 2 - 1)) * 6.28318548f / (float)numDusts), default(Vector2)) + NPC.Center;
                            Vector2 velocity = position - NPC.Center;
                            int dust = Dust.NewDust(position + velocity, 0, 0, EAU.PinkFlame, velocity.X * 2f, velocity.Y * 2f, 100, default(Color), 1.4f);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].noLight = true;
                            Main.dust[dust].velocity = Vector2.Normalize(velocity) * strength;
                        }
                    }
                    NPC.SimpleStrikeNPC(NPC.lifeMax * 2, 0, false, 0f, DamageClass.Default, false, 0, false);
                    SoundEngine.PlaySound(SoundID.Item94, NPC.position);
                    Projectile exp = Main.projectile[Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ProjectileType<ExplosionHostile>(), NPC.damage / 2, 5f, Main.myPlayer, 0f, 0f)];
                    exp.width = 125;
                    exp.height = 125;
                    exp.Center = NPC.Center;
                }
                if (NPC.ai[0] > 300)
                {
                    NPC.Center = new Vector2(NPC.ai[1], NPC.ai[2]) + Main.rand.NextVector2Square(-5, 5);
                    NPC.velocity = Vector2.Zero;
                }
                else if (NPC.ai[0] == 300)
                {
                    NPC.ai[1] = NPC.Center.X;
                    NPC.ai[2] = NPC.Center.Y;
                }
            }
            if (NPC.ai[0] < 300)
            {
                NPC.rotation = NPC.velocity.X * 0.075f;

                float moveSpeed = 0.02f;
                Vector2 toTarget = new Vector2(P.Center.X - NPC.Center.X, P.Center.Y - NPC.Center.Y);
                toTarget.Normalize();
                if ((toTarget.X > 0 && NPC.velocity.X < 5) || (toTarget.X < 0 && NPC.velocity.X > -5)) NPC.velocity.X += toTarget.X * moveSpeed;
                if ((toTarget.Y > 0 && NPC.velocity.Y < 5) || (toTarget.Y < 0 && NPC.velocity.Y > -5)) NPC.velocity.Y += toTarget.Y * moveSpeed;

                float slowSpeed = Main.expertMode ? 0.93f : 0.95f;
                if ((toTarget.X > 0 && NPC.velocity.X < 0) || (toTarget.X < 0 && NPC.velocity.X > 0)) NPC.velocity.X *= slowSpeed;
                if ((toTarget.Y > 0 && NPC.velocity.Y < 0) || (toTarget.Y < 0 && NPC.velocity.Y > 0)) NPC.velocity.Y *= slowSpeed;



                if (NPC.collideX)
                {
                    NPC.velocity.X = NPC.oldVelocity.X * -0.75f;
                    if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
                    {
                        NPC.velocity.X = 2f;
                    }
                    if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
                    {
                        NPC.velocity.X = -2f;
                    }
                }
                if (NPC.collideY)
                {
                    NPC.velocity.Y = NPC.oldVelocity.Y * -0.75f;
                    if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
                    {
                        NPC.velocity.Y = 1f;
                    }
                    if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
                    {
                        NPC.velocity.Y = -1f;
                    }
                }

                for (int k = 0; k < Main.npc.Length; k++)
                {
                    NPC other = Main.npc[k];
                    if (k != NPC.whoAmI && other.type == NPC.type && other.active && Math.Abs(NPC.position.X - other.position.X) + Math.Abs(NPC.position.Y - other.position.Y) < NPC.width)
                    {
                        const float pushAway = 0.05f;
                        if (NPC.position.X < other.position.X) NPC.velocity.X -= pushAway;
                        else NPC.velocity.X += pushAway;
                        if (NPC.position.Y < other.position.Y) NPC.velocity.Y -= pushAway;
                        else NPC.velocity.Y += pushAway;
                    }
                }
                FallThroughPlatforms();
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life < 0 && NPC.scale == 1f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int n = 0; n < 3; n++)
                {
                    NPC smol = Main.npc[NPC.NewNPC(EAU.NPCs(NPC), (int)NPC.Center.X, (int)NPC.Center.Y, NPC.type)];
                    smol.scale = 0.5f;
                    smol.lifeMax = (int)(smol.lifeMax * 0.5f);
                    smol.life = smol.lifeMax;
                }
            }
        }
        private void FallThroughPlatforms()
        {
            Player P = Main.player[NPC.target];
            Vector2 platform = NPC.Bottom / 16;
            Tile platformTile = Framing.GetTileSafely((int)platform.X, (int)platform.Y);
            if (TileID.Sets.Platforms[platformTile.TileType] && NPC.Bottom.Y < P.Bottom.Y && platformTile.HasTile) NPC.position.Y += 0.3f;
        }
    }
}
