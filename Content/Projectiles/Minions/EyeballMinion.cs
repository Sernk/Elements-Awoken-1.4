using ElementsAwoken.Content.Buffs.MinionBuffs;
using ElementsAwoken.EASystem.Global;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Content.Projectiles.Minions
{
    public class EyeballMinion : ModProjectile
    {
        public float shootTimer = 0f;
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
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
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 16)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                    Projectile.frame = 0;
            }
            return true; 
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            player.AddBuff(ModContent.BuffType<Eyeball>(), 3600);

            if (player.dead)
            {
                modPlayer.eyeballMinion = false;
            }
            if (modPlayer.eyeballMinion)
            {
                Projectile.timeLeft = 2;
            }
        
            Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0.3f);

            shootTimer--;
            if (Projectile.owner == Main.myPlayer)
            {
                float max = 400f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && Vector2.Distance(Projectile.Center, nPC.Center) <= max)
                    {
                        float Speed = 9f;
                        float rotation = (float)Math.Atan2(Projectile.Center.Y - nPC.Center.Y, Projectile.Center.X - nPC.Center.X);
                        if (shootTimer <= 0)
                        {
                            Vector2 speed = new Vector2((float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1));
                            SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
                            Projectile.NewProjectile(EAU.Proj(Projectile), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, ModContent.ProjectileType<CelestialBoltFriendly>(), 30, Projectile.knockBack, Projectile.owner, 0f, Main.rand.Next(4));
                            shootTimer = 30;
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