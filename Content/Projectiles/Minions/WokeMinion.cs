using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.EAPlayer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class WokeMinion : ModProjectile
    {
        public float shootTimer = 0f;
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 34;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minion = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Projectile.tileCollide = false;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
        }
        public override void AI()
        {
            bool flag64 = Projectile.type == ModContent.ProjectileType<WokeMinion>();
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<AwakenedMinionBuff>(), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.wokeMinion = false;
                }
                if (modPlayer.wokeMinion)
                {
                    Projectile.timeLeft = 2;
                }
            }
            Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0.3f);

            shootTimer--;

            if (shootTimer == 45)
            {
                SoundEngine.PlaySound(new SoundStyle(EAU.SoundPath("WokeLaserCharge")), Projectile.position);
            }
            if (shootTimer <= 45 && shootTimer >= 0)
            {
                int numDusts = 20;
                if (shootTimer <= 45)
                {
                    numDusts = 1;
                }
                if (shootTimer <= 30)
                {
                    numDusts = 3;
                }
                if (shootTimer <= 20)
                {
                    numDusts = 7;
                }
                if (shootTimer <= 10)
                {
                    numDusts = 13;
                }
                for (int k = 0; k < numDusts; k++)
                {
                    int num5 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 219, 0f, 0f, 200, default(Color), 0.5f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].velocity *= 0.75f;
                    Main.dust[num5].fadeIn = 1.3f;
                    Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector.Normalize();
                    vector *= (float)Main.rand.Next(50, 100) * 0.04f;
                    Main.dust[num5].velocity = vector;
                    vector.Normalize();
                    vector *= 34f;
                    Main.dust[num5].position = Projectile.Center - vector;
                }
            }
            if (Projectile.owner == Main.myPlayer)
            {
                float max = 400f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.CanBeChasedBy(this) && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 9f;
                        if (shootTimer <= 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
                            Vector2 leftEye = new Vector2(Projectile.Center.X - 8f, Projectile.Center.Y - 5);
                            Vector2 rightEye = new Vector2(Projectile.Center.X + 8f, Projectile.Center.Y - 5);

                            float leftRotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                            float rightRotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);

                            Vector2 leftSpeed = new Vector2((float)((Math.Cos(leftRotation) * Speed) * -1), (float)((Math.Sin(leftRotation) * Speed) * -1));
                            Vector2 rightSpeed = new Vector2((float)((Math.Cos(rightRotation) * Speed) * -1), (float)((Math.Sin(rightRotation) * Speed) * -1));

                            Projectile.NewProjectile(EAU.Proj(Projectile), leftEye.X, leftEye.Y, leftSpeed.X, leftSpeed.Y, ModContent.ProjectileType<WokeBeam>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner);
                            Projectile.NewProjectile(EAU.Proj(Projectile), rightEye.X, rightEye.Y, rightSpeed.X, rightSpeed.Y, ModContent.ProjectileType<WokeBeam>(), Projectile.damage * 2, Projectile.knockBack, Projectile.owner);
                            shootTimer = 75;
                        }
                    }
                }
            }


            ProjectileUtils.PushOtherEntities(Projectile);


            float num535 = Projectile.position.X;
            float num536 = Projectile.position.Y;
            float num537 = 900f;
            bool attacking = false;
            int num538 = 500;

            if (Math.Abs(Projectile.Center.X - player.Center.X) + Math.Abs(Projectile.Center.Y - player.Center.Y) > (float)num538)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.tileCollide = true;
                NPC targettedNPC = Projectile.OwnerMinionAttackTargetNPC;
                if (targettedNPC != null && targettedNPC.CanBeChasedBy(Projectile, false))
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * 200, (float)Math.Cos(angle) * 200);

                    float targetX = targettedNPC.Center.X + offset.X;
                    float targetY = targettedNPC.Center.Y + offset.Y;
                    float num541 = Math.Abs(Projectile.Center.X - targetX) + Math.Abs(Projectile.Center.Y - targetY);
                    if (num541 < num537 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, targettedNPC.position, targettedNPC.width, targettedNPC.height))
                    {
                        num537 = num541;
                        num535 = targetX;
                        num536 = targetY;
                        attacking = true;
                    }
                }
                if (!attacking)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC nPC = Main.npc[i];
                        if (nPC.CanBeChasedBy(Projectile, false))
                        {
                            double angle = Main.rand.NextDouble() * 2d * Math.PI;
                            Vector2 offset = new Vector2((float)Math.Sin(angle) * 200, (float)Math.Cos(angle) * 200);

                            float targetX = nPC.Center.X + offset.X;
                            float targetY = nPC.Center.Y + offset.Y;
                            float num541 = Math.Abs(Projectile.Center.X - targetX) + Math.Abs(Projectile.Center.Y - targetY);
                            if (num541 < num537 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height))
                            {
                                num537 = num541;
                                num535 = targetX;
                                num536 = targetY;
                                attacking = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Projectile.tileCollide = false;
            }
            // idle
            if (!attacking)
            {
                float num546 = 8f;
                if (Projectile.ai[0] == 1f)
                {
                    num546 = 12f;
                }
                Vector2 vector42 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num547 = player.Center.X - vector42.X;
                float num548 = player.Center.Y - vector42.Y - 60f;
                float num549 = (float)Math.Sqrt((double)(num547 * num547 + num548 * num548));
                if (num549 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                }
                if (num549 > 2000f)
                {
                    Projectile.position.X = player.Center.X - (float)(Projectile.width / 2);
                    Projectile.position.Y = player.Center.Y - (float)(Projectile.width / 2);
                }
                if (num549 > 70f)
                {
                    num549 = num546 / num549;
                    num547 *= num549;
                    num548 *= num549;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num547) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num548) / 21f;
                }
                else
                {
                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X = -0.15f;
                        Projectile.velocity.Y = -0.05f;
                    }
                    Projectile.velocity *= 1.01f;
                }
                Projectile.rotation = Projectile.velocity.X * 0.05f;

                if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                    return;
                }
            }
            // attack
            else
            {
                if (Projectile.ai[1] == -1f)
                {
                    Projectile.ai[1] = 17f;
                }
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 1f;
                }
                if (Projectile.ai[1] == 0f)
                {
                    float num550 = 8f;
                    float num551 = num535 - Projectile.Center.X;
                    float num552 = num536 - Projectile.Center.Y;
                    float num553 = (float)Math.Sqrt((double)(num551 * num551 + num552 * num552));
                    if (num553 < 100f)
                    {
                        num550 = 10f;
                    }
                    num553 = num550 / num553;
                    num551 *= num553;
                    num552 *= num553;
                    Projectile.velocity.X = (Projectile.velocity.X * 14f + num551) / 15f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 14f + num552) / 15f;
                }
                else
                {
                    if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 10f)
                    {
                        Projectile.velocity *= 1.05f;
                    }
                }
                Projectile.rotation = Projectile.velocity.X * 0.05f;
                if ((double)Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                    return;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}