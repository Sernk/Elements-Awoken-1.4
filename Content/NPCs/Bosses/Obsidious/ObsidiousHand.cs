using ElementsAwoken.Content.Projectiles.NPCProj;
using ElementsAwoken.Content.Projectiles.NPCProj.Obsidious;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.Obsidious
{
    public class ObsidiousHand : ModNPC
    {
        private float handSwipeTimer = 0;
        private float direction
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float swipeAI
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float aiTimer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(handSwipeTimer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            handSwipeTimer = reader.ReadSingle();
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 10000;
            NPC.damage = 90;
            NPC.defense = 25;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.width = 52;
            NPC.height = 76;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.immortal = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
            NPC.dontTakeDamage = true;
            NPC.npcSlots = 1f;
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Obsidious Hand");
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.damage = 140;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            NPC parent = Main.npc[(int)NPC.ai[1]];
            if (parent.ai[1] == 3)
            {
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ExplosionHostile>(), NPC.damage, 1f, 0, 0f, 0f);
                SoundEngine.PlaySound(SoundID.Item14, NPC.position);
                for (int num369 = 0; num369 < 20; num369++)
                {
                    int num370 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, EAU.PinkFlame, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num370].velocity *= 1.4f;
                }
                for (int num371 = 0; num371 < 10; num371++)
                {
                    int num372 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 62, 0f, 0f, 100, default(Color), 2.5f);
                    Main.dust[num372].noGravity = true;
                    Main.dust[num372].velocity *= 5f;
                    num372 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 62, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num372].velocity *= 3f;
                }
                int num373 = Gore.NewGore(EAU.NPCs(NPC), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore85 = Main.gore[num373];
                gore85.velocity.X = gore85.velocity.X + 1f;
                Gore gore86 = Main.gore[num373];
                gore86.velocity.Y = gore86.velocity.Y + 1f;
                num373 = Gore.NewGore(EAU.NPCs(NPC), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore87 = Main.gore[num373];
                gore87.velocity.X = gore87.velocity.X - 1f;
                Gore gore88 = Main.gore[num373];
                gore88.velocity.Y = gore88.velocity.Y + 1f;
                num373 = Gore.NewGore(EAU.NPCs(NPC), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore89 = Main.gore[num373];
                gore89.velocity.X = gore89.velocity.X + 1f;
                Gore gore90 = Main.gore[num373];
                gore90.velocity.Y = gore90.velocity.Y - 1f;
                num373 = Gore.NewGore(EAU.NPCs(NPC), new Vector2(NPC.position.X, NPC.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num373].velocity *= 0.4f;
                Gore gore91 = Main.gore[num373];
                gore91.velocity.X = gore91.velocity.X - 1f;
                Gore gore92 = Main.gore[num373];
                gore92.velocity.Y = gore92.velocity.Y - 1f;
            }
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.5f, 0.5f, 0.5f);
            NPC parent = Main.npc[(int)NPC.ai[1]];
            Player player = Main.player[NPC.target];
            NPC.active = parent.active;
            if (NPC.localAI[0] == 0)
            {
                // bad way to do this probably :lul:
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.position.X, NPC.position.Y, 0, 0, ModContent.ProjectileType<ObsidiousHandOverlay>(), 0, 0, Main.myPlayer, 0, NPC.whoAmI);
                NPC.alpha = 255; // so u cant see the weird ass offset :shruggy:
                NPC.localAI[0]++;
                NPC.netUpdate = true;
            }
            if (parent.ai[1] == 2)
            {
                NPC.localAI[1] = 0;
                NPC.netUpdate = true;
            }
            if (parent.ai[1] == 1 && NPC.localAI[1] == 0)
            {
                int orbitalCount = 3;
                for (int l = 0; l < orbitalCount; l++)
                {
                    int distance = 360 / orbitalCount;
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ObsidiousRockOrbital>(), NPC.damage, 0f, Main.myPlayer, l * distance, NPC.whoAmI);
                }
                NPC.localAI[1]++;
                NPC.netUpdate = true;
            }
            if (parent.ai[3] != 1)
            {
                aiTimer++;
                if (aiTimer >= 300)
                {
                    aiTimer = 0;
                    swipeAI++;
                }
                if (swipeAI > 1)
                {
                    swipeAI = 0;
                }
                if (swipeAI == 0)
                {
                    float targetX = parent.Center.X + 110 * direction - (NPC.width * 0.5f) * direction;
                    float targetY = parent.Center.Y + 50 - (NPC.height * 0.5f);
                    int maxDist = 1000;
                    if (Vector2.Distance(new Vector2(targetX, targetY), NPC.Center) >= maxDist)
                    {
                        float moveSpeed = 8f;
                        Vector2 toTarget = new Vector2(targetX, targetY);
                        toTarget.Normalize();
                        NPC.velocity = toTarget * moveSpeed;
                    }
                    else
                    {
                        if (NPC.Center.Y > targetY)
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y * 0.96f;
                            }
                            NPC.velocity.Y = NPC.velocity.Y - 0.07f;
                            if (NPC.velocity.Y > 3f)
                            {
                                NPC.velocity.Y = 3f;
                            }
                        }
                        else if (NPC.Center.Y < targetY)
                        {
                            if (NPC.velocity.Y < 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y * 0.96f;
                            }
                            NPC.velocity.Y = NPC.velocity.Y + 0.07f;
                            if (NPC.velocity.Y < -3f)
                            {
                                NPC.velocity.Y = -3f;
                            }
                        }
                        if (NPC.Center.X > targetX)
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X * 0.96f;
                            }
                            NPC.velocity.X = NPC.velocity.X - 0.35f;
                            if (NPC.velocity.X > 12f)
                            {
                                NPC.velocity.X = 12f;
                            }
                        }
                        else if (NPC.Center.X < targetX)
                        {
                            if (NPC.velocity.X < 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X * 0.96f;
                            }
                            NPC.velocity.X = NPC.velocity.X + 0.35f;
                            if (NPC.velocity.X < -12f)
                            {
                                NPC.velocity.X = -12f;
                            }
                        }
                    }
                    NPC.rotation = 0;
                    NPC.spriteDirection = (int)direction;
                }
                if (swipeAI == 1)
                {
                    Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X + 10 * direction, NPC.Center.Y - 20, 0, 0, ModContent.ProjectileType<ObsidiousHandTrail>(), (int)(NPC.damage / 2), 1, Main.myPlayer, parent.ai[1]);

                    handSwipeTimer++;

                    float speed = 15f;
                    float num25 = player.Center.X - NPC.Center.X;
                    float num26 = player.Center.Y - NPC.Center.Y;
                    float num27 = (float)Math.Sqrt(num25 * num25 + num26 * num26); // pythagorus distance between points
                    num27 = speed / num27;
                    NPC.velocity.X = num25 * num27;
                    NPC.velocity.Y = num26 * num27;
                    if (handSwipeTimer >= 30)
                    {
                        swipeAI++;
                        handSwipeTimer = 0;
                        if (parent.ai[1] == 2)
                        {
                            int numberProjectiles = parent.life <= parent.lifeMax * 0.5f ? Main.rand.Next(4, 8) : Main.rand.Next(2, 4);
                            SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                            for (int i = 0; i < numberProjectiles; i++)
                            {
                                Vector2 perturbedSpeed = new Vector2(NPC.velocity.X, NPC.velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
                                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ObsidiousIceCrystal>(), NPC.damage / 2, 0f, Main.myPlayer, 0f, 0f);
                            }
                        }
                    }
                    NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
                    Vector2 playerDir = player.Center - NPC.Center;
                    if (playerDir.X > 0f)
                    {
                        NPC.spriteDirection = 1;
                    }
                    if (playerDir.X < 0f)
                    {
                        NPC.spriteDirection = -1;
                    }
                }
            }
            else
            {            
                float targetX = parent.Center.X + 80 * direction - (NPC.width * 0.5f) * direction;
                float targetY = parent.Center.Y + 60 - (NPC.height * 0.5f);
                int maxDist = 1000;
                if (Vector2.Distance(new Vector2(targetX, targetY), NPC.Center) >= maxDist)
                {
                    float moveSpeed = 8f;
                    Vector2 toTarget = new Vector2(targetX, targetY);
                    toTarget.Normalize();
                    NPC.velocity = toTarget * moveSpeed;
                }
                else
                {
                    if (NPC.Center.Y > targetY)
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y * 0.96f;
                        }
                        NPC.velocity.Y = NPC.velocity.Y - 0.15f;
                        if (NPC.velocity.Y > 3f)
                        {
                            NPC.velocity.Y = 3f;
                        }
                    }
                    else if (NPC.Center.Y < targetY)
                    {
                        if (NPC.velocity.Y < 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y * 0.96f;
                        }
                        NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                        if (NPC.velocity.Y < -3f)
                        {
                            NPC.velocity.Y = -3f;
                        }
                    }
                    if (NPC.Center.X > targetX)
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X * 0.96f;
                        }
                        NPC.velocity.X = NPC.velocity.X - 0.4f;
                        if (NPC.velocity.X > 12f)
                        {
                            NPC.velocity.X = 12f;
                        }
                    }
                    else if (NPC.Center.X < targetX)
                    {
                        if (NPC.velocity.X < 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X * 0.96f;
                        }
                        NPC.velocity.X = NPC.velocity.X + 0.4f;
                        if (NPC.velocity.X < -12f)
                        {
                            NPC.velocity.X = -12f;
                        }
                    }
                }
                NPC.rotation = 0;
                NPC.spriteDirection = (int)direction;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}