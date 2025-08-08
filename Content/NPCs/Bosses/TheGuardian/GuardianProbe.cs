using ElementsAwoken.Content.Projectiles.NPCProj.TheGuardian;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.NPCs.Bosses.TheGuardian
{
    public class GuardianProbe : ModNPC
    {
        public float speed = 0.1f;
        private float changeLocTimer
        {
            get => NPC.ai[0];
            set => NPC.ai[0] = value;
        }
        private float vectorX
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        private float vectorY
        {
            get => NPC.ai[2];
            set => NPC.ai[2] = value;
        }
        private float shootTimer
        {
            get => NPC.ai[3];
            set => NPC.ai[3] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 52;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.lifeMax = 1500;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.buffImmune[24] = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = 3000;
            NPC.damage = 75;
            if (MyWorld.awakenedMode)
            {
                NPC.lifeMax = 4500;
                NPC.damage = 85;
                NPC.defense = 30;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (NPC.frameCounter > 5)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void AI()
        {
            Player P = Main.player[NPC.target];
            Vector2 direction = P.Center - NPC.Center;
            if (direction.X > 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = direction.ToRotation();
            }
            if (direction.X < 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = direction.ToRotation() - 3.14f;
            }
            changeLocTimer--;
            if (changeLocTimer == 0)
            {
                int minDist = 150;
                vectorX = Main.rand.Next(-500, 500);
                vectorY = Main.rand.Next(-500, 500);
                if (vectorX < minDist && vectorX > 0)
                {
                    vectorX = minDist;
                }
                else if (vectorX > -minDist && vectorX < 0)
                {
                    vectorX = -minDist;
                }
                if (vectorY < minDist && vectorY > 0)
                {
                    vectorY = minDist;
                }
                else if (vectorY > -minDist && vectorX < 0)
                {
                    vectorY = -minDist;
                }
                changeLocTimer = 200;
                speed = Main.rand.NextFloat(0.05f, 0.1f);
            }
            if (shootTimer <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);

                float Speed = 12f;
                float rotation = (float)Math.Atan2(NPC.Center.Y - P.Center.Y, NPC.Center.X - P.Center.X);
                Projectile.NewProjectile(EAU.NPCs(NPC), NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), ModContent.ProjectileType<GuardianShot>(), 60, 0f, Main.myPlayer);
                shootTimer = Main.rand.Next(150, 300);
                NPC.netUpdate = true;
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<TheGuardianFly>())) NPC.active = false;

            int dustLength = ModContent.GetInstance<Config>().lowDust ? 1 : 3;
            for (int i = 0; i < dustLength; i++)
            {
                Dust dust = Main.dust[Dust.NewDust(NPC.Center - Vector2.One * 2, 4, 4, 6)];
                dust.velocity = Vector2.Zero;
                dust.position -= NPC.velocity / dustLength * (float)i;
                dust.noGravity = true;
            }
            // movement
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Vector2 vector75 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float targetX = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) + vectorX - vector75.X + Main.rand.Next(-25, 25);
            float targetY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) + vectorY - vector75.Y + Main.rand.Next(-25, 25);
            if (NPC.velocity.X < targetX)
            {
                NPC.velocity.X = NPC.velocity.X + speed * 2;
            }
            else if (NPC.velocity.X > targetX)
            {
                NPC.velocity.X = NPC.velocity.X - speed * 2;
            }
            if (NPC.velocity.Y < targetY)
            {
                NPC.velocity.Y = NPC.velocity.Y + speed;
                if (NPC.velocity.Y < 0f && targetY > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y + speed;
                    return;
                }
            }
            else if (NPC.velocity.Y > targetY)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed;
                if (NPC.velocity.Y > 0f && targetY < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y - speed;
                    return;
                }
            }
        }      
    }
}